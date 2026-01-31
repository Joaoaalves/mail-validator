# MX Validator

The MX validator verifies whether the domain part of an e-mail address is capable of receiving e-mail.

It queries DNS MX records using the `DnsClient` package.

---

## What it validates

* Domain existence
* Presence of MX records
* Absence of null MX configurations

---

## What it does NOT validate

* Mailbox existence
* Deliverability
* Temporary DNS failures
* SMTP-level acceptance

---

## Usage

```csharp
MxMailValidator.Validate("user@example.com");
````

---

## Important notes

* A valid MX record does NOT guarantee deliverability
* This validator should always be used **in conjunction with other validators**
* DNS failures are treated as validation failures

---

## When to use

Recommended when:

* Domain validity matters
* You want to avoid accepting non-mail domains
* Validation occurs at trusted or controlled boundaries
