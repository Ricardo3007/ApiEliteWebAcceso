using System;
using System.Collections.Generic;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using Microsoft.EntityFrameworkCore;

namespace ApiEliteWebAcceso.Infrastructure.Context;

public partial class AccesoContext : DbContext
{
    public AccesoContext()
    {
    }

    public AccesoContext(DbContextOptions<AccesoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ACC_APLICACION> ACC_APLICACION { get; set; }

    public virtual DbSet<ACC_EMPRESA> ACC_EMPRESA { get; set; }

    public virtual DbSet<ACC_GRUPO_EMPRESAS> ACC_GRUPO_EMPRESAS { get; set; }

    public virtual DbSet<ACC_MENU_ELITE> ACC_MENU_ELITE { get; set; }

    public virtual DbSet<ACC_OPCIONES_ROL> ACC_OPCIONES_ROL { get; set; }

    public virtual DbSet<ACC_PERMISO_EMPRESA> ACC_PERMISO_EMPRESA { get; set; }

    public virtual DbSet<ACC_PERMISO_USUARIO> ACC_PERMISO_USUARIO { get; set; }

    public virtual DbSet<ACC_ROLES> ACC_ROLES { get; set; }

    public virtual DbSet<ACC_USUARIO> ACC_USUARIO { get; set; }

    public virtual DbSet<GRL_TIPO_IDENTIFICACION> GRL_TIPO_IDENTIFICACION { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ACC_APLICACION>(entity =>
        {
            entity.HasKey(e => e.PK_APLICATIVO_C).HasName("PK__ACC_APLI__4EB541CD5B02674D");

            entity.Property(e => e.PK_APLICATIVO_C).ValueGeneratedNever();
            entity.Property(e => e.ESTADO_C)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.INICIALES_APLICATIVO_C)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NOMBRE_APLICATIVO_C)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ACC_EMPRESA>(entity =>
        {
            entity.HasKey(e => e.PK_EMPRESA_C).HasName("PK__ACC_EMPR__D505DBBF576270E8");

            entity.Property(e => e.PK_EMPRESA_C).ValueGeneratedNever();
            entity.Property(e => e.ESTADO_C)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_EMPRESA_C)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LOGO_EMPRESA_C)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NOMBRE_BD_C)
                .HasMaxLength(60)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NOMBRE_EMPRESA_C)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PASSWORD_BD_C)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SERVIDOR_BD_C)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.USUARIO_BD_C)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.FK_GRUPO_EMPRESA_CNavigation).WithMany(p => p.ACC_EMPRESA)
                .HasForeignKey(d => d.FK_GRUPO_EMPRESA_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACC_GRUPO_EMPRESA");
        });

        modelBuilder.Entity<ACC_GRUPO_EMPRESAS>(entity =>
        {
            entity.HasKey(e => e.PK_GRUPO_EMPRESA_C).HasName("PK__ACC_GRUP__4DDD4AA470C3547A");

            entity.Property(e => e.PK_GRUPO_EMPRESA_C).ValueGeneratedNever();
            entity.Property(e => e.ESTADO_C)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NOMBRE_GRUPO_C)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ACC_MENU_ELITE>(entity =>
        {
            entity.HasKey(e => e.PK_OPCION_MENU_C).HasName("PK__ACC_MENU__4A72AFFD105C186B");

            entity.Property(e => e.PK_OPCION_MENU_C).ValueGeneratedNever();
            entity.Property(e => e.DESCRIPCION_C)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.ESTADO_C)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TEXT_C)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.URL_C)
                .HasMaxLength(254)
                .IsUnicode(false);

            entity.HasOne(d => d.FK_APLICATIVO_CNavigation).WithMany(p => p.ACC_MENU_ELITE)
                .HasForeignKey(d => d.FK_APLICATIVO_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACC_APLICACION");
        });

        modelBuilder.Entity<ACC_OPCIONES_ROL>(entity =>
        {
            entity.HasKey(e => e.PK_OPCION_ROL_C).HasName("PK__ACC_OPCI__8A61E67AD1920145");

            entity.Property(e => e.PK_OPCION_ROL_C).ValueGeneratedNever();

            entity.HasOne(d => d.FK_OPCION_MENU_CNavigation).WithMany(p => p.ACC_OPCIONES_ROL)
                .HasForeignKey(d => d.FK_OPCION_MENU_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OPCION_MENU");

            entity.HasOne(d => d.FK_ROL_CNavigation).WithMany(p => p.ACC_OPCIONES_ROL)
                .HasForeignKey(d => d.FK_ROL_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROL");
        });

        modelBuilder.Entity<ACC_PERMISO_EMPRESA>(entity =>
        {
            entity.HasKey(e => e.PK_USUARIO_C).HasName("PK__ACC_PERM__D2ACAADA84F2E0E9");

            entity.Property(e => e.PK_USUARIO_C).ValueGeneratedNever();
            entity.Property(e => e.ESTADO_C)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.FK_APLICATIVO_CNavigation).WithMany(p => p.ACC_PERMISO_EMPRESA)
                .HasForeignKey(d => d.FK_APLICATIVO_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACC_PERMISO_EMPRESA_ACC_APLICACION");

            entity.HasOne(d => d.FK_EMPRESA_CNavigation).WithMany(p => p.ACC_PERMISO_EMPRESA)
                .HasForeignKey(d => d.FK_EMPRESA_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACC_EMPRESA_ACC_PERMISO_EMPRESA");
        });

        modelBuilder.Entity<ACC_PERMISO_USUARIO>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.PK_PERMISO_USUARIO_C).ValueGeneratedOnAdd();

            entity.HasOne(d => d.FK_EMPRESA_CNavigation).WithMany()
                .HasForeignKey(d => d.FK_EMPRESA_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPRESA_ACC_PERMISO_USUARIO");

            entity.HasOne(d => d.FK_OPCION_MENU_CNavigation).WithMany()
                .HasForeignKey(d => d.FK_OPCION_MENU_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OPCION_MEN_ACC_PERMISO_USUARIOU");

            entity.HasOne(d => d.FK_USUARIO_CNavigation).WithMany()
                .HasForeignKey(d => d.FK_USUARIO_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_ACC_PERMISO_USUARIO");
        });

        modelBuilder.Entity<ACC_ROLES>(entity =>
        {
            entity.HasKey(e => e.PK_ROL_C).HasName("PK__ACC_ROLE__F014E2DD2396CA4D");

            entity.Property(e => e.PK_ROL_C).ValueGeneratedNever();
            entity.Property(e => e.ESTADO_C)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NOMBRE_ROL_C)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.FK_GRUPO_EMPRESA_CNavigation).WithMany(p => p.ACC_ROLES)
                .HasForeignKey(d => d.FK_GRUPO_EMPRESA_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACC_GRUPO_EMPRESAS");
        });

        modelBuilder.Entity<ACC_USUARIO>(entity =>
        {
            entity.HasKey(e => e.PK_USUARIO_C).HasName("PK__ACC_USUA__D2ACAADA6C503748");

            entity.HasIndex(e => e.ID_USUARIO_C, "UQ__ACC_USUA__1D6B60CD04FDF944").IsUnique();

            entity.HasIndex(e => e.USUARIO_C, "UQ__ACC_USUA__879FFDE004A3CD7A").IsUnique();

            entity.HasIndex(e => e.MAIL_USUARIO_C, "UQ__ACC_USUA__9C103970ED590805").IsUnique();

            entity.Property(e => e.PK_USUARIO_C).ValueGeneratedNever();
            entity.Property(e => e.ESTADO_C)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ID_USUARIO_C)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MAIL_USUARIO_C)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NOMBRE_USUARIO_C)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PASSWORD_C)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.USUARIO_C)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.FK_TDI_CNavigation).WithMany(p => p.ACC_USUARIO)
                .HasForeignKey(d => d.FK_TDI_C)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TDI_ACC_USUARIO");
        });

        modelBuilder.Entity<GRL_TIPO_IDENTIFICACION>(entity =>
        {
            entity.HasKey(e => e.PK_TDI_C).HasName("PK__GRL_TIPO__ABAF821643CCF8B4");

            entity.Property(e => e.PK_TDI_C).ValueGeneratedNever();
            entity.Property(e => e.CODIGO_TDI_C)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NOMBRE_TDI_C)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SIGLAS_C)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
