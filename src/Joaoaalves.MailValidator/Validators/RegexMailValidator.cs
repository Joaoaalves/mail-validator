using System.Text.RegularExpressions;
using System.Globalization;
using Joaoaalves.MailValidator.Abstractions;
using Joaoaalves.MailValidator.Exceptions;

namespace Joaoaalves.MailValidator.Validators
{
    public sealed class RegexMailValidator : IMailValidator
    {
        // Class Body
        public static bool Validate(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                throw new InvalidMailException("Empty e-mail is not allowed");

            try
            {
                // Normalize the domain
                mail = Regex.Replace(mail, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                throw new InvalidDomainException("Regex verification timeout.");
            }
            catch (ArgumentException exc)
            {
                throw new InvalidDomainException(exc.Message);
            }

            try
            {
                return Regex.IsMatch(mail,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                throw new InvalidUsernameException("Regex verification timeout.");
            }
        }
    }
}