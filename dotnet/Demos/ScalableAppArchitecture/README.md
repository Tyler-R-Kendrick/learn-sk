# Scalable LLM Apps

## Prompt optimization
    * summarization plugin.
    * chat history reducers
    * minimal plugins

## Memory
    * response caching with vectorDb

## Domain Decomposition

### Right sizing your agents
    * kernel per agent (agent as microservice domain)
    * minimal plugins per agent (plugin as microservice)

### Right sizing your plugins
    * minimal functions per plugin.

## Isolation

### Models
    * model instance / key - per process

### Kernel usage
    * principal of least privilage.
    * kernel per agent.
    * kernel per chat service
    * kernel per group chat.
    * model key per agent / app.

### Agent usage
    * agent per process
    * chat history reducers

## Resiliency Patterns
    * Transient Fault Handling
    * graceful degredation of services

## Monitoring
    * Aspire
    * App Insights
    * Filters
    * Logging (in Agents and Agent group chats).
    * Prompt Flow

## Performance
    * Prefer streaming where possible.
    * constrained decoding + local completion for structured output (guidance / json schema)
    * local models / slms

## Patterns
    * External Configuration Store: Store prompts externally (prompty)
    * Monitor with prompt flow
    * Avoid function calling in templates: prefer variables instead.
    * Constrained decoding for structured outputs.
        * Json schema
        * Logit biasing
    * Sagas - choreography vs orchestration for agents.
    * Actor Model for Agents and "Processes".
    * Retry
    * Rate Limiting / Throttling.

## Security
    * function filters for auth.

## Responsible AI?

## Fine-tuning
    * rlhf through prompt quality ratings in UX.
    * slm fine-tuning pipeline.
    * smart route hydration based on slm performance through IAIServiceSelector

