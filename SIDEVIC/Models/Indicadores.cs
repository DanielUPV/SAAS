using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIDEVIC.Models
{
    public class Indicadores
    {
        public string eje { get; set; }
        public string tema { get; set; }
        public string indicador { get; set; }
        public string evaluacion { get; set; }
        public string comentarios { get; set; }
        public string accion { get; set; }
        public string responsable { get; set; }
        public string inicio { get; set; }
        public string fin { get; set; }
        public string presupuesto { get; set; }
        public string producto { get; set; }
    }
}