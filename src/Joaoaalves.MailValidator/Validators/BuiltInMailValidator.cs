using Joaoaalves.MailValidator.Abstractions;
using Joaoaalves.MailValidator.Exceptions;

namespace Joaoaalves.MailValidator.Validators
{
    public sealed class BuiltInMailValidator : IMailValidator
    {
        public static bool Validate(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                throw new InvalidMailException("Empty e-mail is not allowed");

            var trimmed = mail.Trim();

            try
            {
                var addr = new System.Net.Mail.MailAddress(trimmed);
                return addr.Address == trimmed;
            }
            catch (Exception exc)
            {
                throw new InvalidMailException(exc.Message);
            }
        }
    }
}