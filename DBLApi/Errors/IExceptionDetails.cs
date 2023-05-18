using Microsoft.AspNetCore.Mvc;

namespace DBLApi.Errors
{
    public interface IExceptionDetails
    {
        ProblemDetails GetDetails();
    }
}