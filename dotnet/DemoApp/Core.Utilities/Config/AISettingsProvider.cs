using Ardalis.GuardClauses;
using Core.Utilities.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Core.Utilities.Config
{
    public static class AISettingsProvider
    {
        public static AISettings GetSettings()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

            var aiSettings = config.GetSection("ApplicationSettings").Get<AISettings>();

            Guard.Against.Null(aiSettings);
            Guard.Against.Null(aiSettings.OpenAI);
            Guard.Against.NullOrEmpty(aiSettings.OpenAI.ModelName);
            Guard.Against.NullOrEmpty(aiSettings.OpenAI.Key);
            Guard.Against.NullOrEmpty(aiSettings.OpenAI.Endpoint);

            return aiSettings;
        }
    }
}
