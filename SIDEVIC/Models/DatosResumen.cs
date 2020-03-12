using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIDEVIC.Models
{
    public class DatosResumen
    {
        public string titular { get; set; }
        public string ubicacion { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public List<areasa_cargo> areas { get; set; }
        public int cantidad_gestion { get; set; }
        public int cantidad_desempeño { get; set; }
        public int cantidad_v_g { get; set; }
        public int cantidad_a_g { get; set; }
        public int cantidad_r_g { get; set; }
        public int cantidad_v_d { get; set; }
        public int cantidad_a_d { get; set; }
        public int cantidad_r_d { get; set; }
        public List<IndResumen> lista_gestion { get; set; }
        public List<IndResumen> lista_desempeño { get; set; }
        public string programas_pmd { get; set; }
    }
}