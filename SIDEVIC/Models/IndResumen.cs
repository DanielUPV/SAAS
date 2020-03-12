using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIDEVIC.Models
{
    public class IndResumen
    {
        public string eje { get; set; }
        public string tema { get; set; }
        public string indicador { get; set; }
        public int total { get; set; }
        public int verdes { get; set; }
        public int amarillos { get; set; }
        public int rojos { get; set; }
        public string pmd { get; set; }
    }
}