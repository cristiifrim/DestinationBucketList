namespace DBLApi.Services.Interfaces
{
    public interface IPasswordService
    {
        public string HashPassword(string password, string salt);
        public string GenerateSalt();
        public bool VerifyPassword(string password, string salt, string hash);
    }
}