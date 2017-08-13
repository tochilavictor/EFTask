namespace existingdb
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopModel2 : DbContext
    {
        public ShopModel2()
            : base("name=ShopModel2")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PurchaseProduct> PurchaseProducts { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.PurchaseProducts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Purchase>()
                .HasMany(e => e.PurchaseProducts)
                .WithRequired(e => e.Purchase)
                .WillCascadeOnDelete(false);
        }
    }
}
