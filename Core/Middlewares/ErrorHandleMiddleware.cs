using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Model;
using Microsoft.AspNetCore.Http;

namespace Core.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var model = new ErrorResponseModel()
                {
                    Message = ex.Message,
                    Detail = ex.ToString()
                };

                var errorType = ex.GetType();

                if (ex is ArgumentException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else if (ex is NullReferenceException)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                if (ex is CustomException)
                {
                    var customException = ex as CustomException;
                    model.Code = customException.Code;
                    model.Title = customException.Title;
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(model));
            }
        }
    }
}