using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIDEVIC.Models
{
    public class docModel
    {
        public int id_indicador { get; set; }
        public string nombre_indicador { get; set; }
        public HttpPostedFileBase archivo { get; set; }
    }
}