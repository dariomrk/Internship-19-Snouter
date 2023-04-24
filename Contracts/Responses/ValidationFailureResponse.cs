namespace Contracts.Responses
{
    public class ValidationFailureResponse
    {
        public IEnumerable<ValidationResponse> ValidationErrors { get; set; }

        public class ValidationResponse
        {
            public string PropertyName { get; set; }
            public string Message { get; set; }
        }
    }
}
