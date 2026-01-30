using DnsClient;
using Joaoaalves.MailValidator.Abstractions;
using Joaoaalves.MailValidator.Exceptions;

namespace Joaoaalves.MailValidator.Validators
{
    public sealed class MxMailValidator : IMailValidator
    {
        public static bool Validate(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                return false;

            var atIndex = mail.LastIndexOf('@');
            if (atIndex < 0 || atIndex == mail.Length - 1)
                return false;

            var domain = mail[(atIndex + 1)..];

            try
            {
                var lookup = new LookupClient();
                var result = lookup.Query(domain, QueryType.MX);

                return result.Answers.MxRecords().Any();
            }
            catch (Exception exc)
            {
                throw new InvalidDomainException(exc.Message);
            }
        }
    }
}