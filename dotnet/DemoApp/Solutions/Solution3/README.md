# Solution

## Objective

Solution 3 builds on the previous projects to introduce the concept of chat history.
Developers should experience a conversation that demonstrates knowledge of previous messages within the chat context.

## Setup the Project

### Configuring the environment

To setup the environment, you'll need to set either the following environment variables or app settings properties:

```env
    APPLICATIONSETTINGS__OPENAI__MODELNAME=value
    APPLICATIONSETTINGS__OPENAI__KEY=value
    APPLICATIONSETTINGS__OPENAI__ENDPOINT=value
```

```appsettings.json
{
    "ApplicationSettings": {
        "OpenAI": {
            "ModelName": "value",
            "Key": "value",
            "Endpoint": "value"
        }
    }
}
```

To obtain these values, use the dashboard available for [Azure Open AI](https://oai.azure.com) and follow their [instructions](https://learn.microsoft.com/en-us/azure/ai-services/openai/quickstart?tabs=command-line%2Ctypescript%2Cpython-new&pivots=programming-language-csharp#retrieve-key-and-endpoint).

## Deploying the required resources

An Azure OpenAI resource must be available for you to connect to. You can follow the instructions on [MS Learn](https://learn.microsoft.com/en-us/azure/ai-services/openai/how-to/create-resource?pivots=web-portal) to do a custom deployment.

## Running the Solution

Open a new terminal in the ```Solution3``` folder and run ```dotnet run Solution3.csproj```
