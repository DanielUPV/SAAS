using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIDEVIC.Models
{
    public class DatosIndicador
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string ult_ev { get; set; }
        public string tema { get; set; }     
        public string dato { get; set; }
    }
}