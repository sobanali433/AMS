using Microsoft.EntityFrameworkCore;

namespace AMS.Data
{
    public class AmsContext : DbContext
    {
        public AmsContext(DbContextOptions<AmsContext> options) : base(options)
        {

        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<BranchMaster> BranchMasters { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserMaster → Branch
            modelBuilder.Entity<UserMaster>()
                .HasOne(u => u.BranchMasters)
                .WithMany(b => b.UserMasters)
                .HasForeignKey(u => u.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Stock → Branch
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.BranchMasters)
                .WithMany(b => b.Stocks)
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order → Branch
            modelBuilder.Entity<Order>()
                .HasOne(o => o.BranchMasters)
                .WithMany()
                .HasForeignKey(o => o.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product → Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Categories)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Stock → Product
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Products)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order → Product
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Products)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision fix
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }


    }
}
