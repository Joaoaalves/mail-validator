namespace Joaoaalves.MailValidator.Exceptions
{
    public sealed class InvalidLocalPartException : InvalidMailException
    {
        public InvalidLocalPartException() : base("Invalid local-part for e-mail provided") { }
        public InvalidLocalPartException(string message) : base(message) { }
    }
}