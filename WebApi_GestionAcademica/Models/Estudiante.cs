using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_GestionAcademica.Models
{
    public class Estudiante
    {
        public int EstudianteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public int CarreraID { get; set; }

        // Relación con Carrera
        public virtual Carrera Carrera { get; set; }

        // Relación con Asignaciones
        public virtual ICollection<Asignacion> Asignaciones { get; set; }
    }
}