namespace DBLApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = Roles.User;
        public int Salt { get; set; }
    }
}