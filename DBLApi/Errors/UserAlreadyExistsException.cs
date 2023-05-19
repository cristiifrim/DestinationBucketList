using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Errors
{
    public class UserAlreadyExistsException : Exception, IExceptionDetails
    {
        public string Credential { get; set; }
        public UserAlreadyExistsException(string credential) : base($"An user with credential {credential} already exists.")
        {
            Credential = credential;
        }
        public ProblemDetails GetDetails()
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.Conflict,
                Type = ErrorTypes.AlreadyExists,
                Title = "User already exists error",
                Detail = this.Message
            };
        }    
    }
}