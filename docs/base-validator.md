# Base Validator

The base validator provides **explicit composition** of multiple validation strategies.

It safely chains:

1. Built-in validation
2. Regex validation (optional)
3. MX validation (optional)

---

## Default behavior

By default, all validations are enabled.

```csharp
BaseMailValidator.Validate("user@example.com");
````

---

## Custom configuration

You can explicitly enable or disable individual strategies:

```csharp
BaseMailValidator.Validate(
    mail: "user@example.com",
    validateMX: true,
    validateRegex: true,
    regexTimeoutMS: 100
);
```

---

## Design intent

The base validator exists purely as a **convenience composition**.
It introduces no additional behavior or assumptions.

All validations remain explicit and deterministic.