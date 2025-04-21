using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi_GestionAcademica.Models;

namespace WebApi_GestionAcademica.Controllers
{
    [RoutePrefix("api/cursos")]
    public class CursosController : ApiController
    {
        private SistemaGestionAcademicaContext db = new SistemaGestionAcademicaContext();

        // GET: api/cursos
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCursos(string filtro = null)
        {
            try
            {
                IQueryable<Curso> query = db.Cursos;

                // Aplicar filtro si existe
                if (!string.IsNullOrEmpty(filtro))
                {
                    filtro = filtro.ToLower();
                    query = query.Where(c =>
                        c.Nombre.ToLower().Contains(filtro) ||
                        c.Codigo.ToLower().Contains(filtro) ||
                        c.Descripcion.ToLower().Contains(filtro)
                    );
                }

                var cursos = query.ToList();
                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/cursos/5
        [HttpGet]
        [Route("{id:int}", Name = "GetCursoById")]
        public IHttpActionResult GetCurso(int id)
        {
            try
            {
                Curso curso = db.Cursos.Find(id);
                if (curso == null)
                {
                    return NotFound();
                }

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/cursos
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostCurso(Curso curso)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validar que el código no exista
                if (db.Cursos.Any(c => c.Codigo == curso.Codigo))
                {
                    return BadRequest("Ya existe un curso con el mismo código.");
                }

                db.Cursos.Add(curso);
                db.SaveChanges();

                // Ahora usamos el nombre de ruta correcto que hemos definido arriba
                return CreatedAtRoute("GetCursoById", new { id = curso.CursoID }, curso);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/cursos/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult PutCurso(int id, Curso curso)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != curso.CursoID)
                {
                    return BadRequest("El ID del curso no coincide con el ID de la URL.");
                }

                // Validar que el código no exista (si se está cambiando)
                var cursoExistente = db.Cursos.AsNoTracking().FirstOrDefault(c => c.CursoID == id);
                if (cursoExistente != null && cursoExistente.Codigo != curso.Codigo &&
                    db.Cursos.Any(c => c.Codigo == curso.Codigo))
                {
                    return BadRequest("Ya existe un curso con el mismo código.");
                }

                db.Entry(curso).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
                {
                    if (!CursoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/cursos/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteCurso(int id)
        {
            try
            {
                Curso curso = db.Cursos.Find(id);
                if (curso == null)
                {
                    return NotFound();
                }

                // Verificar si el curso tiene asignaciones
                var tieneAsignaciones = db.Asignaciones.Any(a => a.CursoID == id);
                if (tieneAsignaciones)
                {
                    return BadRequest("No se puede eliminar el curso porque tiene estudiantes asignados.");
                }

                db.Cursos.Remove(curso);
                db.SaveChanges();

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/cursos/5/estudiantes
        [HttpGet]
        [Route("{id:int}/estudiantes")]
        public IHttpActionResult GetEstudiantesPorCurso(int id)
        {
            try
            {
                if (!CursoExists(id))
                {
                    return NotFound();
                }

                var estudiantes = db.Asignaciones
                    .Where(a => a.CursoID == id)
                    .Select(a => a.Estudiante)
                    .ToList();

                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private bool CursoExists(int id)
        {
            return db.Cursos.Count(c => c.CursoID == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}