using System.Net;
using System.Text.RegularExpressions;
using Joaoaalves.MailValidator.Exceptions;

namespace Joaoaalves.MailValidator.Validators
{

    public class RegexMailValidator
    {
        private static readonly string LocalPartRegex =
            @"^((""[^\r\n""]+"")|([a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*))$";

        private static readonly string DomainRegex =
            @"^(localhost|(?:(?!-)[A-Za-z0-9-]{1,63}(?<!-)\.)+[A-Za-z]{2,})$";

        /// <summary>
        /// Validates emails using Regex with a 250ms timeout. This validator checks
        /// both the LocalPart and the Domain regex. Recommended for use when emails
        /// are a critical part of the system. It usually blocks malicious emails,
        /// but does not check if the domain has an MX record.
        /// </summary>
        /// <param name="mail">E-mail to be validated.</param>
        /// <param name="timeoutMS">Timeout time in Mili.</param>
        /// <exception cref="InvalidMailException">InvalidMailException on invalid e-mail.</exception>
        /// <exception cref="InvalidDomain">InvalidMailException on invalid e-mail domain.</exception>
        /// <exception cref="InvalidLocalPartException">InvalidMailException on invalid e-mail Username.</exception>

        public static void Validate(string? email, double timeoutMS = 250)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidDomainException("Email can't be empty.");

            int atIndex = email.LastIndexOf('@');
            if (atIndex < 1 || atIndex == email.Length - 1)
                throw new InvalidDomainException($"Invalid e-mail: {email}");

            string localPart = email.Substring(0, atIndex);
            string domainPart = email.Substring(atIndex + 1);
            var timeout = TimeSpan.FromMilliseconds(timeoutMS);
            try
            {
                var localPartRegex = new Regex(
                    LocalPartRegex,
                    RegexOptions.Compiled | RegexOptions.CultureInvariant,
                    timeout
                );

                var domainRegex = new Regex(
                    DomainRegex,
                    RegexOptions.Compiled | RegexOptions.CultureInvariant,
                    timeout
                );

                // Local-part
                if (!localPartRegex.IsMatch(localPart))
                    throw new InvalidLocalPartException($"Invalid local-part: {localPart}");

                if (localPart.IndexOfAny(['\r', '\n']) >= 0)
                    throw new InvalidDomainException($"Local-part contains invalid characters: {localPart}");

                if (domainPart.IndexOfAny(['\r', '\n']) >= 0)
                    throw new InvalidDomainException($"Domain contains invalid characters: {domainPart}");

                // Domain
                if (domainPart.StartsWith("[") && domainPart.EndsWith("]"))
                {
                    string ip = domainPart.Trim('[', ']');
                    if (ip.StartsWith("IPv6:", StringComparison.OrdinalIgnoreCase))
                    {
                        string ipv6 = ip[5..];
                        if (!IPAddress.TryParse(ipv6, out IPAddress? addr) || addr.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
                            throw new InvalidDomainException($"Invalid IPv6: {ip}");
                    }
                    else
                    {
                        if (!IPAddress.TryParse(ip, out IPAddress? addr) || addr.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                            throw new InvalidDomainException($"Invalid IPv4: {ip}");
                    }
                }
                else
                {
                    if (!domainRegex.IsMatch(domainPart))
                        throw new InvalidDomainException($"Invalid domain: {domainPart}");
                }
            }
            catch (RegexMatchTimeoutException)
            {
                throw new InvalidMailException(
                    $"Regex validation exceeded timeout of {timeoutMS}ms.");
            }

        }

        /// <summary>
        /// Validates emails using Regex with a 250ms timeout. This validator checks
        /// both the LocalPart and the Domain regex. Recommended for use when emails
        /// are a critical part of the system. It usually blocks malicious emails,
        /// but does not check if the domain has an MX record.
        /// </summary>
        /// <param name="mail">E-mail to be validated.</param>
        /// <exception cref="InvalidMailException">InvalidMailException on invalid e-mail.</exception>
        /// <exception cref="InvalidDomain">InvalidMailException on invalid e-mail domain.</exception>
        /// <exception cref="InvalidLocalPartException">InvalidMailException on invalid e-mail Username.</exception>

        public static void Validate(string? email)
        {
            Validate(email, 250);
        }
    }
}
