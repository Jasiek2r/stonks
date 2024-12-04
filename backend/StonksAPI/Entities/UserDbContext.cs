using Microsoft.EntityFrameworkCore;

namespace StonksAPI.Entities
{
    /*
     * A core class for setting up the database
     */
    public class UserDbContext : DbContext
    {
        private string _connectionString =
            "Server=(localdb)\\mssqllocaldb;Database=UserDb;Trusted_Connection=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Holding> Holdings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DB field requirements
            modelBuilder.Entity<User>()
                .Property(r => r.Username)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<User>()
                .Property(r => r.PasswordHash)
                .IsRequired();
            modelBuilder.Entity<Holding>()
                .Property(h=> h.Name)
                .IsRequired();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //setting up the database
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
