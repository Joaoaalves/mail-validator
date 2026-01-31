using Joaoaalves.MailValidator.Exceptions;
using Joaoaalves.MailValidator.Unit.Helpers;

namespace Joaoaalves.MailValidator.Unit.Validators
{
    public class MailValidatorTests
    {
        /* ===========================
         * FULL VALIDATION
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.ValidMxEmails), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Not_Throw_When_All_Validators_Are_Enabled(string email)
        {
            var exception = Record.Exception(() =>
                MailValidator.Validators.MailValidator.Validate(email, validateMX: true, validateRegex: true));

            Assert.Null(exception);
        }

        /* ===========================
         * BUILT-IN ALWAYS RUNS
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.InvalidAt), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Always_Throw_For_BuiltIn_Invalid_Emails(string email)
        {
            Assert.ThrowsAny<Exception>(() =>
                MailValidator.Validators.MailValidator.Validate(email, validateMX: false, validateRegex: false));
        }

        [Fact]
        public void Validate_Should_Throw_For_Null_Email_Regardless_Of_Flags()
        {
            Assert.ThrowsAny<Exception>(() =>
                MailValidator.Validators.MailValidator.Validate(null!, validateMX: false, validateRegex: false));
        }

        /* ===========================
         * REGEX VALIDATION
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.InvalidUsername), MemberType = typeof(EmailTestDataHelper))]
        [MemberData(nameof(EmailTestDataHelper.InvalidSpacesEmails), MemberType = typeof(EmailTestDataHelper))]
        [MemberData(nameof(EmailTestDataHelper.InvalidCharsEmails), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_When_Regex_Is_Enabled(string email)
        {
            Assert.ThrowsAny<InvalidMailException>(() =>
                MailValidator.Validators.MailValidator.Validate(email, validateMX: false, validateRegex: true));
        }


        /* ===========================
         * MX VALIDATION
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.InvalidMxDomains), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_When_MX_Is_Enabled(string email)
        {
            Assert.Throws<InvalidDomainException>(() =>
                MailValidator.Validators.MailValidator.Validate(email, validateMX: true, validateRegex: false));
        }

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.InvalidMxDomains), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Not_Throw_When_MX_Is_Disabled(string email)
        {
            var exception = Record.Exception(() =>
                MailValidator.Validators.MailValidator.Validate(email, validateMX: false, validateRegex: false));

            Assert.Null(exception);
        }

        /* ===========================
         * COMBINED EDGE CASES
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.InvalidDomains), MemberType = typeof(EmailTestDataHelper))]
        [MemberData(nameof(EmailTestDataHelper.InvalidTopLevelDomain), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_When_Regex_Is_Enabled_Even_If_MX_Is_Disabled(string email)
        {
            Assert.ThrowsAny<InvalidMailException>(() =>
                MailValidator.Validators.MailValidator.Validate(email, validateMX: false, validateRegex: true));
        }


        /* ===========================
         * MALICIOUS INPUTS
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.MaliciousEmails), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_When_Regex_Is_Enabled_For_Malicious_Emails(string email)
        {
            Assert.ThrowsAny<InvalidMailException>(() =>
                MailValidator.Validators.MailValidator.Validate(email, validateMX: false, validateRegex: true));
        }

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.MaliciousEmails), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_When_Regex_Is_Disabled_For_Malicious_Emails(string email)
        {
            Assert.ThrowsAny<InvalidMailException>(() =>
                MailValidator.Validators.MailValidator.Validate(email, validateMX: false, validateRegex: false));
        }
    }
}
