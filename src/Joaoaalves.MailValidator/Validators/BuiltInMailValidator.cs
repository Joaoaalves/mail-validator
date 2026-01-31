using Joaoaalves.MailValidator.Abstractions;
using Joaoaalves.MailValidator.Exceptions;

namespace Joaoaalves.MailValidator.Validators
{
    public sealed class BuiltInMailValidator : IMailValidator
    {
        /// <summary>
        /// Basic email validator. It will typically validate emails and throw
        /// exceptions in more basic cases. Recommended for use only when the
        /// email is not a critical part of the system; it does not verify
        /// domain validity and fails in some cases of malicious emails.
        /// </summary>
        /// <param name="mail">E-mail to be validated.</param>
        /// <exception cref="InvalidMailException">InvalidMailException on invalid e-mail.</exception>
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