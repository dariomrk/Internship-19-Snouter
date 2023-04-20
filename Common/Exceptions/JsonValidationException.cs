namespace Common.Exceptions
{
    public class JsonValidationException : Exception
    {
        public JsonValidationException() { }

        public JsonValidationException(string? message) : base(message) { }

        public JsonValidationException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
