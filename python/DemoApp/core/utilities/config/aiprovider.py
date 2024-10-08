from semantic_kernel import Kernel

class AIProvider:
    @staticmethod 
    def GetKernel() -> Kernel:
        from core.utilities.config.aisettings import AISettings
        from semantic_kernel.connectors.ai.open_ai import AzureChatCompletion
        
        kernel = Kernel()
        settings = AISettings.GetSettings()

        service_id = "default"
        chat_completion_service = AzureChatCompletion(service_id=service_id, 
                        deployment_name=settings.deployment_name,
                        endpoint = settings.endpoint,
                        api_key = settings.api_key,
                        api_version=settings.api_version)
        kernel.add_service(chat_completion_service)
        
        return kernel
    