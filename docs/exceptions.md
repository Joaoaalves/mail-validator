# Validation Exceptions

Joaoaalves.MailValidator uses **explicit exception types** to represent validation failures.

No validator returns partial results or warnings.
Validation either succeeds or fails.

---

## Exception hierarchy

````

InvalidMailException
├── InvalidDomainException
└── InvalidUsernameException

```

---

## InvalidMailException

Base exception for all validation failures.

Thrown when an e-mail is malformed or cannot be validated safely.

---

## InvalidDomainException

Thrown when:

* The domain is malformed
* The domain does not exist
* The domain has no valid MX records
* Domain validation cannot be completed safely

---

## InvalidUsernameException

Thrown when the local-part (username) of the e-mail is invalid.

This includes:

* Invalid characters
* Malformed structure
* RFC violations