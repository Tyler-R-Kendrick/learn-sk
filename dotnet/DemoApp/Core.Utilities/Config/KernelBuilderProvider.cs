using Microsoft.SemanticKernel;

namespace Core.Utilities.Config
{
    public static class KernelBuilderProvider
    {
        public static IKernelBuilder CreateKernelWithChatCompletion()
        {
            var applicationSettings = AISettingsProvider.GetSettings();

            var builder = Kernel.CreateBuilder();

            builder.AddAzureOpenAIChatCompletion(
                applicationSettings.OpenAI.ModelName,
                applicationSettings.OpenAI.Endpoint,
                applicationSettings.OpenAI.Key);

            return builder;
        }

    }
}