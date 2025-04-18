﻿// <auto-generated />
using System;
using AutoGestion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoGestion.Migrations
{
    [DbContext(typeof(DbContextAutoGestion))]
    [Migration("20250414195954_AddCampoTipoSolicitud")]
    partial class AddCampoTipoSolicitud
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("AutoGestion.Models.CamposTipoSolicitud", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<ulong>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Adicionado_por")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("EsRequerido")
                        .HasColumnType("bit");

                    b.Property<string>("Modificado_por")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("TipoCampo")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("TipoSolicitudId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("nivel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoSolicitudId");

                    b.ToTable("CampoTipoSolicitud");
                });

            modelBuilder.Entity("AutoGestion.Models.ConfiguracionAprobacion", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<ulong>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Adicionado_por")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("Created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Empresa_id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Modificado_por")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("nivel")
                        .HasColumnType("int");

                    b.Property<string>("puesto_id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("Empresa_id");

                    b.HasIndex("puesto_id");

                    b.ToTable("ConfiguracionAprobacion");
                });

            modelBuilder.Entity("AutoGestion.Models.EmpleadoEmpresa", b =>
                {
                    b.Property<string>("EmpleadoId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("EmpresaId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("EmpleadoId", "EmpresaId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("EmpleadoEmpresa");
                });

            modelBuilder.Entity("AutoGestion.Models.Puesto", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<ulong>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Adicionado_por")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Empresa_id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Modificado_por")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Empresa_id");

                    b.ToTable("Puesto");
                });

            modelBuilder.Entity("AutoGestion.Models.Usuario", b =>
                {
                    b.Property<string>("id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<ulong>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("adicionado_por")
                        .HasColumnType("longtext");

                    b.Property<string>("contrasena")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("empleado_id")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("modificado_por")
                        .HasColumnType("longtext");

                    b.Property<string>("rol_id")
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

                    b.HasIndex("rol_id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Empleado", b =>
                {
                    b.Property<string>("id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<ulong>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("adicionado_por")
                        .HasColumnType("longtext");

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

                    b.Property<string>("jefe_id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("modificado_por")
                        .HasColumnType("longtext");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("puesto_id")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("id");

                    b.HasIndex("jefe_id");

                    b.HasIndex("puesto_id");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("Empresa", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<ulong>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("adicionado_por")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modificado_por")
                        .HasColumnType("longtext");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("Permiso", b =>
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

            modelBuilder.Entity("Role", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

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

                    b.Property<string>("adicionado_por")
                        .HasColumnType("longtext");

                    b.Property<string>("modificado_por")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("RolePermiso", b =>
                {
                    b.Property<string>("RolId")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("PermisoId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("RolId", "PermisoId");

                    b.HasIndex("PermisoId");

                    b.ToTable("RolePermiso");
                });

            modelBuilder.Entity("TipoSolicitud", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<ulong>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("adicionado_por")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("modificado_por")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("TipoSolicitud");
                });

            modelBuilder.Entity("AutoGestion.Models.CamposTipoSolicitud", b =>
                {
                    b.HasOne("TipoSolicitud", "TipoSolicitud")
                        .WithMany()
                        .HasForeignKey("TipoSolicitudId");

                    b.Navigation("TipoSolicitud");
                });

            modelBuilder.Entity("AutoGestion.Models.ConfiguracionAprobacion", b =>
                {
                    b.HasOne("Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("Empresa_id");

                    b.HasOne("AutoGestion.Models.Puesto", "Puesto")
                        .WithMany()
                        .HasForeignKey("puesto_id");

                    b.Navigation("Empresa");

                    b.Navigation("Puesto");
                });

            modelBuilder.Entity("AutoGestion.Models.EmpleadoEmpresa", b =>
                {
                    b.HasOne("Empleado", "Empleado")
                        .WithMany("EmpleadoEmpresas")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Empresa", "Empresa")
                        .WithMany("EmpleadoEmpresas")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("AutoGestion.Models.Puesto", b =>
                {
                    b.HasOne("Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("Empresa_id");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("AutoGestion.Models.Usuario", b =>
                {
                    b.HasOne("Empleado", "Empleado")
                        .WithOne("Usuario")
                        .HasForeignKey("AutoGestion.Models.Usuario", "empleado_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Role", "Role")
                        .WithMany("Usuarios")
                        .HasForeignKey("rol_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Empleado", b =>
                {
                    b.HasOne("Empleado", "Jefe")
                        .WithMany()
                        .HasForeignKey("jefe_id");

                    b.HasOne("AutoGestion.Models.Puesto", "Puesto")
                        .WithMany()
                        .HasForeignKey("puesto_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jefe");

                    b.Navigation("Puesto");
                });

            modelBuilder.Entity("RolePermiso", b =>
                {
                    b.HasOne("Permiso", "Permiso")
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

            modelBuilder.Entity("Empleado", b =>
                {
                    b.Navigation("EmpleadoEmpresas");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Empresa", b =>
                {
                    b.Navigation("EmpleadoEmpresas");
                });

            modelBuilder.Entity("Permiso", b =>
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
