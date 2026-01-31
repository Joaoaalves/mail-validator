using Joaoaalves.MailValidator.Exceptions;
using Joaoaalves.MailValidator.Unit.Helpers;
using Joaoaalves.MailValidator.Validators;

namespace Joaoaalves.MailValidator.Unit.Validators
{
    public class BuiltInMailValidatorTests
    {
        /* ===========================
         * VALID EMAILS
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.ValidEmails), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Return_True_For_Valid_Emails(string email)
        {
            var result = BuiltInMailValidator.Validate(email);

            Assert.True(result);
        }

        /* ===========================
         * INVALID EMAILS
         * =========================== */

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.InvalidAt), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Throw_InvalidMailException_For_Invalid_Emails(string email)
        {
            Assert.Throws<InvalidMailException>(() =>
                BuiltInMailValidator.Validate(email)
            );
        }

        /* ===========================
         * NULL EMAIL
         * =========================== */

        [Fact]
        public void Validate_Should_Throw_Exception_For_Null_Email()
        {
            Assert.ThrowsAny<Exception>(() =>
                BuiltInMailValidator.Validate(null!)
            );
        }
    }
}
