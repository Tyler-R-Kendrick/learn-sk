---
doc-type: lab
marp: true
theme: gaia
paginate: true

header: Introduction to Prompt Engineering
footer: "© Microsoft Corporation. All rights reserved."
style: |
  @import '../styles/msft.css';

---

<!-- _class: 'title-slide' -->
<!-- _paginate: skip -->

# Prompts and Prompt Execution

## Prerequisites
- Prior experience with C# and .NET AI development.
- Familiarity with prompt engineering and Semantic Kernel basics.

## Learning Objectives
- Identify different prompt template engine options (Prompty, Handlebars, Fluid).
- Execute prompts using Semantic Kernel and M.E.AI IChatClient.
- Generate and parse structured outputs.

---

## Summary

Provide an overview of prompt composition and execution in .NET using SK and M.E.AI.

**Background/Context**
Discuss fundamentals of prompts, template rendering, and their role in LLM applications.

---

## Key Concepts

---

## Prompt Templates
- [SK Templates](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/prompt-template-syntax): Built in template syntax for prompt in Semantic Kernel. Allows for more descriptive inputs/outputs than most other alternatives.
- [Prompty](https://prompty.ai/docs/getting-started/concepts): A Microsoft template standard for prompting with a mature tooling ecosystem.
- [Yaml](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/yaml-schema): A common markup format
- [Handlebars](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/handlebars-prompt-templates?pivots=programming-language-csharp): Allows more natural substitution of template inputs.
- [Liquid](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/liquid-prompt-templates): Uses the liquid templating engine to do substitution.

---

## SK Kernel Prompt Execution
- [Kernel prompt execution](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/prompt-execution): Kernel setup and prompt configuration.
- Context variables and chaining prompts

---

## M.E.AI IChatClient Execution
- [IChatClient API](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.ai.ichatclient?view=azure-dotnet): Initialize and use the IChatClient for structured prompts and structured output definitions.
- Structured output parsing and validation

---

## Implementation Guidelines
- Create a console app.
- Install Microsoft.Extensions.AI & Semantic Kernel.
- Load and execute an SK prompt, using the SK Kernel.
- Load and execute a string as a prompt using an IChatClient.

---

## Best Practices
- Use parameterized templates to avoid injection risks.
- Validate template syntax at build time.
- Implement retry and timeout on prompt execution.

---

## Challenge / Hands‑On Exercise

Create a YAML Prompt, a Prompty Prompt, a Liquid prompt, and/or a Handlebar prompt that output structured json.

---

## References & Links
- [SK Prompt Template Syntax](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/prompt-template-syntax)
- [Prompty Concepts](https://prompty.ai/docs/getting-started/concepts)
- [YAML Prompt Schema](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/yaml-schema)
- [Handlebars Prompt Templates](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/handlebars-prompt-templates?pivots=programming-language-csharp)
- [Liquid Prompt Templates](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/liquid-prompt-templates)
- [SK Prompt Execution Overview](https://learn.microsoft.com/en-us/semantic-kernel/concepts/prompts/prompt-execution)
- [M.E.AI IChatClient API](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.ai.ichatclient?view=azure-dotnet)
