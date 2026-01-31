# Common Pitfalls

## Assuming deliverability

A valid e-mail does NOT mean it can receive messages.

MX validation only checks domain capability.

---

## Over-validating too early

Avoid strict validation during early user input stages.
Use stricter validation at system boundaries.

---

## Catching base Exception

Always catch specific validation exceptions.
Avoid swallowing validation failures.
