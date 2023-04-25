using Common.Exceptions;
using Contracts.Responses;
using FluentValidation;
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
            catch (ValidationException ex)
            {
                Log.Error(ex, ex.Message);

                var validationFailureResponse = new ValidationFailureResponse
                {
                    ValidationErrors = ex.Errors.Select(e => new ValidationFailureResponse.ValidationResponse
                    {
                        PropertyName = e.PropertyName,
                        Message = e.ErrorMessage,
                    })
                };

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(validationFailureResponse);
            }
            catch (Exception ex) when (ex
                is BadRequestException
                or JsonValidationException
                or NotFoundException)
            {
                Log.Error(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    ex.Message,
                });
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    ex.Message,
                });
            }
        }
    }
}
