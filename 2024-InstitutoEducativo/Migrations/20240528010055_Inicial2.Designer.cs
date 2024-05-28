﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _2024_InstitutoEducativo.Data;

#nullable disable

namespace _2024_InstitutoEducativo.Migrations
{
    [DbContext(typeof(InstitutoContext))]
    [Migration("20240528010055_Inicial2")]
    partial class Inicial2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Calificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlumnoId")
                        .HasColumnType("int");

                    b.Property<int>("MateriaCursadaId")
                        .HasColumnType("int");

                    b.Property<int?>("MateriaId")
                        .HasColumnType("int");

                    b.Property<int>("NotaFinal")
                        .HasColumnType("int");

                    b.Property<int>("ProfesorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("MateriaCursadaId");

                    b.HasIndex("MateriaId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Carrera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Carreras");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Direccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Localidad")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PersonaId")
                        .HasColumnType("int");

                    b.Property<string>("Provincia")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId")
                        .IsUnique();

                    b.ToTable("Direcciones");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Materia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarreraId")
                        .HasColumnType("int");

                    b.Property<int>("CodMateria")
                        .HasColumnType("int");

                    b.Property<int>("CupoMaximo")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("MateriaNombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("CarreraId");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.MateriaCursada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlumnoId")
                        .HasColumnType("int");

                    b.Property<int>("AnioCursada")
                        .HasColumnType("int");

                    b.Property<string>("Cuatrimestre")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("MateriaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("ProfesorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("MateriaId")
                        .IsUnique();

                    b.HasIndex("ProfesorId");

                    b.ToTable("MateriasCursadas");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Dni")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Personas");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Persona");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Telefono", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodArea")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("PersonaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.ToTable("Telefonos");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Alumno", b =>
                {
                    b.HasBaseType("_2024_InstitutoEducativo.Models.Persona");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int>("CarreraId")
                        .HasColumnType("int");

                    b.Property<int>("NumeroMatricula")
                        .HasColumnType("int");

                    b.HasIndex("CarreraId");

                    b.HasDiscriminator().HasValue("Alumno");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Empleado", b =>
                {
                    b.HasBaseType("_2024_InstitutoEducativo.Models.Persona");

                    b.Property<string>("Legajo")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Empleado");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Profesor", b =>
                {
                    b.HasBaseType("_2024_InstitutoEducativo.Models.Persona");

                    b.Property<string>("Legajo")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Personas", t =>
                        {
                            t.Property("Legajo")
                                .HasColumnName("Profesor_Legajo");
                        });

                    b.HasDiscriminator().HasValue("Profesor");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Calificacion", b =>
                {
                    b.HasOne("_2024_InstitutoEducativo.Models.Alumno", "Alumno")
                        .WithMany("Calificaciones")
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2024_InstitutoEducativo.Models.MateriaCursada", "MateriaCursada")
                        .WithMany("Calificaciones")
                        .HasForeignKey("MateriaCursadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2024_InstitutoEducativo.Models.Materia", null)
                        .WithMany("Calificaciones")
                        .HasForeignKey("MateriaId");

                    b.HasOne("_2024_InstitutoEducativo.Models.Profesor", "Profesor")
                        .WithMany("CalificacionesRealizadas")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("MateriaCursada");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Direccion", b =>
                {
                    b.HasOne("_2024_InstitutoEducativo.Models.Persona", "Persona")
                        .WithOne("Direccion")
                        .HasForeignKey("_2024_InstitutoEducativo.Models.Direccion", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Materia", b =>
                {
                    b.HasOne("_2024_InstitutoEducativo.Models.Carrera", "Carrera")
                        .WithMany("Materias")
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrera");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.MateriaCursada", b =>
                {
                    b.HasOne("_2024_InstitutoEducativo.Models.Alumno", "Alumno")
                        .WithMany("MateriasCursadas")
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2024_InstitutoEducativo.Models.Materia", "Materia")
                        .WithOne("MateriaCursada")
                        .HasForeignKey("_2024_InstitutoEducativo.Models.MateriaCursada", "MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_2024_InstitutoEducativo.Models.Profesor", "Profesor")
                        .WithMany("MateriasCursadaActiva")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("Materia");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Telefono", b =>
                {
                    b.HasOne("_2024_InstitutoEducativo.Models.Persona", "Persona")
                        .WithMany("Telefonos")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Alumno", b =>
                {
                    b.HasOne("_2024_InstitutoEducativo.Models.Carrera", "Carrera")
                        .WithMany("Alumnos")
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrera");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Carrera", b =>
                {
                    b.Navigation("Alumnos");

                    b.Navigation("Materias");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Materia", b =>
                {
                    b.Navigation("Calificaciones");

                    b.Navigation("MateriaCursada");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.MateriaCursada", b =>
                {
                    b.Navigation("Calificaciones");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Persona", b =>
                {
                    b.Navigation("Direccion");

                    b.Navigation("Telefonos");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Alumno", b =>
                {
                    b.Navigation("Calificaciones");

                    b.Navigation("MateriasCursadas");
                });

            modelBuilder.Entity("_2024_InstitutoEducativo.Models.Profesor", b =>
                {
                    b.Navigation("CalificacionesRealizadas");

                    b.Navigation("MateriasCursadaActiva");
                });
#pragma warning restore 612, 618
        }
    }
}
