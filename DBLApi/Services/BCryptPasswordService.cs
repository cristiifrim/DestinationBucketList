using DBLApi.Services.Interfaces;

namespace DBLApi.Services
{
    public class BCryptPasswordService : IPasswordService
    {
        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool VerifyPassword(string password, string salt, string hash)
        {
            return hash == HashPassword(password, salt);
        }
    }
}