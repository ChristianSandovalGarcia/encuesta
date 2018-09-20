using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApplication2.Shared
{
    public class Pregunta
    {
        public int id { get; set; }
        public string pregunta { get; set; }
        public bool esOpcional { get; set; }
        public string tipo { get; set; }
    }
}
