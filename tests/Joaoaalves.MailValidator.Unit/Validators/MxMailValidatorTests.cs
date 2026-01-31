using Joaoaalves.MailValidator.Exceptions;
using Joaoaalves.MailValidator.Unit.Helpers;
using Joaoaalves.MailValidator.Validators;

namespace Joaoaalves.MailValidator.Unit.Validators
{
    public class MxMailValidatorTests
    {
        [Theory]
        [MemberData(nameof(EmailTestDataHelper.ValidMxEmails), MemberType = typeof(EmailTestDataHelper))]
        public void Validate_Should_Not_Throw_For_Valid_Mx_Emails(string email)
        {
            var excepttion = Record.Exception(() => MxMailValidator.Validate(email));

            Assert.Null(excepttion);
        }

        [Theory]
        [MemberData(nameof(EmailTestDataHelper.InvalidMxDomains), MemberType = typeof(EmailTestDataHelper))]
        public void Should_Reject_Email_With_Invalid_MX(string email)
        {
            Assert.Throws<InvalidDomainException>(() =>
                MxMailValidator.Validate(email));
        }
    }
}