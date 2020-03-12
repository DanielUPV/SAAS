using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIDEVIC.Models
{
    public class Login
    {
        public string usuario { get; set; }
        public string contrasena { get; set; }
    }
}