using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Domain.Entities;

public partial class AngularEcommerceContext : DbContext
{
    public AngularEcommerceContext()
    {
    }

    public AngularEcommerceContext(DbContextOptions<AngularEcommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__Order__57ED06817D0DBC55");

            entity.ToTable("Order");

            entity.Property(e => e.OrderItemId).ValueGeneratedNever();

            entity.HasOne(d => d.Transaction).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Transactions");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD7ADBDED6");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B5AACD610");

            entity.Property(e => e.TransactionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BuyerId).HasMaxLength(50);
            entity.Property(e => e.Change).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Comment).HasMaxLength(200);
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EmployeeId).HasMaxLength(50);
            entity.Property(e => e.OrderList).HasMaxLength(50);
            entity.Property(e => e.Paid).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PaymentDetail).HasMaxLength(200);
            entity.Property(e => e.PaymentType).HasMaxLength(50);
            entity.Property(e => e.SellerId).HasMaxLength(50);
            entity.Property(e => e.ShippingCost).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TaxPercent).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CBC093AA6");

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
