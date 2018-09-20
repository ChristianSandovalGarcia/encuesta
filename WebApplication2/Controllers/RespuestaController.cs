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
    public class RespuestaController : Controller
    {
        Data.DataAccessLayer dataAccessLayer = new Data.DataAccessLayer();
        [HttpGet]
        public List<Respuesta> Get()
        {
            Encuesta encuesta = dataAccessLayer.getEncuesta();
            List<Respuesta> respuestas = dataAccessLayer.getPreguntas(encuesta.id);
            return respuestas;
        }
    }
}
