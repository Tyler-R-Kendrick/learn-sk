using Microsoft.Extensions.Configuration;

namespace Config
{
    public class AzureAIConfig
    {
        public string Endpoint { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string DeploymentName { get; set; } = string.Empty;
    }

    public static class AzureAIConfigProvider
    {
        public static AzureAIConfig LoadConfig()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddUserSecrets<AzureAIConfig>()
                .Build();

            var configItems = new List<string> { "AzureAIEndpoint", "AzureAIDeploymentName", "AzureAIKey" };

            foreach (var configItem in configItems)
            {
                if (config[configItem] == null)
                {
                    throw new ArgumentNullException(configItem);
                }
            }

            var aiConfig = new AzureAIConfig()
            {
                Endpoint = config["AzureAIEndpoint"]!,
                DeploymentName = config["AzureAIDeploymentName"]!,
                Key = config["AzureAIKey"]!,
            };

            return aiConfig;
        } 
    }
    

}
