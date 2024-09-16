using Core.Utilities.Models;
using Microsoft.SemanticKernel;

namespace Core.Utilities
{
    public class BaseProgram
    {
        public static IKernelBuilder CreateKernelWithChatCompletion(AISettings applicationSettings)
        {
            var builder = Kernel.CreateBuilder();

            builder.AddAzureOpenAIChatCompletion(
                applicationSettings.OpenAI.ModelName,
                applicationSettings.OpenAI.Endpoint,
                applicationSettings.OpenAI.Key);

            return builder;
        }
    }
}
