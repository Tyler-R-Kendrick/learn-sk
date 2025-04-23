---
title: "Memory Management"
module: "02 - AI Frameworks"
doc-type: lab
order: 4
tags:
  - memory
  - vector-storage
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

# Memory Management

## Prerequisites
- Understanding of embedding generation and similarity search.
- Familiarity with memory persistence concepts in AI workflows.

## Learning Objectives
- Differentiate between Kernel Memory, SK Vector Memory, and Microsoft.Extensions.VectorData.
- Configure and use a vector store for embedding and retrieval.
- Integrate memory persistence into chat workflows.

---

## Summary
Covers how to persist and retrieve context in AI applications using in‑process and external memory stores.

---

## Key Concepts
- Chat History
- SK Vector Memory
- Microsoft.Extensions.VectorData


---

### Chat History

- IChatHistory
- IChatHistoryReducer

---

### Kernel Memory

- IKernelMemory abstraction
- In‑memory vs durable stores

---

### SK Vector Memory
- Embedding models and stores
- Storage options: in‑memory, Azure Cosmos DB, Redis

---

### Microsoft.Extensions.VectorData
- Configuration and DI support
- Extensible providers and adapters

---

**Implementation Guidelines**
- Register and configure a VectorMemoryStore in DI.
- Ingest documents and use memory recall.

---

**Best Practices**
- Normalize and chunk inputs for embeddings.
- Manage embedding model costs and latency.
- Implement cleanup policies for stale data.
