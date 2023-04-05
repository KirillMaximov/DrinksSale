using DrinksSale.Models;
using Microsoft.EntityFrameworkCore;

namespace DrinksSale.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CoinModel> Coins { get; set; }
        public DbSet<ConditionModel> Condition { get; set; }
        public DbSet<SaleModel> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Environment.CurrentDirectory;

            var con_local = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={path}\\App_Data\\DrinksSaleDataBase.mdf;Database=DrinksSaleDataBase;Trusted_Connection=Yes;MultipleActiveResultSets=True;TrustServerCertificate=True;";

            var con_server = "Server=localhost\\SQLEXPRESS;Database=DrinksSaleDataBase;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(con_local);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
            });

            modelBuilder.Entity<CoinModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Denomination).IsRequired();
                entity.Property(e => e.IsActive).IsRequired();
            });

            modelBuilder.Entity<ConditionModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Money).IsRequired();
                entity.Property(e => e.Coin1).IsRequired();
                entity.Property(e => e.Coin2).IsRequired();
                entity.Property(e => e.Coin5).IsRequired();
                entity.Property(e => e.Coin10).IsRequired();
            });

            modelBuilder.Entity<SaleModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ConditionId).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Revenue).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.CreateDate).IsRequired();
            });
        }
    }
}
