using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Errors
{
    public class InvalidDateException : Exception, IExceptionDetails
    {

        public InvalidDateException() : base("Invalid date error")
        {
        }

        public ProblemDetails GetDetails()
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = ErrorTypes.InvalidDate,
                Title = "Invalid date error",
                Detail = this.Message
            };
        }
    }
}