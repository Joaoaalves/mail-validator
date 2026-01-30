using Joaoaalves.MailValidator.Abstractions;
using Joaoaalves.MailValidator.Exceptions;

namespace Joaoaalves.MailValidator.Validators
{
    public sealed class MailValidator : IMailValidator
    {
        public static bool IsValid(string mail, bool validateMX = true, bool validateRegex = true)
        {
            bool isValid;

            if (mail == null)
                throw new InvalidMailException("Null e-mail");

            isValid = BuiltInMailValidator.Validate(mail);

            if (validateMX)
                isValid = MxMailValidator.Validate(mail);

            if (validateRegex)
                isValid = RegexMailValidator.Validate(mail);

            return isValid;
        }
    }
}