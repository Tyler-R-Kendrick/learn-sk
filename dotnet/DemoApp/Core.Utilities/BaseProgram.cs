using Ardalis.GuardClauses;
using Core.Utilities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using System.Reflection;

namespace Core.Utilities
{
    public class BaseProgram
    {
        public static IKernelBuilder CreateKernelWithChatCompletion(ApplicationSettings applicationSettings)
        {
            var builder = Kernel.CreateBuilder();

            builder.AddAzureOpenAIChatCompletion(
                applicationSettings.OpenAI.ModelName,
                applicationSettings.OpenAI.Endpoint,
                applicationSettings.OpenAI.Key);

            return builder;
        }

        public static ApplicationSettings GetApplicationSettings()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();
            
            var applicationSettings = config.GetSection("ApplicationSettings").Get<ApplicationSettings>();

            Guard.Against.Null(applicationSettings);
            Guard.Against.Null(applicationSettings.OpenAI);
            Guard.Against.NullOrEmpty(applicationSettings.OpenAI.ModelName);
            Guard.Against.NullOrEmpty(applicationSettings.OpenAI.Key);
            Guard.Against.NullOrEmpty(applicationSettings.OpenAI.Endpoint);

            return applicationSettings;
        }
    }
}
