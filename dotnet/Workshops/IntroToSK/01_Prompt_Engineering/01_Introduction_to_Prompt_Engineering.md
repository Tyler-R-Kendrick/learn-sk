---
title: "Introduction to Prompt Engineering"
doc-type: intro
module: "01 - Prompt Engineering"
order: 1
tags:
  - prompts
  - sdk

marp: true
theme: default
paginate: true
layout: title

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

# 1. Introduction to Prompt Engineering

- **Definition**: Crafting effective prompts to guide AI models toward optimal outputs.
- **Importance**:
  - Enhances AI performance through clear instructions and context.
  - Ensures accurate, relevant, and safe AI interactions.
  - Improves control and predictability of AI responses.

---

## Prerequisites
- Access to an AI language model supported by Azure Inference Services, OpenAI, or OLLAMA.
- Visual Studio Code or Codespaces.
- AI Toolkit VS Code Extension ([ms-windows-ai-studio.windows-ai-studio](vscode:extension/ms-windows-ai-studio.windows-ai-studio)).

---

**Learning Objectives**
- Define prompt engineering and explain its significance in enterprise AI scenarios.
- Identify the key components and types of prompts.
- Structure prompts for clarity, safety, and effectiveness.
- Optimize prompts using measurable rubrics and evaluation metrics.

---

## Summary

This module on prompt engineering is designed for developers working in enterprise environments who want to master prompt engineering for AI systems. By the end of this section, you will be able to:
- Define prompt engineering and explain its significance in enterprise AI applications.
- Identify the key components and types of prompts.
- Learn how to structure prompts for clarity, safety, and effectiveness.
- Optimize prompts using measurable rubrics - whilst explaining your approach.
- Understand how to leverage tools like Semantic Kernel, Microsoft.Extensions.AI, and Prompty for prompt development and evaluation.

Throughout the workshop, you will use Semantic Kernel and Prompty to design, test, and optimize prompts against measurable rubrics.

---

## Table of Contents

| # | Module | Time | Summary |
|---|--------|------|---------|
| 1 | [Introduction to Prompt Engineering](./01_Introduction_to_Prompt_Engineering.md) | 15min | Core concepts, importance, and objectives. |
| 2 | [Types of Prompts](./02_Types_of_Prompts.md) | 15min | Overview of prompt types. |
| 3 | [Anatomy of an Effective Prompt](./03_Anatomy_of_an_Effective_Prompt.md) | 15min | Required and optional prompt components. |
| 4 | [Prompt Engineering Techniques](./04_Prompt_Engineering_Techniques.md) | 30min | Basic and advanced techniques, parameter tuning. |
| 5 | [Practical Use Cases and Examples](./05_Practical_Use_Cases_and_Examples.md) | 20min | Real-world applications across scenarios. |
| 6 | [Best Practices for Effective Prompts](./06_Best_Practices_for_Effective_Prompts.md) | 15min | Guidelines for clarity, context, and iteration. |
| 7 | [Testing and Evaluation Strategies](./07_Testing_and_Evaluation_Strategies.md) | 15min | Methods for evaluating prompt quality. |
| 8 | [Safety, Ethics, and Fallback Responses](./08_Safety_Ethics_and_Fallback_Responses.md) | 20min | Handling sensitive content and fallback. |
| 9 | [Resources for Further Learning](./09_Resources_for_Further_Learning.md) | 10min | Documentation and training resources. |

--- 

### Install the AI Toolkit VS Code Extension

To install the "[ms-windows-ai-studio.windows-ai-studio](vscode:extension/ms-windows-ai-studio.windows-ai-studio)" extension in VSCode using the CLI, follow these steps:

1. Open a terminal and run the following command to install the extension:

  ```bash
  code --install-extension ms-windows-ai-studio.windows-ai-studio
  ```

2. Ensure that the extension is successfully installed by running:

  ```bash
  code --list-extensions | grep ms-windows-ai-studio.windows-ai-studio
  ```

Alternatively, you can open this repository in a dev container and install the extension using the same command inside the container's terminal.
