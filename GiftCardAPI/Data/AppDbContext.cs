using GiftCardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftCardAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<GiftCardTransaction> GiftCardTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.GiftCardTransactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<GiftCard>()
                .HasMany(g => g.Transactions)
                .WithOne(t => t.GiftCard)
                .HasForeignKey(t => t.GiftCardId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
