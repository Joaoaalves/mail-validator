using Joaoaalves.MailValidator.Exceptions;
using Joaoaalves.MailValidator.Unit.Helpers;
using Joaoaalves.MailValidator.Validators;

namespace Joaoaalves.MailValidator.Unit.Validators
{
    public class RegexMailValidatorTests
    {
        /* ===========================
         * VALID EMAILS
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.ValidEmails), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Not_Throw_For_Valid_Emails(string email)
        {
            var excepttion = Record.Exception(() => RegexMailValidator.Validate(email));

            Assert.Null(excepttion);
        }

        /* ===========================
         * INVALID EMAILS (NO EXCEPTION)
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.InvalidAt), MemberType = typeof(EmailTestDataHelper))]
        [MemberData(nameof(EmailTestDataHelper.InvalidUsername), MemberType = typeof(EmailTestDataHelper))]
        [MemberData(nameof(EmailTestDataHelper.InvalidSpacesEmails), MemberType = typeof(EmailTestDataHelper))]
        [MemberData(nameof(EmailTestDataHelper.InvalidCharsEmails), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_For_Invalid_Emails(string email)
        {
            Assert.ThrowsAny<InvalidMailException>(() =>
                RegexMailValidator.Validate(email)
            );
        }

        /* ===========================
         * INVALID DOMAIN (IDN / FORMAT)
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.InvalidDomains), MemberType = typeof(EmailTestDataHelper))]
        [MemberData(nameof(EmailTestDataHelper.InvalidTopLevelDomain), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_InvalidDomainException_For_Invalid_Domains(string email)
        {
            Assert.Throws<InvalidDomainException>(() =>
                RegexMailValidator.Validate(email)
            );
        }

        /* ===========================
         * MALICIOUS / GARBAGE INPUTS
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.MaliciousEmails), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_For_Malicious_Inputs(string email)
        {
            Assert.ThrowsAny<InvalidMailException>(() =>
                RegexMailValidator.Validate(email)
            );
        }

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.ReDOSEmail), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_For_ReDOS_Inputs(string email)
        {
            Assert.Throws<InvalidMailException>(() => RegexMailValidator.Validate(email, 0.1));
        }

        /* ===========================
         * NULL INPUT
         * =========================== */

        [Fact]
        public void Validate_Should_Throw_Exception_For_Null_Email()
        {
            Assert.ThrowsAny<Exception>(() =>
                RegexMailValidator.Validate(null!)
            );
        }
    }
}
