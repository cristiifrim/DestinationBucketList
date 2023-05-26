using System.Net;
using System.Text.Json;
using DBLApi.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Middleware;

public class ErrorHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            ProblemDetails error;

            switch (exception)
            {
                // Template:
                // case CigaretteNotFoundException ex:
                //     error = ex.GetDetails();
                //     break;
                // Add here any custom exceptions you want to handle.

                case UserAlreadyExistsException ex:
                    error = ex.GetDetails();
                    break;
                    
                case UserNotFoundException ex:
                    error = ex.GetDetails();
                    break;

                case InvalidPasswordException ex:
                    error = ex.GetDetails();
                    break;

                case DestinationNotFoundException ex:
                    error = ex.GetDetails();
                    break;
                
                case DestinationAlreadyExistsException ex:
                    error = ex.GetDetails();
                    break;
                
                case InvalidDateException ex:
                    error = ex.GetDetails();
                    break;

                default:
                    error = new ProblemDetails();
                    error.Status = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(error);
            context.Response.StatusCode = error.Status.GetValueOrDefault((int)HttpStatusCode.InternalServerError);
            await response.WriteAsync(result);
        }
    }
}
