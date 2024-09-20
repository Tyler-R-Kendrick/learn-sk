using Microsoft.SemanticKernel;

namespace Solution5;

public class MlbCitationFunctionFilter : IFunctionInvocationFilter
{
    public async Task OnFunctionInvocationAsync(
        FunctionInvocationContext context,
        Func<FunctionInvocationContext, Task> next)
    {
        await next(context);
        if(context.Function.PluginName != nameof(MlbBaseballDataPlugin)) return;
        var result = context.Result;
        Console.WriteLine($"intercepting MlbBaseballDataPlugin.{context.Function.Name}");
        context.Arguments.TryGetValue("citations", out object? citationsObj);
        var citations = citationsObj as string[];
        var citationsLength = citations?.Length ?? 0;   
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var prompt = result.RenderedPrompt;
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var citationIndex = citationsLength + 1;
        Console.WriteLine("formatting citation");
        var citation = FormatCitation(citationIndex, context.Function.Name, context.Arguments);
        Console.WriteLine($"formatted citation: {citation}");
        citations = [..citations, citation];
        var rewrittenResponse = $"{prompt} [^{++citationIndex}]{Environment.NewLine}{citation}";
        Console.WriteLine("rewrittenResponse:" + rewrittenResponse);
        context.Arguments["citations"] = new Dictionary<string, object?>() { ["citations"] = citations };
        context.Result = new(result, rewrittenResponse)
        {
            //Metadata = new Dictionary<string, object?>() { ["citations"] = citations }
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
