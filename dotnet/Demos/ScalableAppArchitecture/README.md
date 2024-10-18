# Scalable LLM Apps with Microsoft's Semantic Kernel for .NET

## Introduction to Scalable LLM Application Design
   - **Overview**: Understanding LLM-based applications and scalability challenges.
   - **Microsoft’s Semantic Kernel**: Role and features for building scalable LLM applications.
   - **Objective**: Principles and practices we’ll cover—12-Factor App, Microservices, SOLID principles, Reactive Architecture, and Security.

## Core Design Principles for Scalability
    - 12-Factor Apps: Applying these principles to LLM applications to standardize and scale.
    - Microservices Analogs:
        - Bounded Contexts & Domain-Driven Design: Ensuring separation of concerns and clear domain boundaries.
        - Agent as Bounded Context: Using the Semantic Kernel to manage LLM agents as domain-focused microservices.
        - Process as Process:
        - Plugin as Microservice: Assigning minimal, purpose-driven plugins to agents for domain-specific tasks.
        - Model per service: For same reasons as database per service.
        - db per service: Includes vector databases; use these as backup.
    - SOLID Principles: Modular, testable, and maintainable LLM components.

## Right-Sizing
    - Prompts:
        - Issues with "loss of focus" with larger context windows.
        - Excessive token consumption
    - Plugins:
        - Function definitions take up tokens in the context window.
    - Plans:
        - Plans also contribute to total prompt size.

## Token Management
    - Prompt Optimization: Techniques to optimize prompts for cost and efficiency.
        - Summarization: Using summarization plugins to reduce token usage.
        - Chat History Reducers: Streamlining context length to manage chat history effectively.
        - Caching Strategies: Use semantic caching to reduce the number of calls to the llm.
    - Constrained Decoding: Controlling LLM outputs for reliable and safe responses.
        - Logit Biasing: Guiding outputs to adhere to specific constraints.
        - JSON Schema: Enforcing structured outputs for consistent results.
        - Escape Sequences: Managing special characters and output formatting.
        - Token Limits:
    - Precalculating tokens with [Microsoft.ML.Tokenizers](https://github.com/microsoft/Tokenizer)

## Prompt Management
    - Prompts managed / stored remotely for continuous delivery.
    - Prefer variables over tool calling in templates (using Prompty)

## Isolation and Idempotency
    - Model key management: Ensuring appropriate scoping and usage limitations.
        - Per Application: Contigent on the complexity or simplicity of an application, it may be beneficial to manage usage limitations in aggregate. This trades off resource flexibility for management simplicity.
        - Per Kernel: When multiple kernels are configured with specific tools and model configurations, allocating separate keys for each kernel allows for variable usage limitations to be configured for kernels that are more chatty than others. This tradeoff provides greater resource flexibility and performance over management complexity.
    - Kernel Isolation: Limiting kernel access to essential resources only.
        - Per AI Service: When issuing requests across multiple user sessions, its important to not expose data. Each service instance should at least clone a kernel configuration. No two service instances should share mutable data.
        - Per Agent: Allocating a kernel per agent conforms to the OpenAI definition of an agent, limiting tools to what are accessible to the agent. Agents should only be able to invoke the capabilities that they're authorized to invoke.
    - Agent Isolation:
        - Per Process: Assigning each agent its own process for isolation.

## Resiliency and Availability
    - Resiliency pipelines: using polly and dotnet core resiliency pipelines to ensure continued operations.
    - Local models: Leveraging local slms for high availability.
    - Graceful degredation of services: routing to other models to ensure a smooth experience.

## Monitoring and Observability
    - Filters: Interception with prompt and function hooks.
    - App Insights: Integrations with Azure
    - Prompt Flow: Integrations with Azure Services
    - dotnet Aspire: Integrations with dotnet observability.

## Security and Responsible AI Practices
   - Identity and Access Management: Using function filters to enforce authorization for functions/plugins.
   - Responsible AI Guidelines:
     - Prompt Filtering: Screening prompts for ethical and safe usage.
     - Content Moderation: Monitoring outputs to adhere to responsible AI standards.

## Fine-Tuning and Optimization for Continuous Improvement
   - Reinforcement Learning through Human Feedback (RLHF): Gathering and using user feedback for model improvement.
   - Fine-Tuning Pipelines:
     - SLM Fine-Tuning: Establishing a pipeline to fine-tune based on semantic learning models.
     - Smart Routing: Leveraging IAIServiceSelector for model route hydration based on performance metrics.

## VII. Conclusion and Q&A
   - **Recap of Key Topics**: Summarizing the design principles, optimization strategies, and security considerations.
   - **Q&A**: Open floor for questions and final thoughts on building scalable LLM applications with Microsoft’s Semantic Kernel.
