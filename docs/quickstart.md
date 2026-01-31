# Quickstart

Joaoaalves.MailValidator is designed to be **simple by default** and **explicit when needed**.
You can choose between fast validation, strict validation, or a fully composed strategy depending on your requirements.

---

## Basic Validation (Built-in)

For non-critical scenarios where performance matters more than strictness, you can use the built-in validator.

```csharp
using Joaoaalves.MailValidator.Validators;

BuiltInMailValidator.Validate("user@example.com");
````

This validation checks basic e-mail syntax using .NET built-in parsing.
It does **not** validate the domain or protect against advanced malformed inputs.

---

## Strict Validation (Regex)

For systems where e-mails are a critical input, the regex validator provides strict validation of:

* Local-part
* Domain
* IPv4 and IPv6 literal domains
* Malicious input patterns

```csharp
using Joaoaalves.MailValidator.Validators;

RegexMailValidator.Validate("user@example.com");
```

You can also control the regex timeout explicitly:

```csharp
RegexMailValidator.Validate(
    email: "user@example.com",
    timeoutMS: 100
);
```

---

## Domain Validation (MX Records)

To ensure the e-mail domain is capable of receiving messages, you can validate its MX records.

```csharp
using Joaoaalves.MailValidator.Validators;

MxMailValidator.Validate("user@example.com");
```

Important:
This validation **does not guarantee deliverability** or that the mailbox exists.
It only verifies that the domain accepts e-mail.

---

## Full Validation (Composed)

For maximum safety, use the base validator, which chains all strategies explicitly.

```csharp
using Joaoaalves.MailValidator.Validators;

BaseMailValidator.Validate("user@example.com");
```

You can also fine-tune which strategies are applied:

```csharp
BaseMailValidator.Validate(
    mail: "user@example.com",
    validateMX: true,
    validateRegex: true,
    regexTimeoutMS: 100
);
```

---

## Failure Semantics

All validators fail **explicitly** by throwing exceptions.

You are expected to handle these failures at the application boundary:

```csharp
try
{
    BaseMailValidator.Validate(input.Email);
}
catch (InvalidMailException ex)
{
    // Invalid e-mail format
}
catch (InvalidDomainException ex)
{
    // Invalid or non-existent domain
}
catch (InvalidUsernameException ex)
{
    // Invalid local-part
}
```

No validator ever returns partial success or warnings.
If it passes, the rule was satisfied.