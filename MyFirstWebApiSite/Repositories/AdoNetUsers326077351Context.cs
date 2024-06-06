using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Repositories;

public partial class AdoNetUsers326077351Context : DbContext
{
    public AdoNetUsers326077351Context()
    {
    }

    public AdoNetUsers326077351Context(DbContextOptions<AdoNetUsers326077351Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        if(!optionsBuilder.IsConfigured)
              optionsBuilder.UseSqlServer("Data Source=srv2\\PUPILS;Initial Catalog=AdoNetUsers_326077351;Trusted_Connection=True;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("CATEGORY_NAME");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");
            entity.Property(e => e.OrderDate).HasColumnName("ORDER_DATE");
            entity.Property(e => e.OrderSum).HasColumnName("ORDER_SUM");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UsersOrders");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("Order_Item");

            entity.Property(e => e.OrderItemId).HasColumnName("ORDER_ITEM_ID");
            entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");
            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderItems_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderItems_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_PRODUCTS");

            entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.imageUrl)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("IMAGE_URL");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.ProductName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("PRODUCT_NAME");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_CategoryProduct");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("PASSWORD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
