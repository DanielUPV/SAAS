using SIDEVIC.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIDEVIC.Controllers
{
    public class HomeController : Controller
    {
        [NotLoggedUser]
        public ActionResult Index()
        {
            return View();
        }


       
    }
}