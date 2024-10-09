import json
import logging
from semantic_kernel.filters.functions.function_invocation_context import FunctionInvocationContext
from collections.abc import Callable, Coroutine
from typing import Any


class FunctionInvocationLoggingFilter:
    def __init__(self, logger: logging.Logger) -> None:
        self.logger = logger

    async def OnFunctionInovationAsync(self, context: FunctionInvocationContext,
    next: Callable[[FunctionInvocationContext], Coroutine[Any, Any, None]]) -> None:
        if self.logger.level == logging.DEBUG:
            self.logger.debug(f"function invoking: {context.function.name}")
            self.logger.debug(f"arguments: {json.dumps(context.arguments)}")

        await next(context)

        if self.logger.level == logging.DEBUG:
            self.logger.debug(f"function invoked: {context.function.name}")
            self.logger.debug(f"results: \n {context.result}")
