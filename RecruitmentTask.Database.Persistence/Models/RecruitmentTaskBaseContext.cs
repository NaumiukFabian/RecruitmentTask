using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RecruitmentTask.Database.Persistence.Models;

public partial class RecruitmentTaskBaseContext : DbContext
{
    public RecruitmentTaskBaseContext()
    {
    }

    public RecruitmentTaskBaseContext(DbContextOptions<RecruitmentTaskBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=recruitmentTaskBase;Username=postgres;Password=beatka");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inventory_pkey");

            entity.ToTable("inventory");

            entity.HasIndex(e => e.Sku, "inventory_sku_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Shippincost).HasColumnName("shippincost");
            entity.Property(e => e.Sku).HasColumnName("sku");
            entity.Property(e => e.Unit).HasColumnName("unit");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prices_pkey");

            entity.ToTable("prices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nettproductpirce).HasColumnName("nettproductpirce");
            entity.Property(e => e.Sku).HasColumnName("sku");

            entity.HasOne(d => d.SkuNavigation).WithMany(p => p.Prices)
                .HasPrincipalKey(p => p.Sku)
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prices_sku_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => e.Sku, "products_sku_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Defaultimage).HasColumnName("defaultimage");
            entity.Property(e => e.Ean).HasColumnName("ean");
            entity.Property(e => e.Producername).HasColumnName("producername");
            entity.Property(e => e.Shipping).HasColumnName("shipping");
            entity.Property(e => e.Sku).HasColumnName("sku");

            entity.HasOne(d => d.SkuNavigation).WithOne(p => p.Product)
                .HasPrincipalKey<Inventory>(p => p.Sku)
                .HasForeignKey<Product>(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_sku_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
