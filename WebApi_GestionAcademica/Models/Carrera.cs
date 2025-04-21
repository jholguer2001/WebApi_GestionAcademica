using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_GestionAcademica.Models
{
    public class Carrera
    {
        [Key]
        public int CarreraID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        // Relación con Estudiantes
        public virtual ICollection<Estudiante> Estudiantes { get; set; }
    }
}