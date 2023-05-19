namespace DBLApi.Errors
{
    public static class ErrorTypes
    {
        public const string NotFound = "not-found-error";
        public const string BadRequest = "bad-request-error";
        public const string InvalidPassword = "invalid-password-error";
        public const string InvalidDate = "invalid-date-error";
        public const string AlreadyExists = "already-exists-error";
        public const string Unauthorized = "unauthorized-error";
        public const string Forbidden = "forbidden-error";
        public const string InternalServerError = "internal-server-error";
    }
}