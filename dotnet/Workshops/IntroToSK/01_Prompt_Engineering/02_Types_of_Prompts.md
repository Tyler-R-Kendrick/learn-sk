---
title: "Types of Prompts"
doc-type: content
module: "01 - Prompt Engineering"
order: 2
tags:
  - prompts

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

# Types of Prompts

## Prerequisites
- Understand core principles from the Introduction to Prompt Engineering module.

## Learning Objectives
- Describe different prompt types and their use cases.

---

## Key Concepts
- Zero-shot prompts
- Few-shot and multi-shot prompts
- Chain of Thought prompts
- Zero-shot Chain of Thought prompts

Prompts influence how an AI, like ChatGPT, understands and responds to your request. Mastering prompt types will help you achieve more accurate and useful results.

--- 

### Zero-shot (Direct)

**Definition:** Provide the AI with a direct instruction—no examples are given.  
**Purpose:** Test the AI’s general capability to follow instructions.  
**Example:**
> Translate the following sentence to French: "Where is the library?"

**Use when:** You want a quick, straightforward answer or to test the model’s basic abilities.

--- 

### Few-shot and Multi-shot
**Definition:** Add one (few-shot) or several (multi-shot) examples in your prompt to guide the AI’s response.  
**Purpose:** Demonstrate the desired format, style, or logic with concrete examples.

**Few-shot Example:**
```
Q: What is the capital of France?
A: Paris
Q: What is the capital of Italy?
A: Rome
Q: What is the capital of Germany?
A:
```

**Multi-shot Example:** (More than two examples)
```
Translate the following:
English: Hello | French: Bonjour
English: Good morning | French: Bonjour
English: Good night | French:
```

**Use when:** The task requires specific output style or handling uncommon instructions.

--- 

### Chain of Thought (CoT)

**Definition:** Ask the model to display its reasoning step-by-step.  
**Purpose:** Promote transparency and accuracy in complex or multi-step problems.

**Example:**
```
Q: If there are 3 red balls and 2 blue balls in a box, and you take out one ball at random, what is the probability it is red? Explain your reasoning.
A: There are 3 red balls and 2 blue balls, so a total of 5 balls. The probability of drawing a red ball is 3 out of 5, or 3/5.
```

**Use when:** The task involves logical or mathematical reasoning that benefits from breaking down into steps.

---

### Zero-shot Chain of Thought (Zero-shot CoT)

**Definition:** Directly ask the model to reason step-by-step without providing examples.  
**Purpose:** Encourages explicit reasoning without prior samples.

**Example:**
> What is 27 times 16? Please think step by step.

(AI might respond with intermediate calculation steps.)

**Use when:** You want the model to engage in reasoning but haven’t supplied examples.

---

### Tree of Thought

**Definition:** A prompt structure that encourages the model to explore multiple reasoning paths or solutions in a branching, tree-like manner. Each branch represents a different line of reasoning or possible answer, allowing the model to backtrack and compare alternatives.

**Purpose:** Useful for complex problem-solving where evaluating multiple options or strategies is beneficial.

**Example:**
> You are solving a puzzle. At each step, list all possible moves and their consequences. Then, choose the most promising path and continue. If you reach a dead end, backtrack and try another branch.

(AI might respond by outlining several possible moves at each step, evaluating them, and selecting the best one.)

**Use when:** The task involves decision-making, planning, or problems with multiple possible solutions that benefit from exploring alternatives.

---

### Graph of Thought

**Definition:** A prompt structure that allows the model to reason using a network or graph, where ideas, facts, or reasoning steps are represented as nodes connected by relationships (edges). This enables the model to consider dependencies and interactions between different concepts.

**Purpose:** Useful for tasks that require mapping relationships, integrating information from multiple sources, or handling non-linear reasoning.

**Example:**
> Map out the relationships between climate change, renewable energy, and economic growth. Show how each concept influences the others, and identify feedback loops.

(AI might respond with a diagram or description of nodes and connections, explaining how each factor affects the others.)

**Use when:** The task involves complex interdependencies, systems thinking, or integrating information from various domains.

---

### Sketch of Thought

**Definition:** A prompt style that encourages the model to quickly outline or sketch key ideas, steps, or components before elaborating in detail. This can take the form of bullet points, diagrams, or high-level summaries.

**Purpose:** Helps organize thinking, clarify structure, and ensure all important aspects are considered before deep reasoning.

**Example:**
> Before writing a detailed essay on the impact of AI in healthcare, list the main points and arguments you plan to cover as a quick outline.

(AI might respond with a bulleted list of key arguments or a rough structure for the essay.)

**Use when:** You want to brainstorm, plan, or structure a response before generating detailed content.
