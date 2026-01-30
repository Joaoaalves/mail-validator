namespace Joaoaalves.MailValidator.Exceptions
{
    public sealed class InvalidUsernameException : InvalidMailException
    {
        public InvalidUsernameException() : base("Invalid username for e-mail provided") { }
        public InvalidUsernameException(string message) : base(message) { }
    }
}