namespace Common.Exceptions
{
    public class JsonValidationException : BadRequestException
    {
        public JsonValidationException() { }

        public JsonValidationException(string? message) : base(message) { }

        public JsonValidationException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
