using DBLApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DBLApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Destination> Destinations { get; set; } = default!;
        public DbSet<StayDates> StayDates { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            
            modelBuilder.Entity<StayDates>()
                .HasIndex(sd => new { sd.UserId, sd.DestinationId })
                .IsUnique();

            modelBuilder.Entity<StayDates>()
                .HasOne(sd => sd.User)
                .WithMany(u => u.StayDates)
                .HasForeignKey(sd => sd.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StayDates>()
                .HasOne(sd => sd.Destination)
                .WithMany(d => d.StayDates)
                .HasForeignKey(sd => sd.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}