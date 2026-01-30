using System.Net;
using System.Text.RegularExpressions;
using Joaoaalves.MailValidator.Exceptions;

namespace Joaoaalves.MailValidator.Validators
{
    public class RegexMailValidator
    {
        private static readonly Regex LocalPartRegex = new(
            @"^((""[^\r\n""]+"")|([a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*))$",
            RegexOptions.Compiled);

        private static readonly Regex DomainRegex = new(
            @"^(localhost|(?:(?!-)[A-Za-z0-9-]{1,63}(?<!-)\.)+[A-Za-z]{2,})$",
            RegexOptions.Compiled);

        public static void Validate(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidDomainException("Email can't be empty.");

            int atIndex = email.LastIndexOf('@');
            if (atIndex < 1 || atIndex == email.Length - 1)
                throw new InvalidDomainException($"Invalid e-mail: {email}");

            string localPart = email.Substring(0, atIndex);
            string domainPart = email.Substring(atIndex + 1);

            // Local-part
            if (!LocalPartRegex.IsMatch(localPart))
                throw new InvalidDomainException($"Invalid local-part: {localPart}");

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
                    if (!IPAddress.TryParse(ipv6, out IPAddress addr) || addr.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
                        throw new InvalidDomainException($"Invalid IPv6: {ip}");
                }
                else
                {
                    if (!IPAddress.TryParse(ip, out IPAddress addr) || addr.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                        throw new InvalidDomainException($"Invalid IPv4: {ip}");
                }
            }
            else
            {
                if (!DomainRegex.IsMatch(domainPart))
                    throw new InvalidDomainException($"Invalid domain: {domainPart}");
            }
        }
    }
}
