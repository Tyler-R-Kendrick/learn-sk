// See https://aka.ms/new-console-template for more information


using Config;

var config = AzureAIConfigProvider.Load();

Console.WriteLine($"Endpoint: {config.Endpoint}");
