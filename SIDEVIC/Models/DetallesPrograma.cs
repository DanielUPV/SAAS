using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIDEVIC.Models
{
    public class DetallesPrograma
    {
        public Programa programa { get; set; }
        public List<Subprograma> subprogramas { get; set; }
    }
}