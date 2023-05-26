using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Errors
{
    public class DestinationAlreadyExistsException : Exception, IExceptionDetails
    {
        public string Detail { get; set; }
        public DestinationAlreadyExistsException(string detail) : base($"Destination with {detail} as a detail already exists in the public list.")
        {
            Detail = detail;
        }
        public ProblemDetails GetDetails()
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.Conflict,
                Type = ErrorTypes.AlreadyExists,
                Title = "Destination already exists error",
                Detail = this.Message
            };
        }

    }  
}