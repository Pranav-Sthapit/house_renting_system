using HouseRentalBackend.DTO;
using HouseRentalBackend.Exceptions;
using Microsoft.AspNetCore.Diagnostics;


namespace HouseRentalBackend.Controllers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponse();

            switch (exception)
            {
                case DuplicateException de:
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    response.StatusCode=StatusCodes.Status409Conflict;
                    response.Message = de.Message;
                    break;

                case AlreadyExistException aee:
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    response.StatusCode = StatusCodes.Status409Conflict;
                    response.Message = aee.Message;
                    break;

                case NotFoundException nfe:
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = nfe.Message;
                    break;
                case Exception e:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = e.Message;
                    break;
                default:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "Internal Server Error";
                    break;
            }

            httpContext.Response.ContentType= "application/json";
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
