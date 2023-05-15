using System.Net;

namespace WealthApi.Middleware
{
    public class ErrorMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode HttpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode;

            ErrorDetails errorDetails = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode,
                Status = HttpStatusCode.ToString(),
                Message = exception.Message
            };

            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
