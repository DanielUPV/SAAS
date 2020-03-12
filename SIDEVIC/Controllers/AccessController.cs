using SIDEVIC.Filtros;
using SIDEVIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIDEVIC.Controllers
{
   
    public class AccessController : Controller
    {
        snowball_sidevicbdEntities bd = new snowball_sidevicbdEntities();
        [LoggedUser]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login datos)
        {
            if (ModelState.IsValid)
            {
                var validate = bd.Usuario.Where(u => u.username.ToUpper() == datos.usuario.ToUpper()).ToList().FirstOrDefault();
                if(validate != null)
                {
                    if(validate.contraseña == datos.contrasena)
                    {
                        Session["Rol"] = validate.Rol.tipo;
                        if (validate.id_persona != null && validate.id_persona != 1)
                        {
                            Session["Usuario"] = validate.Persona.nombre;
                        }
                        else
                        {
                            Session["Usuario"] = validate.username;
                        }

                        Session["idus"] = validate.id_usuario;
                        var encargado = bd.Area.Where(a => a.id_persona == validate.Persona.id_persona).FirstOrDefault();
                        if(encargado != null)
                        {
                            Session["idarea"] = encargado.id_area;
                        }
                        else
                        {
                            Session["idarea"] = 0;
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                ViewBag.Flag = "user_pass_error";
                return View();
            }
            ViewBag.Flag = "model_error";
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        [NotLoggedUser]
        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Login","Access");
        }
    }
}