using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Errors
{
    public class DestinationNotFoundException : Exception, IExceptionDetails
    {
        public string Detail { get; set; }
        public DestinationNotFoundException(string detail) : base($"Destination with {detail} as a detail was not found.")
        {
            Detail = detail;
        }
        public ProblemDetails GetDetails()
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Type = ErrorTypes.NotFound,
                Title = "Destination not found error",
                Detail = this.Message
            };
        }
    }
}
