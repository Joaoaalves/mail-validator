namespace Joaoaalves.MailValidator.Unit.Helpers
{
    /// <summary>
    /// Centers all test cases
    /// </summary>
    public static class EmailTestDataHelper
    {
        /// <summary>
        /// Valid Mails
        /// </summary>
        public static IEnumerable<string> BasicValidEMails => [
            "email@example.com",
            "firstname.lastname@example.com",
            "email@subdomain.example.com",
            "firstname+lastname@example.com",
        ];

        public static IEnumerable<string> AllowedCharsEmails => [
            "email_with_underscore@example.com",
            "email-with-dash@example.com",
            "email.with.dots@example.com",
            "email123@example.com",
        ];

        public static IEnumerable<string> ComplexDomainEmails => [
            "email@example.co.uk",
            "email@example.travel",
            "email@example.museum",
            "email@example.io",
        ];

        public static IEnumerable<string> UppercaseEmails => [
            "EMAIL@EXAMPLE.COM",
            "Email.MixedCase@example.com",
        ];

        public static IEnumerable<string> ComplexValidUsernameEmails => [
            "x@example.com",
            "very.common@example.com",
            "disposable.style.email.with+symbol@example.com",
        ];

        public static IEnumerable<string> RFCValidEmails => [
            "\"email\"@example.com",
            "\"email with spaces\"@example.com",
            "\"very.unusual.@.unusual.com\"@example.com",
        ];

        public static IEnumerable<string> RFCLocalDomainEmails => [
            "user@localhost",
        ];

        public static IEnumerable<string> IpLiteralEmails => [
            "user@[192.168.1.1]",
            "user@[IPv6:2001:db8::1]"
        ];


        /// <summary>
        /// Invalid emails
        /// </summary>
        public static IEnumerable<string> EmptyEmails => [
            null!,
            "",
            " ",
            "     ",
        ];

        public static IEnumerable<string> InvalidAt => [
            "plainaddress",
            "email.example.com",
            "email@@example.com",
            "email@ex@ample.com",
        ];

        public static IEnumerable<string> InvalidUsername => [
            ".email@example.com",
            "email.@example.com",
            "email..email@example.com",
            "email()@example.com",
            "email<>@example.com",
            "email,comma@example.com",
            "email:semicolon@example.com",
        ];

        public static IEnumerable<string> InvalidDomains => [
            "email@",
            "email@.",
            "email@..com",
            "email@-example.com",
            "email@example-.com",
            "email@example..com",
            "email@example_com",
        ];

        public static IEnumerable<string> InvalidTopLevelDomain => [
            "email@example.c",
            "email@example.123",
        ];

        public static IEnumerable<string> InvalidSpacesEmails => [
            "email @example.com",
            "email@ example.com",
            "email@example .com",
        ];

        public static IEnumerable<string> InvalidCharsEmails => [
            "email@exam!ple.com",
            "email@exa#mple.com",
        ];
        public static IEnumerable<string> InvalidRFCEmails => [
            "\"unclosed-quote@example.com",
            "email@[300.300.300.300]",
            "email@[IPv6:12345::]",
        ];
        public static IEnumerable<string> MaliciousEmails => [
            "<script>alert(1)</script>@example.com",
            "email@example.com<script>",
            "email\rexample@example.com",
            "email\n@example.com"
        ];
    }
}