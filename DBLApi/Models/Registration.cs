namespace DBLApi.Models
{
    public class Registration
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateOnly Birthday { get; set; } 
    }
}