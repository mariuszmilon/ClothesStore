using Microsoft.EntityFrameworkCore;

namespace ClothesStore.Entities
{
    public class ClothesStoreDbContext : DbContext
    {
        private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=ClothesStoreDb;Trusted_Connection=True";
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Trousers> Trousers { get; set; }
        public DbSet<PulloverAndSweatshirt> PulloversAndSweats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role() { Id = 1, Name = "User" },
                    new Role() { Id = 2, Name = "Admin" });

            modelBuilder.Entity<Item>()
                .Property(p => p.Price)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<User>()
                .HasOne(a => a.Address)
                .WithOne(u => u.User)
                .HasForeignKey<Address>(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasOne(r => r.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(r => r.RoleId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
