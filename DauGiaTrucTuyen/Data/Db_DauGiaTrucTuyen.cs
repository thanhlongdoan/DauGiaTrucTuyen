namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Db_DauGiaTrucTuyen : DbContext
    {
        public Db_DauGiaTrucTuyen()
            : base("name=Db_DauGiaTrucTuyen")
        {
        }

        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<FeedBack> FeedBacks { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<StatusUser> StatusUsers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TransactionAuction> TransactionAuctions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.Category_Id);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductDetails)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.Product_Id);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Transactions)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.Product_Id);

            modelBuilder.Entity<TransactionAuction>()
                .Property(e => e.AuctionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.PriceStart)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Transaction>()
                .HasMany(e => e.TransactionAuctions)
                .WithRequired(e => e.Transaction)
                .WillCascadeOnDelete(false);
        }
    }
}
