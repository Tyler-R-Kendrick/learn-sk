---
title: "Tools and Function Invocation"
module: "02 - AI Frameworks"
doc-type: lab
tags:
  - tools
  - functions
duration: "20min"
marp: true
theme: default
paginate: true

header: Introduction to Prompt Engineering
footer: "© Microsoft Corporation. All rights reserved."
style: |
  @import '../styles/msft.css';

  section {
    overflow-y: scroll;
  }


---

<!-- _class: 'title-slide' -->
<!-- _paginate: skip -->

# Tools and Function Invocation

## Prerequisites
- Working knowledge of C# and Semantic Kernel basics.
- Familiarity with function invocation patterns and plugin architecture.

## Learning Objectives
- Understand SK KernelFunctions & Plugins (Semantic and Native Functions).
- Use M.E.AI AITools for function-based interactions.
- Configure and invoke custom tool integrations.

---

## Summary
Covers how to extend LLM applications by integrating external tools and functions for richer capabilities.

---

## Key Concepts

---

## M.E.AI AITools
- Generic, unopinionated tools for converting delegates to AITools.

## SK KernelFunctions & Plugins
- Semantic Functions: declarative prompt-driven blocks.
- Native Functions: code-based extensions.
- Plugin registration and discovery.
- Attribute based discovery.
- Microsoft.SemanticKernel.Extensions.ApiManifest

---

## Implementation Guidelines
- Register functions in Semantic Kernel builder with Attributes.
- Import an Api Manifest as a toolset.
- Convert to AITools with an associated chat client.
- Invoke tools with auto function invocation from an IChatClient.

---

## Challenge / Hands-on Exercise
- Use ModelContextProtocol dotnet to register tools for either SK or Microsoft.Extensions.AI.

---

## Best Practices
- Limit tool surface area to required capabilities.
- Use secure storage for API keys and secrets.
- Validate inputs/outputs of tool calls.
