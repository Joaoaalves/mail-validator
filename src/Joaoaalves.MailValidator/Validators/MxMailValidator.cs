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

                var mxRecords = result.Answers.MxRecords();

                if (!mxRecords.Any())
                    throw new InvalidDomainException("No MX Records found for provided e-mail domain.");

                var hasValidMx = mxRecords.Any(mx =>
                    !string.IsNullOrWhiteSpace(mx.Exchange.Value) &&
                    mx.Exchange.Value != "."
                );

                if (!hasValidMx)
                    throw new InvalidDomainException("Domain does not accept e-mail (Null MX).");
            }
            catch (DnsResponseException)
            {
                throw new InvalidDomainException("Domain does not exist.");
            }
            catch (Exception)
            {
                throw new InvalidDomainException("Could not validate domain MX records.");
            }
        }
    }
}