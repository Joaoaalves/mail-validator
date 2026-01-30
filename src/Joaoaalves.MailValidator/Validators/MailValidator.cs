using Joaoaalves.MailValidator.Abstractions;

namespace Joaoaalves.MailValidator.Validators
{
    public sealed class MailValidator : IMailValidator
    {
        public static void Validate(string mail, bool validateMX = true, bool validateRegex = true)
        {
            BuiltInMailValidator.Validate(mail);

            if (validateMX)
                MxMailValidator.Validate(mail);

            if (validateRegex)
                RegexMailValidator.Validate(mail);
        }
    }
}