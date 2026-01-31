using Joaoaalves.MailValidator.Abstractions;

namespace Joaoaalves.MailValidator.Validators
{
    public sealed class BaseMailValidator : IMailValidator
    {
        /// <summary>
        /// Default call of this function will call Validate with all validations enabled.
        /// see <seealso cref="Validate(string email, bool, bool)" />.
        /// </summary>
        /// <param name="mail">Email to be validated</param>
        /// <exception cref="InvalidMailException">InvalidMailException on invalid e-mail.</exception>
        /// <exception cref="InvalidDomain">InvalidMailException on invalid e-mail domain.</exception>
        /// <exception cref="InvalidUsernameException">InvalidMailException on invalid e-mail Username.</exception>
        public static void Validate(string mail)
        {
            Validate(mail, true, true, 100);
        }

        /// <summary>
        /// Chains all validators call, this is the most safe e-mail validation. 
        /// You can disable either MX checks or/and Regex if you need to run validations faster.
        /// </summary>
        /// <param name="mail">E-mail to be validated.</param>
        /// <param name="validateMX">If true, checks for MX records on E-mail Domain.</param>
        /// <param name="validateRegex">If true, run Regex checks on E-mail.</param>
        /// <param name="regexTimeoutMS">Set the time in MS to Regex verifications Timeout.</param>
        /// <exception cref="InvalidMailException">InvalidMailException on invalid e-mail.</exception>
        /// <exception cref="InvalidDomain">InvalidMailException on invalid e-mail domain.</exception>
        /// <exception cref="InvalidUsernameException">InvalidMailException on invalid e-mail Username.</exception>
        public static void Validate(string mail, bool validateMX = true, bool validateRegex = true, double regexTimeoutMS = 50)
        {
            BuiltInMailValidator.Validate(mail);

            if (validateRegex)
                RegexMailValidator.Validate(mail, regexTimeoutMS);

            if (validateMX)
                MxMailValidator.Validate(mail);

        }
    }
}