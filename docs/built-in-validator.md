# Built-in Validator

The built-in validator provides **fast and minimal e-mail validation** using .NET's internal mail parsing.

It is implemented by `BuiltInMailValidator` and uses `System.Net.Mail.MailAddress` internally.

---

## What it validates

* Basic e-mail syntax
* Presence of local-part and domain
* General structural correctness

---

## What it does NOT validate

* Domain existence
* MX records
* RFC edge cases
* Malicious inputs (CRLF, crafted payloads)

---

## Usage

```csharp
BuiltInMailValidator.Validate("user@example.com");
````

---

## When to use

Recommended only when:

* E-mails are not a critical system input
* Performance is more important than strict correctness
* Validation is purely cosmetic or advisory

Not recommended for security-sensitive or critical workflows.