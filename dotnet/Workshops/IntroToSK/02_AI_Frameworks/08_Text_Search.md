---
title: "Text Search"
module: "02 - AI Frameworks"
doc-type: lab
order: 8
tags:
  - text-search
  - search
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

# Text Search

## Prerequisites
- Familiarity with embedding-based search concepts.
- Experience with vector stores and .NET DI configuration.

---

## Learning Objectives
- Differentiate semantic search from traditional keyword search.
- Implement document retrieval using SK vector memory and external indices.
- Configure and execute search queries in .NET applications.
- Apply fallback strategies for robust search results.

---

## Summary
Covers how to integrate text search capabilities into AI workflows using Semantic Kernel and vector stores for high‑quality document retrieval.

---

## Key Concepts

### Semantic Search
- Embedding generation and similarity measures
- Vector index structures (e.g., HNSW)

### Lexical (Keyword) Search
- Inverted index basics
- Tradeoffs between speed and relevance

### Fallback Strategies
- Combining semantic and lexical scores
- Thresholding and hybrid query patterns

---

## Implementation Guidelines
- Configure a vector store (e.g., Azure Cognitive Search, Redis) in SK via DI.
- Populate the index with document embeddings and metadata.
- Construct and execute search requests using `kernel.Memory.SearchAsync()`.
- Handle pagination and result ranking programmatically.

---

## Best Practices
- Preprocess and clean text before embedding (tokenization, stop‑word removal).
- Balance recall and precision via tuning of similarity thresholds.
- Monitor index performance and costs; shard or partition large indexes.

---

## Challenge / Hands‑On Exercise
Build a console app that indexes a set of markdown files, then prompts the user for a query and displays the top 5 semantically relevant document snippets.
