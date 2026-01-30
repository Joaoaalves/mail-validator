namespace Joaoaalves.MailValidator.Abstractions
{
    /// <summary>
    /// Validates an e-mail and throws exception on Fail
    /// </summary>
    public interface IMailValidator
    {
        bool Validate(string mail);
    }
}