﻿// <auto-generated />
using System;
using Condominios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Condominios.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240115194447_MoreFieldsProviders")]
    partial class MoreFieldsProviders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Condominios.Models.Entities.Equipo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("CostoAdquisicion")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("EstatusID")
                        .HasColumnType("int");

                    b.Property<string>("Funcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSerie")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UbicacionID")
                        .HasColumnType("int");

                    b.Property<int>("VarianteID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EstatusID");

                    b.HasIndex("NumSerie")
                        .IsUnique();

                    b.HasIndex("UbicacionID");

                    b.HasIndex("VarianteID");

                    b.ToTable("Equipo");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Estatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Estatus");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Mantenimiento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("CostoMantenimiento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostoReparacion")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("FechaAplicacion")
                        .HasColumnType("bigint");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProveedorID")
                        .HasColumnType("int");

                    b.Property<int>("TipoMantenimientoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProveedorID");

                    b.HasIndex("TipoMantenimientoID");

                    b.ToTable("Mantenimiento");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Marca", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Marca");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Motor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Motor");
                });

            modelBuilder.Entity("Condominios.Models.Entities.MtoProgramado", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Aplicable")
                        .HasColumnType("bit");

                    b.Property<bool>("Aplicado")
                        .HasColumnType("bit");

                    b.Property<int>("EquipoID")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int?>("MantenimientoID")
                        .HasColumnType("int");

                    b.Property<long>("ProximaAplicacion")
                        .HasColumnType("bigint");

                    b.Property<long>("UltimaAplicacion")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("EquipoID");

                    b.HasIndex("MantenimientoID");

                    b.ToTable("MtoProgramado");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Perfil", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Perfil");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Periodo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("Meses")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Periodo");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Proveedor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Contacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Empresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Servicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Proveedor");
                });

            modelBuilder.Entity("Condominios.Models.Entities.TipoEquipo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TipoEquipo");
                });

            modelBuilder.Entity("Condominios.Models.Entities.TipoMantenimiento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TipoMantenimiento");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Ubicacion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Ubicacion");
                });

            modelBuilder.Entity("Condominios.Models.Entities.UnidadMedida", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("UnidadMedida");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PerfilID")
                        .HasColumnType("int");

                    b.Property<bool>("Restablecer")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Validado")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("PerfilID");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Variante", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Capacidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("MarcaID")
                        .HasColumnType("int");

                    b.Property<int>("MotorID")
                        .HasColumnType("int");

                    b.Property<int>("PeriodoID")
                        .HasColumnType("int");

                    b.Property<int>("TipoEquipoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MarcaID");

                    b.HasIndex("MotorID");

                    b.HasIndex("PeriodoID");

                    b.HasIndex("TipoEquipoID");

                    b.ToTable("Variante");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Equipo", b =>
                {
                    b.HasOne("Condominios.Models.Entities.Estatus", "Estatus")
                        .WithMany()
                        .HasForeignKey("EstatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Condominios.Models.Entities.Ubicacion", "Ubicacion")
                        .WithMany()
                        .HasForeignKey("UbicacionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Condominios.Models.Entities.Variante", "Variante")
                        .WithMany()
                        .HasForeignKey("VarianteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estatus");

                    b.Navigation("Ubicacion");

                    b.Navigation("Variante");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Mantenimiento", b =>
                {
                    b.HasOne("Condominios.Models.Entities.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Condominios.Models.Entities.TipoMantenimiento", "TipoMantenimiento")
                        .WithMany()
                        .HasForeignKey("TipoMantenimientoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");

                    b.Navigation("TipoMantenimiento");
                });

            modelBuilder.Entity("Condominios.Models.Entities.MtoProgramado", b =>
                {
                    b.HasOne("Condominios.Models.Entities.Equipo", "Equipo")
                        .WithMany("Programados")
                        .HasForeignKey("EquipoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Condominios.Models.Entities.Mantenimiento", "Mantenimiento")
                        .WithMany()
                        .HasForeignKey("MantenimientoID");

                    b.Navigation("Equipo");

                    b.Navigation("Mantenimiento");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Usuario", b =>
                {
                    b.HasOne("Condominios.Models.Entities.Perfil", "Perfil")
                        .WithMany()
                        .HasForeignKey("PerfilID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Variante", b =>
                {
                    b.HasOne("Condominios.Models.Entities.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Condominios.Models.Entities.Motor", "Motor")
                        .WithMany()
                        .HasForeignKey("MotorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Condominios.Models.Entities.Periodo", "Periodo")
                        .WithMany()
                        .HasForeignKey("PeriodoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Condominios.Models.Entities.TipoEquipo", "TipoEquipo")
                        .WithMany()
                        .HasForeignKey("TipoEquipoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marca");

                    b.Navigation("Motor");

                    b.Navigation("Periodo");

                    b.Navigation("TipoEquipo");
                });

            modelBuilder.Entity("Condominios.Models.Entities.Equipo", b =>
                {
                    b.Navigation("Programados");
                });
#pragma warning restore 612, 618
        }
    }
}
