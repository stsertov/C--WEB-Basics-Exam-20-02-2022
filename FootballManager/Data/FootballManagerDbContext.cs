namespace FootballManager.Data
{
    using FootballManager.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class FootballManagerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-FH98M39\SQLEXPRESS01;Database=FootballManager;Integrated Security=True;");
            }
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<UserPlayer> UserPlayers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPlayer>(up =>
            {
                up.HasKey(k => new { k.UserId, k.PlayerId });

                up.HasOne(up => up.User)
                .WithMany("UserPlayers")
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                up.HasOne(up => up.Player)
                .WithMany("UserPlayers")
                .HasForeignKey(up => up.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
