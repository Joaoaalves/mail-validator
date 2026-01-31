# Introduction

**Joaoaalves.MailValidator** is a lightweight .NET library that provides **explicit and composable e-mail validation primitives**.

It focuses on validating e-mail addresses at different levels of strictness — from basic syntax checks to **RFC-oriented regex validation** and **DNS MX record verification** — without introducing application or infrastructure constraints.

It is **not a framework**.
Joaoaalves.MailValidator does not enforce validation flows, dependency injection patterns, or error-handling strategies. Instead, it exposes **small, deterministic validators** that can be composed according to the needs of each system.

The primary goal of the library is to provide **safe, predictable, and configurable e-mail validation**, avoiding false positives while protecting systems from malformed or malicious inputs.

Joaoaalves.MailValidator is designed to support systems that require:

* Reliable user input validation
* Defensive boundaries against malformed e-mails
* Configurable trade-offs between performance and strictness
* Explicit failure semantics via exceptions

---

## Design Intent

Joaoaalves.MailValidator is intentionally **small, explicit, and validation-focused**.

The library provides **no hidden behavior** and no implicit validation pipelines. Each validator performs a single responsibility and fails explicitly via well-defined exceptions.

Key design principles:

* No runtime magic
* No reflection-based behavior
* No background verification (SMTP, inbox probing, etc.)
* No deliverability guarantees
* No coupling to application or infrastructure layers

Validation is **synchronous, deterministic, and explicit**.
If a validation passes, it means the rule was satisfied — nothing more, nothing less.

---

## Scope

Joaoaalves.MailValidator provides:

* A **basic built-in validator** based on `System.Net.Mail.MailAddress`
* A **strict Regex-based validator** covering:

  * Local-part validation
  * Domain validation
  * IPv4 and IPv6 literal domains
  * Protection against malicious inputs (CRLF, malformed parts)
  * Configurable regex timeout
* An **MX record validator** using DNS queries to verify domain mail acceptance
* A **composed base validator** that safely chains multiple strategies
* Explicit exception types for failure scenarios:

  * Invalid e-mail
  * Invalid domain
  * Invalid local-part (username)

Out of scope by design:

* SMTP-level validation
* Inbox existence checks
* Deliverability guarantees
* Temporary or disposable e-mail detection
* Asynchronous or background validation
* Infrastructure concerns (logging, retries, caching)

These concerns are intentionally left to higher-level application or infrastructure layers.

---

## Architectural Context

Joaoaalves.MailValidator fits naturally into architectures where:

* Input validation is treated as a **first-class boundary**
* Failure modes must be **explicit and deterministic**
* Validation strictness varies by use case
* Security and correctness matter more than silent acceptance

It can be used as:

* A validation layer for user registration flows
* A defensive boundary in APIs and gateways
* A shared validation component in modular monoliths
* A reusable utility in microservices

Joaoaalves.MailValidator makes **no assumptions** about where or how validation errors are handled — only how validation rules are applied.

---

## Package Overview

The package is organized around clear validation responsibilities:

* **Abstractions**

  * `IMailValidator` contract

* **Validators**

  * `BuiltInMailValidator`

    * Fast, basic validation using .NET built-in parsing
    * Recommended only for non-critical scenarios
  * `RegexMailValidator`

    * Strict RFC-oriented validation
    * Validates local-part, domain, IPv4 and IPv6 literals
    * Configurable timeout to prevent regex abuse
  * `MxMailValidator`

    * DNS-based MX record validation
    * Ensures the domain is capable of receiving e-mail
    * Does not guarantee deliverability or account existence
  * `BaseMailValidator`

    * Safe composition of built-in, regex, and MX validators
    * Allows enabling/disabling strategies explicitly

* **Exceptions**

  * Explicit exception types representing validation failures

