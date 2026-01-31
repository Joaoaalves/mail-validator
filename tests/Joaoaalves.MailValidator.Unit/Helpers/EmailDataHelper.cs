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
            [ "_______@example.com"]
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

        public static IEnumerable<object[]> ValidMxEmails =>
        [
            [ "user@gmail.com" ],
            [ "firstname.lastname@gmail.com" ],
            [ "user+tag@gmail.com" ],

            [ "user@outlook.com" ],
            [ "user@hotmail.com" ],

            [ "user@yahoo.com" ],

            [ "user@icloud.com" ],

            [ "user@proton.me" ],
            [ "user@protonmail.com" ],

            [ "user@zoho.com" ]
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
                IpLiteralEmails,
                ValidMxEmails
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

        public static IEnumerable<object[]> InvalidLocalPart =>
        [
            [ ".email@example.com" ],
            [ "email.@example.com" ],
            [ "email..email@example.com" ],
            [ "email()@example.com" ],
            [ "email<>@example.com" ],
            [ "email,comma@example.com" ],
            [ "email:semicolon@example.com" ],
            [ "”(),:;<>[\\]@example.com"],
            [ "just”not”right@example.com"],
            [ "this\\ is\"really\"not\allowed@example.com"],
            [ "#@%^%#$@#$@#.com"]
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
            [ "email@111.222.333.44444"]
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

        public static IEnumerable<object[]> ReDOSEmail => [
            ["a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a.a@example.com"]
        ];

        public static IEnumerable<object[]> InvalidMxDomains =>
        [
            // Invalid Domains
            [ "user@thisdomaindoesnotexist123456.com" ],
            [ "user@abcdef-ghijklmnop-xyz.com" ],

            // Valid Domains, without MX
            [ "user@localhost" ],
            [ "user@example.com" ]
        ];

        public static IEnumerable<object[]> InvalidEmails =>
            ConcatAll(
                EmptyEmails,
                InvalidAt,
                InvalidLocalPart,
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
