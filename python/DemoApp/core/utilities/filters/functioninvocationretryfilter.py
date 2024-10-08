from semantic_kernel.filters.functions.function_invocation_context import FunctionInvocationContext
from collections.abc import Callable, Coroutine
from typing import Any


class FunctionInvocationRetryFilter:
    def __init__(self, max_retries = 5) -> None:
        self._max_retries = max_retries

    async def OnFunctionInovationAsync(self, context: FunctionInvocationContext,
    next: Callable[[FunctionInvocationContext], Coroutine[Any, Any, None]]) -> None:
        
        count = 0
        while (count < self._max_retries):
            try:
                await next(context)
                break
            except Exception as e:
                count += 1
                print(f"Retrying function {context.function.name} due to error: {e}")
                continue
