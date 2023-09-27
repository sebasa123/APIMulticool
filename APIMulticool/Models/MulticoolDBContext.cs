using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIMulticool.Models
{
    public partial class MulticoolDBContext : DbContext
    {
        public MulticoolDBContext()
        {
        }

        public MulticoolDBContext(DbContextOptions<MulticoolDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<CodigoRecuperacion> CodigoRecuperacions { get; set; } = null!;
        public virtual DbSet<Herramientum> Herramienta { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Repuesto> Repuestos { get; set; } = null!;
        public virtual DbSet<TipoProducto> TipoProductos { get; set; } = null!;
        public virtual DbSet<TipoRepuesto> TipoRepuestos { get; set; } = null!;
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("SERVER=LAPTOP-0OCE7TFC\\SQLEXPRESS;DATABASE=MulticoolDB;INTEGRATED SECURITY=TRUE;User Id=;Password=");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Idcli)
                    .HasName("PK__Cliente__91A9D48D59F4BEBE");

                entity.ToTable("Cliente");

                entity.Property(e => e.Idcli).HasColumnName("IDCli");

                entity.Property(e => e.ApellidoCli)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionCli)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCli)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CodigoRecuperacion>(entity =>
            {
                entity.HasKey(e => e.Idcr)
                    .HasName("PK__CodigoRe__B87D80B7783F6C06");

                entity.ToTable("CodigoRecuperacion");

                entity.Property(e => e.Idcr).HasColumnName("IDCR");

                entity.Property(e => e.CodigoRec)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCr).HasColumnName("EstadoCR");

                entity.Property(e => e.FechaCr)
                    .HasColumnType("date")
                    .HasColumnName("FechaCR");

                entity.Property(e => e.Fkusuario).HasColumnName("FKUsuario");

                entity.HasOne(d => d.FkusuarioNavigation)
                    .WithMany(p => p.CodigoRecuperacions)
                    .HasForeignKey(d => d.Fkusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CodigoRec__FKUsu__276EDEB3");
            });

            modelBuilder.Entity<Herramientum>(entity =>
            {
                entity.HasKey(e => e.Idher)
                    .HasName("PK__Herramie__A60961CFA6E8E507");

                entity.Property(e => e.Idher).HasColumnName("IDHer");

                entity.Property(e => e.NombreHer)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Idped)
                    .HasName("PK__Pedido__98F987F425CA9AE2");

                entity.ToTable("Pedido");

                entity.Property(e => e.Idped).HasColumnName("IDPed");

                entity.Property(e => e.DecripcionPed)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaPed).HasColumnType("date");

                entity.Property(e => e.Fkcli).HasColumnName("FKCli");

                entity.Property(e => e.Fkprod).HasColumnName("FKProd");

                entity.Property(e => e.Fkrep).HasColumnName("FKRep");

                entity.Property(e => e.Fkus).HasColumnName("FKUs");

                entity.HasOne(d => d.FkcliNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.Fkcli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__FKCli__239E4DCF");

                entity.HasOne(d => d.FkprodNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.Fkprod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__FKProd__267ABA7A");

                entity.HasOne(d => d.FkrepNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.Fkrep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__FKRep__22AA2996");

                entity.HasOne(d => d.FkusNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.Fkus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__FKUs__24927208");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Idprod)
                    .HasName("PK__Producto__965F3D2323D69585");

                entity.ToTable("Producto");

                entity.Property(e => e.Idprod).HasColumnName("IDProd");

                entity.Property(e => e.FktipoProd).HasColumnName("FKTipoProd");

                entity.Property(e => e.NombreProd)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FktipoProdNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FktipoProd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Producto__FKTipo__25869641");
            });

            modelBuilder.Entity<Repuesto>(entity =>
            {
                entity.HasKey(e => e.Idrep)
                    .HasName("PK__Repuesto__A6819372F40C6725");

                entity.ToTable("Repuesto");

                entity.Property(e => e.Idrep).HasColumnName("IDRep");

                entity.Property(e => e.DescripcionRep)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fkherramientas).HasColumnName("FKHerramientas");

                entity.Property(e => e.FktipoRep).HasColumnName("FKTipoRep");
            });

            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.HasKey(e => e.Idtp)
                    .HasName("PK__TipoProd__B87C3A846DE61860");

                entity.ToTable("TipoProducto");

                entity.Property(e => e.Idtp).HasColumnName("IDTP");

                entity.Property(e => e.NombreTp)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NombreTP");
            });

            modelBuilder.Entity<TipoRepuesto>(entity =>
            {
                entity.HasKey(e => e.Idtr)
                    .HasName("PK__TipoRepu__B87C3AFA4DA6C8C9");

                entity.ToTable("TipoRepuesto");

                entity.Property(e => e.Idtr).HasColumnName("IDTR");

                entity.Property(e => e.DescripcionTr)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DescripcionTR");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.Idtu)
                    .HasName("PK__TipoUsua__B87C3AF926EB900F");

                entity.ToTable("TipoUsuario");

                entity.Property(e => e.Idtu).HasColumnName("IDTU");

                entity.Property(e => e.NombreTu)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NombreTU");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idus)
                    .HasName("PK__Usuario__B87C12B8BD8E1852");

                entity.ToTable("Usuario");

                entity.Property(e => e.Idus).HasColumnName("IDUs");

                entity.Property(e => e.ContraUs)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FktipoUsuario).HasColumnName("FKTipoUsuario");

                entity.Property(e => e.NombreUs)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FktipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.FktipoUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__FKTipoU__21B6055D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
