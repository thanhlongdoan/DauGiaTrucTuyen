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
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StatusUser> StatusUsers { get; set; }
        public virtual DbSet<TransactionAuction> TransactionAuctions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

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

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

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

            modelBuilder.Entity<User>()
                .HasMany(e => e.FeedBacks)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.User_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.User_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.User_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.StatusUsers)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.User_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TransactionAuctions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
