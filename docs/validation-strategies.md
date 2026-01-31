# Validation Strategies

Different systems require different levels of validation strictness.

Joaoaalves.MailValidator allows explicit trade-offs between **performance**, **safety**, and **correctness**.

---

## Strategy comparison

| Strategy | Performance | Strictness | Domain Validation |
|--------|------------|------------|-------------------|
| Built-in | High | Low | No |
| Regex | Medium | High | No |
| MX | Low | Medium | Yes |
| Base | Configurable | High | Optional |

---

## Recommended approaches

* UI-only validation → Built-in
* API boundaries → Regex
* Registration flows → Regex + MX
* Security-sensitive systems → Base validator
