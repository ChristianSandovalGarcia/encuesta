using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.APP.Pages
{
    public class GraciasModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Apreciamos mucho tu calificación!";
        }
    }
}
