import os
from enum import Enum

#setup where the configuration data is stored
parent_dir = os.path.join(os.path.dirname(__file__), '..')
grandparent_dir = os.path.join(parent_dir, '..')
grandparent2_dir = os.path.join(grandparent_dir, '..')
DEFAULT_CONFIG_DIR = os.path.abspath(grandparent2_dir) 

class AIService(Enum):
    """
    Attributes:
        OpenAI (str): Represents the OpenAI service.
        AzureOpenAI (str): Represents the Azure OpenAI service.
        HuggingFace (str): Represents the HuggingFace service.
    """

    OpenAI = "openai"
    AzureOpenAI = "azureopenai"
    HuggingFace = "huggingface"


class AISettings:
    def __init__(self, deployment_name: str, endpoint: str, api_key: str, api_version: str, embedding_deployment_name: str):
        self.deployment_name = deployment_name
        self.endpoint = endpoint
        self.api_key = api_key
        self.api_version = api_version
        self.embedding_deployment_name = embedding_deployment_name
    
    @staticmethod 
    def GetSettings():  
        from dotenv import dotenv_values
        
        #print (f"{base_dir}")
        env_file = DEFAULT_CONFIG_DIR + "/.env"
        config = dotenv_values(env_file)

        #uses the GLOBAL_LLM_SERVICE variable from the .env file to determine which AI service to use
        selectedService = AIService.AzureOpenAI if config.get("GLOBAL_LLM_SERVICE") == "AzureOpenAI" else AIService.OpenAI

        if selectedService == AIService.AzureOpenAI:
            deployment = config.get("AZURE_OPENAI_CHAT_DEPLOYMENT_NAME")
            api_key = config.get("AZURE_OPENAI_API_KEY")
            endpoint = config.get("AZURE_OPENAI_ENDPOINT")
            api_version = config.get("AZURE_OPENAI_API_VERSION")
            embedding_deployment_name = config.get("AZURE_OPENAI_EMBEDDING_DEPLOYMENT_NAME")
        else:
            deployment = config.get("OPENAI_DEPLOYMENT_NAME")
            api_key = config.get("OPENAI_API_KEY")
            endpoint = config.get("OPENAI_ENDPOINT")
            api_version = config.get("OPENAI_API_VERSION")
            embedding_deployment_name = config.get("OPENAI_EMBEDDING_DEPLOYMENT_NAME")

        #print (f"deployment: {deployment}")
        #print (f"enpoint: {endpoint} ")
        #print (f"key: {api_key}")
        
        if deployment == None or api_key == None or endpoint == None:
            raise Exception("Missing configuration settings", (deployment, api_key, endpoint))
        
        return AISettings(deployment, endpoint, api_key, api_version, embedding_deployment_name)


