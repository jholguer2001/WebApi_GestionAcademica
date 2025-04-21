using System.Data.Entity;

namespace WebApi_GestionAcademica.Models
{
    public class SistemaGestionAcademicaContext : DbContext
    {
        public SistemaGestionAcademicaContext() : base("name=SistemaGestionAcademicaConnection")
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
    }
}