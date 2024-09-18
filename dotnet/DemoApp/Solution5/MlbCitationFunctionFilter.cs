using Microsoft.SemanticKernel;

namespace Solution5;

public class MlbCitationFunctionFilter : IFunctionInvocationFilter
{
    public async Task OnFunctionInvocationAsync(
        FunctionInvocationContext context,
        Func<FunctionInvocationContext, Task> next)
    {
        if(context.Function.PluginName != nameof(MlbBaseballDataPlugin)) return;
        await next(context);
        var result = context.Result;
        object? citationsObj = null;
        result.Metadata?.TryGetValue("citations", out citationsObj);
        var citations = citationsObj as string[];
        var citationsLength = citations?.Length ?? 0;   
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var prompt = result.RenderedPrompt;
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var citationIndex = citationsLength + 1;
        var citation = FormatCitation(citationIndex, context.Function.Name, context.Arguments);
        citations = [..citations, citation];
        var rewrittenResponse = $"{prompt} [^{++citationIndex}]{Environment.NewLine}{citation}";
        context.Result = new(result, rewrittenResponse)
        {
            Metadata = new Dictionary<string, object?>() { ["citations"] = citations }
        };
    }

    private string FormatCitation(int index, string functionName, KernelArguments args)
    {
        const string apiEndpoint = "http://statsapi.mlb.com/api/v1/";
        static string GetArgString(KernelArguments args)
            => string.Join(";", args.Select(next => $"{next.Key}={next.Value}"));
        static string GetPath(string path, KernelArguments args)
            => $"{apiEndpoint}/{path}?{GetArgString(args)}";
        string GetCitation(string path)
            => $"[^{index}]: {GetPath(path, args)}";
        return functionName switch
        {
            nameof(MlbBaseballDataPlugin.GetGamePlayByPlay) => GetCitation(functionName),
            nameof(MlbBaseballDataPlugin.GetTeamIdsData) => GetCitation(functionName),
            nameof(MlbBaseballDataPlugin.GetTeamScheduleData) => GetCitation(functionName),
            nameof(MlbBaseballDataPlugin.GetTeamsLastPlayedGameId) => GetCitation(functionName),
            string path when path == nameof(MlbBaseballDataPlugin.GetTeamIdsData) => GetPath(path, args),
            _ => apiEndpoint 
        };
    }
}
