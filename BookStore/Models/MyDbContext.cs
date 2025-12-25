
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BookStore.Models;

public partial class MyDbContext : IdentityDbContext<AppUserModel>
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CartModel> Carts { get; set; }

    public virtual DbSet<CategoryModel> Categories { get; set; }

    public virtual DbSet<ProductModel> Products { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderDetail> orderDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CartModel>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__carts__415B03D8BC45F806");

            entity.ToTable("carts");

            entity.Property(e => e.CartId)
                .ValueGeneratedNever()
                .HasColumnName("cartID");
            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UsersId).HasColumnName("usersID");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__carts__productID__3C69FB99");
        });

        modelBuilder.Entity<CategoryModel>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__category__23CAF1F890EE987B");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("categoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .HasColumnName("categoryName");
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__product__2D10D14AE12D0CA9");

            entity.ToTable("product");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("productID");
            entity.Property(e => e.CategoryId).HasColumnName("categoryID");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("imageUrl");
            entity.Property(e => e.NhaXuatBan)
                .HasMaxLength(10)
                .HasColumnName("nhaXuatBan");
            entity.Property(e => e.Nxb).HasColumnName("NXB");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("productName");
            entity.Property(e => e.TenTg)
                .HasMaxLength(20)
                .HasColumnName("tenTG");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__product__categor__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
