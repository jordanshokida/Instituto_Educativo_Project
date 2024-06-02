﻿using _2024_InstitutoEducativo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _2024_InstitutoEducativo.Data
{
    public class InstitutoContext : IdentityDbContext<IdentityUser<int>,IdentityRole<int>,int>
    {
        public InstitutoContext(DbContextOptions options) : base(options)
        {

        }

        public InstitutoContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Alumno>()
            //    .HasOne(a => a.Carrera)
            //    .WithMany(c => c.Alumnos)
            //    .HasForeignKey(a => a.CarreraId);

            //modelBuilder.Entity<Carrera>()
            //.HasMany(c => c.Materias)
            //.WithOne(m => m.Carrera)
            //.HasForeignKey(m => m.CarreraId);

            //modelBuilder.Entity<Calificacion>()
            //.HasOne(c => c.MateriaCursada)
            //.WithMany(mc => mc.Calificaciones)
            //.HasForeignKey(c => c.MateriaCursadaId);

            //modelBuilder.Entity<Calificacion>()
            //.HasOne(c => c.Profesor)
            //.WithMany(p => p.CalificacionesRealizadas)
            //.HasForeignKey(c => c.ProfesorId);

            //modelBuilder.Entity<Calificacion>()
            //.HasOne(c => c.Alumno)
            //.WithMany(a => a.Calificaciones)
            //.HasForeignKey(c => c.AlumnoId);

            #region Establecer nombres para los Identity Stores

            modelBuilder.Entity<IdentityUser<int>>().ToTable("Personas"); //resuelve la utilización de ASPNETUSERS

            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles"); //resuelve la utilización de ASPNETROLES

            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("PersonasRoles");

            #endregion

        }





        public DbSet<Persona> Personas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<MateriaCursada> MateriasCursadas { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }

        public DbSet<Rol> Roles {  get; set; }






    }
}
