---
title: "Safety, Ethics, and Fallback Responses"
module: "01 - Prompt Engineering"
order: 8
tags:
  - prompts
duration: "20min"
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

# Safety, Ethics, and Fallback Responses

## Prerequisites

- Complete Testing and Evaluation Strategies module.

## Learning Objectives

- Explain key safety and ethical considerations in prompt design.
- Implement and evaluate fallback mechanisms for sensitive content.

---

## Key Concepts

- Positive instructions vs constraints
- Fallback prompts and explicit responses
- Self-consistency prompting for reliability
- System and role-based safety directives

---

## Addressing Sensitive Content
Prompt engineering for large language models (LLMs) must account for safety and ethics, especially when handling sensitive content. A key best practice is using positive instructions in prompts instead of negative constraints. For instance, instead of saying “Do not return toxic or biased content,” it’s usually more effective to explicitly instruct the LLM: “You should provide respectful, unbiased, and safe answers.” This direct instruction sets clear expectations while fostering creative, compliant output within desired boundaries. Constraints (explicit “do not…” statements) should be reserved only for cases where strict limits are required, such as to explicitly avoid legally or ethically sensitive output or to enforce tight output structures for safety or clarity reasons.

---

### Best Practices for Addressing Sensitive Content with LLMs

- **Use positive instructions:** Direct the model to produce safe and respectful outputs.
- **Apply constraints where necessary:** Only add negative (“do not…”) constraints for topics where explicit boundaries are essential.
- **Combine instructions and constraints strategically:** Start with clear positive instructions and include constraints only if needed for heightened safety or compliance.
- **Iterate and document results:** Experiment with different prompt formulations, review outputs, and iterate based on results, always documenting what works and what does not.

---

## Fallback Mechanisms

Fallback mechanisms are essential to ensure LLMs provide safe and appropriate responses, especially in the presence of sensitive or ambiguous prompts:

- **Explicit Fallback Instructions:** Prompts can include specific fallback instructions, such as: “If the topic is sensitive or potentially harmful, respond with: ‘I am unable to provide a response to this request.’”
- **Self-consistency prompting:** By running the same prompt multiple times and aggregating the most common response, you can reduce the risk of unsafe or unethical outputs and improve reliability. This technique is particularly effective in ambiguous or “edge case” scenarios where LLMs’ responses might be variable.
- **Use of roles and system prompting:** By combining system prompts that set strong boundaries (e.g., “Always prioritize user safety over completeness in your answers”) with role prompts (e.g., responding as an ethicist or compliance officer), the likelihood of harmful content is reduced.
- **Token Limits and Output Formatting:** Strictly limiting output length and enforcing structured formats (e.g., JSON schemas with clear fields for allowed responses) can prevent the generation of unpredictable or unsafe outputs.

---

### Example Fallback Prompt

> You are an AI assistant. Provide clear, helpful, and safe answers. If you are asked for sensitive, harmful, or inappropriate content, reply: “Sorry, I can’t help with that request.”

Or, with context:

> If the user query contains medical, legal, or otherwise sensitive content, respond only with “I cannot provide information on this topic.”
