using System.Collections.Generic;

namespace Joaoaalves.MailValidator.Unit.Helpers
{
    /// <summary>
    /// Centers all test cases for email validation
    /// </summary>
    public static class EmailTestDataHelper
    {
        /* ===========================
         * VALID EMAILS
         * =========================== */

        public static IEnumerable<object[]> BasicValidEmails =>
        [
            ["email@example.com"],
            ["firstname.lastname@example.com"],
            ["email@subdomain.example.com"],
            ["firstname+lastname@example.com"],
        ];

        public static IEnumerable<object[]> AllowedCharsEmails =>
        [
            [ "email_with_underscore@example.com" ],
            [ "email-with-dash@example.com" ],
            [ "email.with.dots@example.com" ],
            [ "email123@example.com" ],
        ];

        public static IEnumerable<object[]> ComplexDomainEmails =>
        [
            [ "email@example.co.uk" ],
            [ "email@example.travel" ],
            [ "email@example.museum" ],
            [ "email@example.io" ],
        ];

        public static IEnumerable<object[]> UppercaseEmails =>
        [
            [ "EMAIL@EXAMPLE.COM" ],
            [ "Email.MixedCase@example.com" ],
        ];

        public static IEnumerable<object[]> ComplexValidUsernameEmails =>
        [
            [ "x@example.com" ],
            [ "very.common@example.com" ],
            [ "disposable.style.email.with+symbol@example.com" ],
        ];

        public static IEnumerable<object[]> RFCValidEmails =>
        [
            [ "\"email\"@example.com" ],
            [ "\"email with spaces\"@example.com" ],
            [ "\"very.unusual.@.unusual.com\"@example.com" ],
        ];

        public static IEnumerable<object[]> RFCLocalDomainEmails =>
        [
            [ "user@localhost" ],
        ];

        public static IEnumerable<object[]> IpLiteralEmails =>
        [
            [ "user@[192.168.1.1]" ],
            [ "user@[IPv6:2001:db8::1]" ],
        ];

        public static IEnumerable<object[]> ValidEmails =>
            ConcatAll(
                BasicValidEmails,
                AllowedCharsEmails,
                ComplexDomainEmails,
                UppercaseEmails,
                ComplexValidUsernameEmails,
                RFCValidEmails,
                RFCLocalDomainEmails,
                IpLiteralEmails
            );

        /* ===========================
         * INVALID EMAILS
         * =========================== */

        public static IEnumerable<object[]> EmptyEmails =>
        [
            [ "" ],
            [ " " ],
            [ "     " ],
        ];

        public static IEnumerable<object[]> InvalidAt =>
        [
            [ "plainaddress" ],
            [ "email.example.com" ],
            [ "email@@example.com" ],
            [ "email@ex@ample.com" ],
        ];

        public static IEnumerable<object[]> InvalidUsername =>
        [
            [ ".email@example.com" ],
            [ "email.@example.com" ],
            [ "email..email@example.com" ],
            [ "email()@example.com" ],
            [ "email<>@example.com" ],
            [ "email,comma@example.com" ],
            [ "email:semicolon@example.com" ],
        ];

        public static IEnumerable<object[]> InvalidDomains =>
        [
            [ "email@" ],
            [ "email@." ],
            [ "email@..com" ],
            [ "email@-example.com" ],
            [ "email@example-.com" ],
            [ "email@example..com" ],
            [ "email@example_com" ],
        ];

        public static IEnumerable<object[]> InvalidTopLevelDomain =>
        [
            [ "email@example.c" ],
            [ "email@example.123" ],
        ];

        public static IEnumerable<object[]> InvalidSpacesEmails =>
        [
            [ "email @example.com" ],
            [ "email@ example.com" ],
            [ "email@example .com" ],
        ];

        public static IEnumerable<object[]> InvalidCharsEmails =>
        [
            [ "email@exam!ple.com" ],
            [ "email@exa#mple.com" ],
        ];

        public static IEnumerable<object[]> InvalidRFCEmails =>
        [
            [ "\"unclosed-quote@example.com" ],
            [ "email@[300.300.300.300]" ],
            [ "email@[IPv6:12345::]" ],
        ];

        public static IEnumerable<object[]> MaliciousEmails =>
        [
            [ "<script>alert(1)</script>@example.com" ],
            [ "email@example.com<script>" ],
            [ "email\rexample@example.com" ],
            [ "email\n@example.com" ],
        ];

        public static IEnumerable<object[]> InvalidEmails =>
            ConcatAll(
                EmptyEmails,
                InvalidAt,
                InvalidUsername,
                InvalidDomains,
                InvalidTopLevelDomain,
                InvalidSpacesEmails,
                InvalidCharsEmails,
                InvalidRFCEmails,
                MaliciousEmails
            );

        /* ===========================
         * INTERNAL HELPERS
         * =========================== */

        private static IEnumerable<object[]> ConcatAll(params IEnumerable<object[]>[] sources)
        {
            foreach (var source in sources)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }
    }
}
