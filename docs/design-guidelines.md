# Design Guidelines

Joaoaalves.MailValidator is intentionally conservative.

---

## Principles

* Validation must be explicit
* Failures must be deterministic
* No silent acceptance
* No false guarantees

---

## Guidelines

* Never assume deliverability
* Never rely on MX validation alone
* Always validate before persistence
* Treat validation as a boundary concern

---

## What this library dont't do

* Probe SMTP servers
* Validate inbox existence
* Perform background checks
* Retry DNS automatically