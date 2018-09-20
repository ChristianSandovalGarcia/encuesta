using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : Controller
    {
        Data.DataAccessLayer dataAccessLayer = new Data.DataAccessLayer();
        [HttpGet]
        public List<Pregunta> Get()
        {
            Encuesta encuesta = dataAccessLayer.getEncuesta();
            List<Pregunta> preguntas = dataAccessLayer.getPreguntas(encuesta.id, encuesta.nombre);
            return preguntas;
        }

        [HttpPost]
        public void Post([FromBody] List<Respuesta> respuestas)
        {
            foreach (var respuesta in respuestas)
            {
                dataAccessLayer.setRespuestas(respuesta.id, respuesta.respuesta);
            }
        }
    }
}
