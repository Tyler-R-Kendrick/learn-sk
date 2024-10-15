# Scalable LLM Apps

## Constrained Decoding
    * logit biasing
    * json schema
    * escape sequences

## Prompt optimization
    * summarization plugin.
    * chat history reducers

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

## Security
    * function filters for auth.

## Responsible AI?
    * prompt filters
    * 

## Fine-tuning
    * rlhf through prompt quality ratings in UX.
    * slm fine-tuning pipeline.
    * smart route hydration based on slm performance through IAIServiceSelector

