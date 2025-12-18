using System.Net;
using System.Text.Json;
using NewCarRental.Application.Wrappers;

namespace NewCarRental.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                // Mặc định là lỗi Server 500
                var responseModel = new ErrorResponse("An error occurred");
                switch (error)
                {
                    case Application.Exceptions.ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = e.Message;
                        responseModel.Errors = e.Errors;
                        break;

                    case Application.Exceptions.NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Message = e.Message;
                        break;

                    case Application.Exceptions.UnauthorizedException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        responseModel.Message = e.Message;
                        break;

                    case Application.Exceptions.UnauthenticatedException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        responseModel.Message = e.Message;
                        break;

                    case KeyNotFoundException e:
                        response.StatusCode= (int)HttpStatusCode.NotFound;
                        responseModel.Message = e.Message;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = "Lỗi hệ thống";
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
