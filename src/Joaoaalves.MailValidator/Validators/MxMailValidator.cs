using DnsClient;
using Joaoaalves.MailValidator.Abstractions;
using Joaoaalves.MailValidator.Exceptions;

namespace Joaoaalves.MailValidator.Validators
{
    public sealed class MxMailValidator : IMailValidator
    {
        public static void Validate(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                throw new InvalidMailException("Empty or null e-mails are not allowed");

            var atIndex = mail.LastIndexOf('@');
            if (atIndex < 0 || atIndex == mail.Length - 1)
                throw new InvalidMailException("You cant start or finish your email with '@' character");

            var domain = mail[(atIndex + 1)..];

            try
            {
                var lookup = new LookupClient();
                var result = lookup.Query(domain, QueryType.MX);

                if (!result.Answers.MxRecords().Any())
                    throw new InvalidDomainException("No MX Records found for provided e-mail domain.");
            }
            catch (Exception exc)
            {
                throw new InvalidDomainException(exc.Message);
            }
        }
    }
}