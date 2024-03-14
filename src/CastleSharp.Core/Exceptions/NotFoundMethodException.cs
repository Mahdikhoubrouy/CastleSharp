namespace CastleSharp.Core.Exceptions
{
    public class NotFoundMethodException : Exception
    {
        public NotFoundMethodException()
        {
        }

        public NotFoundMethodException(string? message) : base(message)
        {
        }
    }
}
