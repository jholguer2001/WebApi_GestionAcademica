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
    [RoutePrefix("api/asignaciones")]
    public class AsignacionesController : ApiController
    {
        private SistemaGestionAcademicaContext dbContext = new SistemaGestionAcademicaContext();

        // GET: api/asignaciones
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAsignaciones()
        {
            try
            {
                // Desactivar LazyLoading y ProxyCreation temporalmente para esta consulta
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;

                var asignaciones = dbContext.Asignaciones
                    .Include(a => a.Estudiante)
                    .Include(a => a.Curso)
                    .Include(a => a.Estudiante.Carrera)
                    .ToList();
                return Ok(asignaciones);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/asignaciones/5
        [HttpGet]
        [Route("{id:int}", Name = "GetAsignacionById")]
        public IHttpActionResult GetAsignacion(int id)
        {
            try
            {
                // Desactivar LazyLoading y ProxyCreation temporalmente para esta consulta
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;

                Asignacion asignacion = dbContext.Asignaciones
                    .Include(a => a.Estudiante)
                    .Include(a => a.Curso)
                    .Include(a => a.Estudiante.Carrera)
                    .FirstOrDefault(a => a.AsignacionID == id);

                if (asignacion == null)
                {
                    return NotFound();
                }

                return Ok(asignacion);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/asignaciones
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostAsignacion(Asignacion asignacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validar que el estudiante exista
                if (!dbContext.Estudiantes.Any(e => e.EstudianteID == asignacion.EstudianteID))
                {
                    return BadRequest("El estudiante no existe.");
                }

                // Validar que el curso exista
                if (!dbContext.Cursos.Any(c => c.CursoID == asignacion.CursoID))
                {
                    return BadRequest("El curso no existe.");
                }

                // Validar que no exista ya la misma asignación
                if (dbContext.Asignaciones.Any(a =>
                    a.EstudianteID == asignacion.EstudianteID &&
                    a.CursoID == asignacion.CursoID))
                {
                    return BadRequest("El estudiante ya está asignado a este curso.");
                }

                dbContext.Asignaciones.Add(asignacion);
                dbContext.SaveChanges();

                // Desactivar LazyLoading y ProxyCreation temporalmente para esta consulta
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;

                // Cargar las relaciones para la respuesta
                asignacion = dbContext.Asignaciones
                    .Include(a => a.Estudiante)
                    .Include(a => a.Curso)
                    .Include(a => a.Estudiante.Carrera)
                    .FirstOrDefault(a => a.AsignacionID == asignacion.AsignacionID);

                return CreatedAtRoute("GetAsignacionById", new { id = asignacion.AsignacionID }, asignacion);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/asignaciones/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult PutAsignacion(int id, Asignacion asignacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != asignacion.AsignacionID)
                {
                    return BadRequest("El ID de la asignación no coincide con el ID en la URL.");
                }

                // Validar que el estudiante exista
                if (!dbContext.Estudiantes.Any(e => e.EstudianteID == asignacion.EstudianteID))
                {
                    return BadRequest("El estudiante no existe.");
                }

                // Validar que el curso exista
                if (!dbContext.Cursos.Any(c => c.CursoID == asignacion.CursoID))
                {
                    return BadRequest("El curso no existe.");
                }

                // Validar que no exista ya la misma asignación para otro registro
                if (dbContext.Asignaciones.Any(a =>
                    a.EstudianteID == asignacion.EstudianteID &&
                    a.CursoID == asignacion.CursoID &&
                    a.AsignacionID != asignacion.AsignacionID))
                {
                    return BadRequest("El estudiante ya está asignado a este curso en otra asignación.");
                }

                var existingAsignacion = dbContext.Asignaciones.Find(id);
                if (existingAsignacion == null)
                {
                    return NotFound();
                }

                // Actualizar propiedades
                existingAsignacion.EstudianteID = asignacion.EstudianteID;
                existingAsignacion.CursoID = asignacion.CursoID;

                dbContext.Entry(existingAsignacion).State = EntityState.Modified;
                dbContext.SaveChanges();

                // Desactivar LazyLoading y ProxyCreation temporalmente para esta consulta
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;

                // Cargar las relaciones para la respuesta
                existingAsignacion = dbContext.Asignaciones
                    .Include(a => a.Estudiante)
                    .Include(a => a.Curso)
                    .Include(a => a.Estudiante.Carrera)
                    .FirstOrDefault(a => a.AsignacionID == id);

                return Ok(existingAsignacion);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/asignaciones/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteAsignacion(int id)
        {
            try
            {
                Asignacion asignacion = dbContext.Asignaciones.Find(id);
                if (asignacion == null)
                {
                    return NotFound();
                }

                dbContext.Asignaciones.Remove(asignacion);
                dbContext.SaveChanges();

                return Ok(asignacion);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/asignaciones/estudiante/5
        [HttpGet]
        [Route("estudiante/{estudianteId:int}")]
        public IHttpActionResult GetAsignacionesPorEstudiante(int estudianteId)
        {
            try
            {
                if (!dbContext.Estudiantes.Any(e => e.EstudianteID == estudianteId))
                {
                    return NotFound();
                }

                // Desactivar LazyLoading y ProxyCreation temporalmente para esta consulta
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;

                var asignaciones = dbContext.Asignaciones
                    .Include(a => a.Curso)
                    .Where(a => a.EstudianteID == estudianteId)
                    .ToList();

                return Ok(asignaciones);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/asignaciones/curso/5
        [HttpGet]
        [Route("curso/{cursoId:int}")]
        public IHttpActionResult GetAsignacionesPorCurso(int cursoId)
        {
            try
            {
                if (!dbContext.Cursos.Any(c => c.CursoID == cursoId))
                {
                    return NotFound();
                }

                // Desactivar LazyLoading y ProxyCreation temporalmente para esta consulta
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;

                var asignaciones = dbContext.Asignaciones
                    .Include(a => a.Estudiante)
                    .Include(a => a.Estudiante.Carrera)
                    .Where(a => a.CursoID == cursoId)
                    .ToList();

                return Ok(asignaciones);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}