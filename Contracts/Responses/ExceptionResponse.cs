using System.Net;

namespace Contracts.Responses
{
    public class ExceptionResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
