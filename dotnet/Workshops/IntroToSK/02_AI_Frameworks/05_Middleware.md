---
title: "Middleware"
module: "02 - AI Frameworks"
doc-type: lab
order: 5
tags:
  - middleware
  - observability
  - filters
duration: "15min"

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

# Middleware

## Prerequisites
- Familiarity with .NET pipeline and middleware patterns.
- Understanding of observability and logging basics.

## Learning Objectives
- Explain the role of middleware in LLM application pipelines.
- Configure observability middleware for telemetry and logging.
- Implement and register SK filters for input/output processing.

---

## Summary
Middleware components allow cross-cutting features such as logging, monitoring, and data transformation to be applied consistently across AI workflows.

---

## Key Concepts
- Observability
- SK Filters


---

### Observability
- Telemetry collection with Application Insights or other providers
- Logging request/response payloads and metrics

---

### SK Filters
- Interface definitions for filters (IFunctionFilter, IPromptFilter, and IAutoFunctionInvocationFilter)
- Common filter use cases (sanitization, masking, enrichment)

---

## Implementation Guidelines
- Define custom filter classes implementing the SK filter interfaces.
- Configure logging providers and telemetry exporters via DI.

---

## Best Practices
- Keep filters stateless and idempotent.
- Avoid logging sensitive data; implement masking filters when necessary.
- Use health checks and metrics to monitor middleware impact.

---

## Challenge / Hands‑On Exercise
Create and register a logging filter that records prompt inputs and model outputs to Azure Application Insights.
