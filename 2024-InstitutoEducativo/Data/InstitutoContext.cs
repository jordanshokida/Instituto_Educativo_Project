using _2024_InstitutoEducativo.Models;
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

            modelBuilder.Entity<MateriaCursada>()
            .HasMany(mc => mc.Calificaciones)
            .WithOne(c => c.MateriaCursada)
            .HasForeignKey(c => c.MateriaCursadaId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Calificacion>()
            .HasOne(c => c.MateriaCursada)
            .WithMany(mc => mc.Calificaciones)
            .HasForeignKey(c => c.MateriaCursadaId)
            .OnDelete(DeleteBehavior.Cascade);

            #region Establecer nombres para los Identity Stores

            modelBuilder.Entity<IdentityUser<int>>().ToTable("Personas"); //resuelve la utilización de ASPNETUSERS

            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles"); //resuelve la utilización de ASPNETROLES

            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("PersonasRoles");

            #endregion


            #region Unique

            modelBuilder.Entity<Empleado>().HasIndex(e => e.Legajo).IsUnique();



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
