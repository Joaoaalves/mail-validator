# Dependencies

Joaoaalves.MailValidator has **a single external runtime dependency**, used exclusively for DNS-based validation.

It relies on:

* **DnsClient (1.8.0)**  
  Used only by the MX validator to query DNS MX records.

All other functionality relies exclusively on:

* .NET runtime libraries

This makes Joaoaalves.MailValidator safe to use in:

* Application boundaries
* API layers
* Shared validation libraries
* Modular monoliths
* Microservices

The DNS dependency is **explicit, isolated, and optional**.  
If MX validation is not used, no DNS queries are performed.