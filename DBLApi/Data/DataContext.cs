using DBLApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DBLApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
    }
}