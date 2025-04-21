using System.Linq;
using System.Web.Http;
using System.Net;
using System.Data.Entity;
using WebApi_GestionAcademica.Models;

namespace WebApi_GestionAcademica.Controllers
{
    [RoutePrefix("api/estudiantes")]
    public class EstudiantesController : ApiController
    {
        private SistemaGestionAcademicaContext db = new SistemaGestionAcademicaContext();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetEstudiantes(string filtro = null)
        {
            var query = db.Estudiantes.Include(e => e.Carrera).AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(e => e.Nombre.ToLower().Contains(filtro)
                                      || e.Apellido.ToLower().Contains(filtro)
                                      || e.Correo.ToLower().Contains(filtro));
            }

            return Ok(query.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetEstudiante(int id)
        {
            var estudiante = db.Estudiantes.Include(e => e.Carrera).FirstOrDefault(e => e.EstudianteID == id);
            if (estudiante == null)
                return NotFound();

            return Ok(estudiante);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostEstudiante(Estudiante estudiante)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!IsValidEmail(estudiante.Correo))
                return BadRequest("Correo electrónico no válido.");

            if (!db.Carreras.Any(c => c.CarreraID == estudiante.CarreraID))
                return BadRequest("La carrera especificada no existe.");

            db.Estudiantes.Add(estudiante);
            db.SaveChanges();

            return Created($"api/estudiantes/{estudiante.EstudianteID}", estudiante);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult PutEstudiante(int id, Estudiante estudiante)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != estudiante.EstudianteID)
                return BadRequest("El ID del estudiante no coincide.");

            if (!IsValidEmail(estudiante.Correo))
                return BadRequest("Correo electrónico no válido.");

            if (!db.Carreras.Any(c => c.CarreraID == estudiante.CarreraID))
                return BadRequest("La carrera especificada no existe.");

            db.Entry(estudiante).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteEstudiante(int id)
        {
            var estudiante = db.Estudiantes.Include(e => e.Asignaciones).FirstOrDefault(e => e.EstudianteID == id);
            if (estudiante == null)
                return NotFound();

            // Eliminar las asignaciones asociadas al estudiante
            foreach (var asignacion in estudiante.Asignaciones.ToList())
            {
                db.Asignaciones.Remove(asignacion);
            }

            // Ahora eliminar el estudiante
            db.Estudiantes.Remove(estudiante);
            db.SaveChanges();

            return Ok(estudiante);
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
