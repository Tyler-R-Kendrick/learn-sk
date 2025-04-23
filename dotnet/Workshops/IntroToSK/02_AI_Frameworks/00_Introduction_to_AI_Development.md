---
title: "Introduction to AI Development in .NET"
module: "02 - AI Frameworks"
doc-type: intro
order: 0
tags:
  - AI
  - .NET
  - Introduction
duration: "15min"

marp: true
theme: gaia
paginate: true
header: AI Frameworks - Introduction to AI Development
footer: "© Microsoft Corporation. All rights reserved."
style: |
  @import '../styles/msft.css';

---

<!-- _class: 'title-slide' -->
<!-- _paginate: skip -->

# Introduction to AI Development in .NET

## Prerequisites

- Prior knowledge of C# and basic AI concepts.
- Familiarity with dotnet project setup and NuGet package management.

## Learning Objectives

- Understand the key components of AI development workflows in dotnet.
- Describe prompt execution and evaluation mechanisms in common dotnet frameworks.
- Identify tools for function invocation and memory management.
- Recognize middleware and SK-specific frameworks used in AI applications.

---

## Summary

This section introduces the core concepts and workflows for developing AI applications in .NET. Readers will learn about prompt execution, evaluations, tooling, memory strategies, middleware, and SK-specific frameworks to build robust AI solutions.

---

<!-- _class: 'toc' -->

## Table-of-Contents

| | Topic | Summary |
|--------------------------|---------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------|
| 1 | [Prompts & Prompt Execution](01_Prompts_and_Prompt_Execution.md) | Explore prompt templates (Prompty, Handlebars, Fluid) and structured execution via SK Kernel and IChatClient. |
| 2 | [Evaluations & Tests](02_Evaluations_and_Tests.md) | Automate testing and assess AI model outputs using the M.E.AI Evaluations SDK. |
| 3 | [Tools & Function Invocation](03_Tools_and_Function_Invocation.md) | Integrate external functions via SK KernelFunctions, plugins, and M.E.AI AITools. |
| 4 | [Memory](04_Memory.md) | Manage context with kernel memory, SK Vector Memory, and Microsoft.Extensions.VectorData. |
| 5 | [Middleware](05_Middleware.md) | Monitor and modify AI requests and responses using observability tools and SK filters. |
| 6 | [SK Agent Framework](06_SK_Agent_Framework.md) | Orchestrate AI workflows with the SK Agent Framework. |
| 7 | [Process Framework](07_Process_Framework.md) | Define and execute multi-step processes in AI applications. |
| 8 | [Text Search](08_Text_Search.md) | Implement text search capabilities for context retrieval within AI workflows. |

---

## Workshop Content

1. Navigate to [github/learn-sk]([https://github.com/Tyler-R-Kendrick/learn-sk) and pull down the repo locally or run ``` gh repo clone Tyler-R-Kendrick/learn-sk ``` with the GitHub CLI.
1. Follow the instructions in the README.md file to setup the project.
1. Execute each of the projects in the ```dotnet/DemoApp/Solutions``` folder and read each section.
