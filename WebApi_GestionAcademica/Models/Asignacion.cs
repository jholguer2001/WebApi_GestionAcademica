using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_GestionAcademica.Models
{
    [Table("Asignaciones")]
    public class Asignacion
    {
        [Key]
        public int AsignacionID { get; set; }

        [Required]
        public int EstudianteID { get; set; }

        [Required]
        public int CursoID { get; set; }

        [ForeignKey("EstudianteID")]
        public virtual Estudiante Estudiante { get; set; }

        [ForeignKey("CursoID")]
        public virtual Curso Curso { get; set; }
    }
}