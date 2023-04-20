using Common.Exceptions;
using Contracts.Responses;
using Serilog;
using System.Net;

namespace Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e) when (
                e is NotFoundException
                or JsonValidationException
                or BadRequestException)
            {
                Log.Error(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
            catch (Exception e)
            {
                Log.Fatal(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken = default)
        {

            var statusCode = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                JsonValidationException => HttpStatusCode.BadRequest,
                BadRequestException => HttpStatusCode.BadRequest,
                ArgumentNullException => HttpStatusCode.InternalServerError,
                ArgumentException => HttpStatusCode.InternalServerError,
                InvalidOperationException => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError,
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(new ExceptionResponse
            {
                StatusCode = statusCode,
                Message = exception.Message,
            }, cancellationToken);

        }
    }
}
