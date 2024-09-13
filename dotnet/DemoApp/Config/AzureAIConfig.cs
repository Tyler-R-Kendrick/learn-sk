using Microsoft.Extensions.Configuration;

namespace Config
{
    public class AzureAIConfig
    {
        public string? Endpoint { get; set; }
        public string? Key {  get; set; }
        public string? DeploymentName { get; set; }
    }

    public static class AzureAIConfigProvider
    {
        public static AzureAIConfig Load()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddUserSecrets<AzureAIConfig>()
                .Build();

            var aiConfig = new AzureAIConfig()
            {
                Endpoint = config["AzureAIEndpoint"],
                DeploymentName = config["AzureAIKey"],
                Key = config["AzureAIDeploymentName"],
            };

            return aiConfig;
        } 
    }
    

}
