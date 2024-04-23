using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NotBK.Models;

public partial class NotBkContext : DbContext
{
    public NotBkContext()
    {
    }

    public NotBkContext(DbContextOptions<NotBkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PromoItem> PromoItems { get; set; }

    public virtual DbSet<Promocion> Promocions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
    
        // taken off according to MS recommendations

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CodCategoria).HasName("PK_Cat");

            entity.Property(e => e.CodCategoria)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codCategoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CodCliente);

            entity.ToTable("Cliente");

            entity.Property(e => e.CodCliente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codCliente");
            entity.Property(e => e.Nit)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nit");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => new { e.CodItem, e.CodPedido }).HasName("PK_DetPedido");

            entity.ToTable("DetallePedido");

            entity.Property(e => e.CodItem)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codItem");
            entity.Property(e => e.CodPedido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codPedido");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("Local")
                .HasColumnName("direccion");

            entity.HasOne(d => d.CodItemNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.CodItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemDP");

            entity.HasOne(d => d.CodPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.CodPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedidoDP");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.CodItem);

            entity.ToTable("Item");

            entity.Property(e => e.CodItem)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codItem");
            entity.Property(e => e.CodCategoria)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codCategoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("money")
                .HasColumnName("precio");
            entity.Property(e => e.Size)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("size");

            entity.HasOne(d => d.CodCategoriaNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.CodCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatItem");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.CodPedido);

            entity.ToTable("Pedido");

            entity.Property(e => e.CodPedido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codPedido");
            entity.Property(e => e.CodCliente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codCliente");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");

            entity.HasOne(d => d.CodClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.CodCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CliPedido");
        });

        modelBuilder.Entity<PromoItem>(entity =>
        {
            entity.HasKey(e => new { e.CodItem, e.CodPromo });

            entity.ToTable("PromoItem");

            entity.Property(e => e.CodItem)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codItem");
            entity.Property(e => e.CodPromo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codPromo");
            entity.Property(e => e.FechaFin)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaFin");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaInicio");

            entity.HasOne(d => d.CodItemNavigation).WithMany(p => p.PromoItems)
                .HasForeignKey(d => d.CodItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemPromItem");

            entity.HasOne(d => d.CodPromoNavigation).WithMany(p => p.PromoItems)
                .HasForeignKey(d => d.CodPromo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PromPromoItem");
        });

        modelBuilder.Entity<Promocion>(entity =>
        {
            entity.HasKey(e => e.CodPromo).HasName("PK_Promo");

            entity.ToTable("Promocion");

            entity.Property(e => e.CodPromo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codPromo");
            entity.Property(e => e.Descuento)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
