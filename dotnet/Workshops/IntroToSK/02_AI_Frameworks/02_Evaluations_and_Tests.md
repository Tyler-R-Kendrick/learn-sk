---
title: "Evaluations and Tests"
module: "02 - AI Frameworks"
order: 2
doc-type: lab

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

# Evaluations and Tests

## Prerequisites
- Working knowledge of C# and .NET testing frameworks.
- Basic understanding of LLM prompting and evaluation metrics.

## Learning Objectives
- Use M.E.AI Evaluations SDK for LLM output assessments.
- Design evaluation metrics and assertion strategies.
- Automate test cases to validate prompt quality.

---

## Summary

Overview of evaluation frameworks and testing strategies to ensure LLM application reliability.

---

## Instructions

Follow the instructions for the [LLM-Eval](https://learn.microsoft.com/en-us/dotnet/ai/tutorials/llm-eval) practice project at: https://learn.microsoft.com/en-us/dotnet/ai/tutorials/llm-eval

---

## Key Concepts

## M.E.AI Evaluations SDK
- Setup and configuration
- Defining evaluation tasks
- Scoring and reporting

## Evaluation Metrics
- Precision, recall, F1
- Human-in-the-loop vs automated scoring

---

### Hands‑On Exercise
- Integrate Evaluations SDK into .NET test projects.
- Write tests for existing prompt execution. 
- Execute batch evaluations and collect results.

---

### Best Practices
- Write clear test cases with edge scenarios.
- Use CI pipelines to automate evaluations.
- Log detailed reports for analysis.
