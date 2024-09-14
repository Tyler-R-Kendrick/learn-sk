using Core.Utilities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using System.Reflection;

namespace Core.Utilities
{
    public class BaseProgram
    {
        public static Kernel CreateKernelWithChatCompletion(ApplicationSettings applicationSettings)
        {
            var builder = Kernel.CreateBuilder();

            builder.AddAzureOpenAIChatCompletion(
                applicationSettings.OpenAI.ModelName,
                applicationSettings.OpenAI.Endpoint,
                applicationSettings.OpenAI.Key);

            return builder.Build();
        }

        public static ApplicationSettings GetApplicationSettings()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

            return config.GetSection("ApplicationSettings").Get<ApplicationSettings>();
        }
    }
}
