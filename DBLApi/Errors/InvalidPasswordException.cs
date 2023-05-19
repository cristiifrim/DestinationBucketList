using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Errors
{
    public class InvalidPasswordException : Exception, IExceptionDetails
    {
        public InvalidPasswordException() : base("Invalid password. Please try again.")
        {

        }
        public ProblemDetails GetDetails()
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = ErrorTypes.InvalidPassword,
                Title = "Invalid password error",
                Detail = this.Message
            };
        }
    }
}