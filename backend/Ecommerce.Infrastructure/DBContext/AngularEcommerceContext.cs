﻿using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.DBContext;

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

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPosition> UserPositions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__Order__57ED06817D0DBC55");

            entity.ToTable("Order");

            entity.Property(e => e.OrderItemId).ValueGeneratedNever();
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Transaction).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Transactions");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A79D1AECB46");

            entity.ToTable("Position");

            entity.Property(e => e.PositionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PositionName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD7ADBDED6");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Image)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B5AACD610");

            entity.Property(e => e.TransactionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.BuyerId)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Change).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Comment).HasMaxLength(200);
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.EmployeeId)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.OrderList)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Paid).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PaymentDetail).HasMaxLength(200);
            entity.Property(e => e.PaymentType)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.SellerId)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ShippingCost).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
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
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Position).WithMany(p => p.Users)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Position");
        });

        modelBuilder.Entity<UserPosition>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PositionId });

            entity.ToTable("UserPosition");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Position).WithMany(p => p.UserPositions)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPosition_Position");

            entity.HasOne(d => d.User).WithMany(p => p.UserPositions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPosition_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
