﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace RestauranteBackend.Migrations
{
    [DbContext(typeof(DbContextInventario))]
    [Migration("20250320220809_AddInitTable")]
    partial class AddInitTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Empleado", b =>
                {
                    b.Property<string>("id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<ulong>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("correo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("edad")
                        .HasColumnType("int");

                    b.Property<string>("genero")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("id");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("RestauranteBackend.Models.Permiso", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("Activo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("RestauranteBackend.Models.RolePermiso", b =>
                {
                    b.Property<string>("RolId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PermisoId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("RolId", "PermisoId");

                    b.HasIndex("PermisoId");

                    b.ToTable("RolePermiso", (string)null);
                });

            modelBuilder.Entity("Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Activo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.Property<string>("id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<ulong>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("contrasena")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("empleado_id")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("role_id")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("usuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("id");

                    b.HasIndex("empleado_id")
                        .IsUnique();

                    b.HasIndex("role_id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("RestauranteBackend.Models.RolePermiso", b =>
                {
                    b.HasOne("RestauranteBackend.Models.Permiso", "Permiso")
                        .WithMany("RolePermisos")
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Role", "Rol")
                        .WithMany("RolePermisos")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permiso");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Usuario", b =>
                {
                    b.HasOne("Empleado", "Empleado")
                        .WithOne("Usuario")
                        .HasForeignKey("Usuario", "empleado_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Role", "Role")
                        .WithMany("Usuarios")
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Empleado", b =>
                {
                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RestauranteBackend.Models.Permiso", b =>
                {
                    b.Navigation("RolePermisos");
                });

            modelBuilder.Entity("Role", b =>
                {
                    b.Navigation("RolePermisos");

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
