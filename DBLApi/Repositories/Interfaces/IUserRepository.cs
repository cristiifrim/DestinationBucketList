using DBLApi.Models;

namespace DBLApi.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> IsEmailTaken(string email);
        Task<bool> IsUsernameTaken(string username);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByUsername(string username);
    }
}