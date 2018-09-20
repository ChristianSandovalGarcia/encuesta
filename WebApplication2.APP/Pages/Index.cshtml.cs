using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApplication2.Shared;

namespace WebApplication2.APP.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Respuesta> respuestas { get; set; }
        public List<Pregunta> preguntas { get; set; }
        public string encuesta { get; set; }

        IConfiguration _iconfiguration;
        public IndexModel(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public void OnGet()
        {
            ProcessRepositories().Wait();
        }
        public IActionResult OnPost()
        {
            HttpClient client;
            client = new HttpClient();
            client.BaseAddress = new Uri(_iconfiguration["url"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.PostAsJsonAsync(_iconfiguration["url"] + "pregunta", respuestas);
            preguntas = new List<Pregunta>();
            encuesta = "";
            return RedirectToPage("Gracias");
        }
        private async Task ProcessRepositories()
        {
            HttpClient client;
            client = new HttpClient();
            client.BaseAddress = new Uri(_iconfiguration["url"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetStreamAsync(_iconfiguration["url"] + "pregunta");
            var serializer = new DataContractJsonSerializer(typeof(List<Pregunta>));
            var response = serializer.ReadObject(await responseMessage) as List<Pregunta>;
            preguntas = response;
            
            var responseMessage2 = client.GetStreamAsync(_iconfiguration["url"] + "respuesta");
            var serializer2 = new DataContractJsonSerializer(typeof(List<Respuesta>));
            var response2 = serializer2.ReadObject(await responseMessage2) as List<Respuesta>;
            respuestas = response2;
            
            Task<string> responseMessage3 = client.GetStringAsync(_iconfiguration["url"] + "encuesta");
            encuesta = responseMessage3.Result.Replace("\"","");
        }
    }
}
