namespace Joaoaalves.MailValidator.Exceptions
{
    public sealed class InvalidDomainException : InvalidMailException
    {
        public InvalidDomainException() : base("Invalid domain for e-mail provided ") { }
        public InvalidDomainException(string message) : base(message) { }
    }
}