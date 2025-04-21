using System.Linq;
using System.Web.Http;
using WebApi_GestionAcademica.Models;

namespace WebApi_GestionAcademica.Controllers
{
    [RoutePrefix("api/carreras")]
    public class CarrerasController : ApiController
    {
        private SistemaGestionAcademicaContext db = new SistemaGestionAcademicaContext();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCarreras()
        {
            var carreras = db.Carreras.ToList();
            return Ok(carreras);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCarrera(int id)
        {
            var carrera = db.Carreras.Find(id);
            if (carrera == null)
                return NotFound();

            return Ok(carrera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
