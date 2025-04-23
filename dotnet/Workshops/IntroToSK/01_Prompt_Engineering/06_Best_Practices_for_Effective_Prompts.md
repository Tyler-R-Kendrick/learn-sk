---
title: "Best Practices for Effective Prompts"
module: "01 - Prompt Engineering"
order: 6
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

# Best Practices for Effective Prompts

## Prerequisites
- Complete Practical Use Cases and Examples module.

## Learning Objectives
- Implement and evaluate best practices for prompt clarity and precision.
- Apply iterative prompt refinement strategies.

---

## Key Concepts

- Define clear objectives
- Provide context and background
- Demonstrate with examples
- Iterate and experiment
- Emphasize positive instructions
- Consider information order
- Offer alternative paths
- Optimize token usage

---

## Define Clear Objectives

- **Be explicit about your goal:** Clearly state what you expect the model to do. For example, specify if you want a summary, a classification, a code snippet, or a creative story.
- **State the role and output requirements:** Assigning a role (e.g., “Act as a travel guide...”) or defining the format helps the model align with your expectations.

---

## Provide Context and Background

- **Set the scene:** Give the model relevant information, background, or context necessary for the task. Examples include specifics about the audience, prior conversation, or relevant facts.
- **Use system, contextual, and role prompting:**
  - *System prompts* define big-picture objectives.
  - *Contextual prompts* supply situation-specific input.
  - *Role prompts* assign an identity or perspective to the model.

---

## Demonstrate with Examples
- **Show what you want:** Include one-shot or few-shot examples relevant to your task.
- **Edge cases:** Use diverse, high-quality examples (including edge cases) to increase robustness.

---

## Be Precise and Descriptive
- **Clarity beats cleverness:** Use simple, direct language.
- **Describe the desired output:** Specify structure, style, and length (e.g., "Return the result in JSON", "Write three paragraphs in a conversational style").

---

## Iterate and Experiment
- **Prompt engineering is iterative:** Test, analyze, and revise your prompts based on model output.
- **Experiment with format and style:** Try inputting the task as a question, statement, or direct instruction to observe variations in results.
- **Document all attempts:** Track prompt iterations and results for learning and optimization.

---

## Emphasize Critical Instructions
- **Use instructions over constraints:** Tell the model what to do rather than what not to do (positive instructions are generally more effective).
- **Reserve constraints** for strict requirements or avoiding harm.

---

## Consider Information Order
- **Order matters:** Arrange information logically, leading with what’s most important and providing details sequentially.
- **For classification/few-shot prompts:** Mix up the order of example classes to avoid overfitting to sequence.

---

## Provide Alternative Paths
- **Offer fallback options:** If there are multiple acceptable response types, clarify priorities or give instructions for alternatives.
- **Structured output:** For complex outputs, consider using schemas or structured formats like JSON.

---

## Optimize Token Usage
- **Max token control:** Limit response length using configuration or by explicitly requesting brevity (e.g., “Explain quantum physics in a tweet.”).
- **Use variables:** Use placeholders for reusable parts of prompts, increasing flexibility for integration in automated systems.
- **Efficiency and cost:** Shorter prompts and controlled output save computation resources and reduce model costs.
