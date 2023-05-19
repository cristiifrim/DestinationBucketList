using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Errors
{
    public class UserNotFoundException : Exception, IExceptionDetails
    {

        public string Credential { get; set; }
        public UserNotFoundException(string credential) : base($"No users with credential {credential} found.")
        {
            Credential = credential;
        }
        public ProblemDetails GetDetails()
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Type = ErrorTypes.NotFound,
                Title = "User not found error",
                Detail = this.Message
            };
        }
    }
}