---
title: "SK Agent Framework"
module: "02 - AI Frameworks"
doc-type: lab
order: 6
tags:
  - agent-framework
  - sk-Agents
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

# SK Agent Framework

## Prerequisites
- Knowledge of .NET AI application development and Semantic Kernel basics.
- Familiarity with AI agent concepts, skills, and plugin architectures.

---

## Learning Objectives
- Understand the architecture and components of the SK Agent Framework.
- Configure and instantiate agents with skills and plugins.
- Build and run an autonomous agent workflow.

---

## Summary
Explore how to use Semantic Kernel’s Agent Framework to create autonomous, goal-driven AI agents that coordinate skills and functions.

---

## Key Concepts

### Agents and Planners
- Planner types (e.g., backward, forward chaining)
- Agent orchestration loop

### Skills and Plugins
- Semantic Functions as skills
- Native Functions and tool integration

### Execution Context
- Per-agent state
- Cancellation and error handling

---

## Implementation Guidelines
- Create an Agent to answer questions.
- Create an Agent to check answers.
- Create a group chat to find a correct solution with 3 maximum iterations.

---

## Best Practices
- Limit agent goals to maintain control and predictability.
- Securely manage credentials for external tools.
- Monitor and log agent actions for troubleshooting.
- Use a single kernel per agent.
