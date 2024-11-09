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

    public virtual DbSet<Empresas> Empresas { get; set; }

    public virtual DbSet<Menus> Menus { get; set; }

    public virtual DbSet<Rol_Menu> Rol_Menu { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Usuario_Empresa> Usuario_Empresa { get; set; }

    public virtual DbSet<Usuario_Rol> Usuario_Rol { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empresas>(entity =>
        {
            entity.HasKey(e => e.pk_empresa_c).HasName("PK__Empresas__4897E041C4CCDA96");

            entity.Property(e => e.cadena_conexion_c)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.estado_c)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.fecha_actualizacion_c).HasColumnType("datetime");
            entity.Property(e => e.fecha_creacion_c)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.logo_c)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.nombre_c)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Menus>(entity =>
        {
            entity.HasKey(e => e.pk_menu_c).HasName("PK__Menus__0CE82B31A9659CA7");

            entity.Property(e => e.estado_c)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.fecha_actualizacion_c).HasColumnType("datetime");
            entity.Property(e => e.fecha_creacion_c)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.icono_c)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.nombre_c)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.url_c)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rol_Menu>(entity =>
        {
            entity.HasKey(e => e.pk_rol_menu_c).HasName("PK__Rol_Menu__5805A66F9AB2844B");

            entity.HasOne(d => d.fk_menu_cNavigation).WithMany(p => p.Rol_Menu)
                .HasForeignKey(d => d.fk_menu_c)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rol_Menu__fk_men__7E37BEF6");

            entity.HasOne(d => d.fk_rol_cNavigation).WithMany(p => p.Rol_Menu)
                .HasForeignKey(d => d.fk_rol_c)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rol_Menu__fk_rol__7D439ABD");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.pk_rol_c).HasName("PK__Roles__12A9F2612260DA4E");

            entity.Property(e => e.estado_c)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.fecha_actualizacion_c).HasColumnType("datetime");
            entity.Property(e => e.fecha_creacion_c)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.nombre_c)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.fk_empresa_cNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.fk_empresa_c)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Roles__fk_empres__76969D2E");
        });

        modelBuilder.Entity<Usuario_Empresa>(entity =>
        {
            entity.HasKey(e => e.pk_usuario_empresa_c).HasName("PK__Usuario___A86D1E60FE8F3CF2");

            entity.Property(e => e.estado_c)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.fk_empresa_cNavigation).WithMany(p => p.Usuario_Empresa)
                .HasForeignKey(d => d.fk_empresa_c)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario_E__fk_em__6FE99F9F");

            entity.HasOne(d => d.fk_usuario_cNavigation).WithMany(p => p.Usuario_Empresa)
                .HasForeignKey(d => d.fk_usuario_c)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario_E__fk_us__6EF57B66");
        });

        modelBuilder.Entity<Usuario_Rol>(entity =>
        {
            entity.HasKey(e => e.pk_usuario_rol_c).HasName("PK__Usuario___D1C329D1346F3407");

            entity.HasOne(d => d.fk_rol_cNavigation).WithMany(p => p.Usuario_Rol)
                .HasForeignKey(d => d.fk_rol_c)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario_R__fk_ro__7A672E12");

            entity.HasOne(d => d.fk_usuario_cNavigation).WithMany(p => p.Usuario_Rol)
                .HasForeignKey(d => d.fk_usuario_c)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario_R__fk_us__797309D9");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.pk_usuario_c).HasName("PK__Usuarios__8A2F09F2A0019EAA");

            entity.Property(e => e.documento_c)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.email_c)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.estado_c)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.fecha_actualizacion_c).HasColumnType("datetime");
            entity.Property(e => e.fecha_creacion_c)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.nombre_c)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.password_c)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
