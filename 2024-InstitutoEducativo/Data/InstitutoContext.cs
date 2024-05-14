using _2024_InstitutoEducativo.Models;
using Microsoft.EntityFrameworkCore;

namespace _2024_InstitutoEducativo.Data
{
    public class InstitutoContext : DbContext
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








    }
}
