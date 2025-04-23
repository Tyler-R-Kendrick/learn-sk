---
title: "Process Framework"
module: "02 - AI Frameworks"
doc-type: lab
order: 7
tags:
  - process-framework
  - orchestration
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

# Process Framework

## Prerequisites
- Familiarity with Semantic Kernel Process Framework basics in .NET.
- Understanding of workflow orchestration and error handling patterns.

## Learning Objectives
- Understand the Semantic Kernel Process Framework architecture.
- Define and orchestrate multi-step processes using workflows.
- Handle errors and cancellations in process execution.

---

## Summary
Covers building structured workflows in AI applications with the Semantic Kernel Process Framework to manage complex, multi-step logic.

---

## Key Concepts

---

### Process Definition
- Defining steps and transitions in JSON or code
- Parallel vs sequential process flows

---

### Workflow Execution
- Kernel.ProcessBuilder APIs
- Cancellation tokens and error propagation

---

### Monitoring and Control
- Tracking process state
- Logging step outputs and metrics

---

## Implementation Guidelines
- Create a process that runs your answer validation agent after your question answering agent provides a response, instead of using an Agent Group Chat.
- After a valid response, have the process workflow cache the response.

---

**Best Practices**
- Keep steps small and focused.
- Implement retry policies for transient failures.
- Validate input schemas before execution.
