# Regex Validator

The regex validator provides **strict and defensive e-mail validation**.

It validates both the **local-part** and the **domain** using compiled, culture-invariant regular expressions with a configurable timeout.

---

## What it validates

* RFC-oriented local-part syntax
* Domain format
* IPv4 literal domains
* IPv6 literal domains
* Malicious patterns (CRLF injection, malformed segments)

---

## Timeout protection

To prevent regex-based denial-of-service attacks, validation is executed with a timeout.

```csharp
RegexMailValidator.Validate(
    email: "user@example.com",
    timeoutMS: 100
);
````

If the timeout is exceeded, validation fails explicitly.

---

## Usage

```csharp
RegexMailValidator.Validate("user@example.com");
```

---

## When to use

Recommended when:

* E-mails are a critical system input
* Security and correctness matter
* Input is exposed to untrusted sources

This validator does **not** verify domain existence or mail acceptance.
