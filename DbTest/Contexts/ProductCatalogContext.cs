using System;
using System.Collections.Generic;
using Datalagring.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datalagring.Contexts;

public partial class ProductCatalogContext : DbContext
{
    public ProductCatalogContext()
    {
    }

    public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brands__3214EC07239B227E");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07DC0DEA28");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Colors__3214EC070D880FFD");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Images__3214EC0770BD89E7");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC070C5E50A3");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__BrandI__45F365D3");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__4222D4EF");

            entity.HasOne(d => d.Color).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__ColorI__4316F928");

            entity.HasOne(d => d.Image).WithMany(p => p.Products).HasConstraintName("FK__Products__ImageI__440B1D61");

            entity.HasOne(d => d.Size).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__SizeId__44FF419A");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sizes__3214EC075F24CB7E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
