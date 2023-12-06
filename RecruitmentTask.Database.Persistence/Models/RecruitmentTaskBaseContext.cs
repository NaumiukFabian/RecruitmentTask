using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<Vproductinfo> Vproductinfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("inventory_pkey");

            entity.ToTable("inventory");

            entity.HasIndex(e => e.Sku, "inventory_sku_key").IsUnique();

            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Shippincost).HasColumnName("shippincost");
            entity.Property(e => e.Shipping).HasColumnName("shipping");
            entity.Property(e => e.Sku).HasColumnName("sku");
            entity.Property(e => e.Unit).HasColumnName("unit");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prices_pkey");

            entity.ToTable("prices");

            entity.HasIndex(e => e.Sku, "prices_sku_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nettproductpice).HasColumnName("nettproductpice");
            entity.Property(e => e.Sku).HasColumnName("sku");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => e.Sku, "products_sku_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Available).HasColumnName("available");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Defaultimage).HasColumnName("defaultimage");
            entity.Property(e => e.Ean).HasColumnName("ean");
            entity.Property(e => e.Isvendor).HasColumnName("isvendor");
            entity.Property(e => e.Iswire).HasColumnName("iswire");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Productername).HasColumnName("productername");
            entity.Property(e => e.Sku).HasColumnName("sku");
        });

        modelBuilder.Entity<Vproductinfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vproductinfo");

            entity.Property(e => e.CenaNetto).HasColumnName("Cena netto");
            entity.Property(e => e.Ean).HasColumnName("EAN");
            entity.Property(e => e.JednostkaLogistyczna).HasColumnName("Jednostka logistyczna");
            entity.Property(e => e.KosztDostawy).HasColumnName("Koszt dostawy");
            entity.Property(e => e.NazwaProducenta).HasColumnName("Nazwa producenta");
            entity.Property(e => e.NazwaProduktu).HasColumnName("Nazwa produktu");
            entity.Property(e => e.Sku).HasColumnName("sku");
            entity.Property(e => e.StanMagazynowy).HasColumnName("Stan magazynowy");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
