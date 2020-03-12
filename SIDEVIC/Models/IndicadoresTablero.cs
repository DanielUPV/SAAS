using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIDEVIC.Models
{
    public class IndicadoresTablero
    {
        public int id { get; set; }
        public string eje { get; set; }
        public string tema { get; set; }
        public string indicador { get; set; }
        public string area { get; set; }
        public string tipo { get; set; }
        public string inicio { get; set; }
        public string fin { get; set; }
        public string evaluacion { get; set; }
    }
}