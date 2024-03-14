namespace CastleSharp.Core.Exceptions
{
    public class CustomConditionException : Exception
    {
        public CustomConditionException()
        {
        }

        public CustomConditionException(string? message) : base(message)
        {
        }
    }
}
