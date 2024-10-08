from core.utilities.services.censorservice import CensorService
from semantic_kernel.filters.prompts.prompt_render_context import PromptRenderContext
from semantic_kernel.functions.function_result import FunctionResult
from collections.abc import Callable, Coroutine
from typing import Any

class CensoredPromptRenderFilter ():
    def __init__(self, censor_service: CensorService) -> None:
        self._censor_service = censor_service

    async def OnPromptRenderAsync(self, context: PromptRenderContext,
                next: Callable[[PromptRenderContext], Coroutine[Any, Any, None]]) -> None:

        await next(context)

        try:
          self._censor_service.reject(context.rendered_prompt)
        except ValueError as e:
            context.function_result = FunctionResult(function=context.function.metadata, value="I'm sorry, you've mentioned a topic that's very sensitive to Cubs fans. I cannot continue this conversation.") 
