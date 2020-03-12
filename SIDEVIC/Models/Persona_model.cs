using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIDEVIC.Models
{
    public class Persona_model
    {
        public int id_persona { get; set; }
        public string nombre { get; set; }
        public string cargo { get; set; }
        public string ruta { get; set; }
        public HttpPostedFileBase archivo { get; set; }
    }
}