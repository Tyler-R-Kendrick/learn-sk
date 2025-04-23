---
title: "Testing and Evaluation Strategies"
module: "01 - Prompt Engineering"
order: 7
tags:
  - prompts
duration: "15min"
marp: true
theme: default
paginate: true
doc-type: content

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

# Testing and Evaluation Strategies

## Prerequisites
- Complete Best Practices for Effective Prompts module.

## Learning Objectives
- Describe methods for evaluating and testing prompt performance.
- Implement automated evaluations and unit tests with gold standards.
- Apply consistency checks and iteration in prompt evaluation.

---

## Key Concepts
- Iterative evaluation procedures
- Automated prompt engineering metrics
- Unit tests and gold-standard answers
- Self-consistency and multiple sampling

This section covers robust procedures for evaluating, testing, and ensuring consistency in prompt engineering work. Effective evaluation is critical for both maintaining and improving large language model (LLM) performance.

---

## Evaluation Procedures

---

### Iterative Approach
Prompt engineering requires continual testing, analysis, and documentation of prompt iterations. Refine prompts based on comprehensive model performance analysis, and re-test as model configurations or architectures change.

---

### Documentation
Use a template to record each experiment, including prompt name/version, goal, model and its configuration, prompt text, output, and performance feedback. This process makes it easier to compare changes, revisit work, and debug future errors.

- Document fields such as iteration, status (OK/NOT OK/SOMETIMES OK), and qualitative feedback.
- Ideally, link prompts stored in systems like Vertex AI Studio for fast re-testing.

---

### Separation of Concerns
Store prompts separately from code within the codebase for maintainability and transparency.

### Special Considerations for RAG Systems
For retrieval-augmented generation, log details about inserted content, such as queries, chunk settings, and outputs that affect prompt content.

---

## Automated Evaluations

---

### Automatic Prompt Engineering
- Use LLMs themselves to generate and iterate on prompt variants.
- Evaluate candidates using metrics like BLEU or ROUGE to score for semantic similarity or task-based accuracy.
- Select and, if needed, further tweak the best-performing prompt(s).
- This process can drastically speed up prompt development and optimization.

---

## Unit Tests and Gold-Standard Answers

---

### Unit Testing
- For prompts with deterministic outputs (like code generation or mathematical solutions), manually create test cases with gold-standard (correct) answers.
- Re-run these systematically after any model or prompt changes.

---

### Consistency Checks
- If using approaches like Chain of Thought or Self-Consistency, extract the final answer separately from the reasoning to enable comparison with gold-standard outputs and facilitate automated grading.
- Chain of Thought and self-consistency prompting are best with temperature set to 0, maximizing determinism for extraction and validation.

---

### Multiple Sampling
- Use self-consistency checks: run the same prompt multiple times and analyze the spread of answers.
- For complex reasoning, select the most common answer or average the results for more robust outputs.

---

## Best Practices

- **Document everything:** Track all prompt experiments, even failed ones.
- **Automate where possible:** Leverage LLMs and scripts for efficiency in creating, scoring, and selecting prompts.
- **Systematic Testing:** Implement unit tests and regression tests around gold standards for mission-critical tasks.
- **Consistency is key:** Run prompts multiple times, across different models/versions, to establish reliability and minimize unexpected variation.
