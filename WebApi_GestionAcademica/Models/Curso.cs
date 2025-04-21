using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi_GestionAcademica.Models
{
    [Table("Cursos")]
    public class Curso
    {
        [Key]
        public int CursoID { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(20, ErrorMessage = "El código no puede tener más de 20 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
        public string Descripcion { get; set; }

        public virtual ICollection<Asignacion> Asignaciones { get; set; }

        public Curso()
        {
            Asignaciones = new HashSet<Asignacion>();
        }

    }
}