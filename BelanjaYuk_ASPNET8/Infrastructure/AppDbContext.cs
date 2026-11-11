using Microsoft.EntityFrameworkCore;
using BelanjaYuk.Entities;

namespace BelanjaYuk.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<LtGender> LtGenders { get; set; }
        public DbSet<MsUser> MsUsers { get; set; }
        public DbSet<LtCategory> LtCategories { get; set; }
        public DbSet<LtPayment> LtPayments { get; set; }
        public DbSet<MsUserPassword> MsUserPasswords { get; set; }
        public DbSet<MsUserSeller> MsUserSellers { get; set; }
        public DbSet<MsProduct> MsProducts { get; set; }
        public DbSet<TrProductImages> TrProductImages { get; set; }
        public DbSet<TrBuyerCart> TrBuyerCarts { get; set; }
        public DbSet<TrBuyerTransaction> TrBuyerTransactions { get; set; }
        public DbSet<TrBuyerTransactionDetail> TrBuyerTransactionDetails { get; set; }
        public DbSet<TrHomeAddress> TrHomeAddresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // foreign key dari TrBuyerCart ke MsUser tidak cascade delete
            modelBuilder.Entity<TrBuyerCart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrHomeAddress>()
                .HasOne(a => a.User)
                .WithMany(u => u.HomeAddresses)
                .HasForeignKey(a => a.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrBuyerTransaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}