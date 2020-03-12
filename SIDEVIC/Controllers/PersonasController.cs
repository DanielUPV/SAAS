using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIDEVIC.Filtros;
using SIDEVIC.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SIDEVIC.Controllers
{
    [NotLoggedUser]
    [CapturistaPermission]
    public class PersonasController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Personas
        public ActionResult Index()
        {
            return View(db.Persona.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Persona_model persona)
        {
            //Use Namespace called :  System.IO  
            string filename;
            string path;
            if(persona.archivo != null)
            {
                filename = Path.GetFileName(persona.archivo.FileName);
                path = Server.MapPath(Path.Combine("~/Content/Files/img_personas", persona.nombre + filename));
                persona.archivo.SaveAs(path);
                path = "/SIDEVIC/Content/Files/img_personas/" + persona.nombre + filename;
            }
            else
            {
                path = "/SIDEVIC/Content/img/profilepicture.jpg";
            }
            
            var personaX = new Persona();
            personaX.nombre = persona.nombre;
            personaX.cargo = persona.cargo;
            personaX.ruta_foto = path;
            db.Persona.Add(personaX);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }

            ViewBag.nombre = persona.nombre;
            ViewBag.cargo = persona.cargo;
            Persona_model pm = new Persona_model();
            pm.id_persona = persona.id_persona;
            pm.nombre = persona.nombre;
            pm.cargo = persona.cargo;
            pm.ruta = persona.ruta_foto;
            return View(pm);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Persona_model personaX)
        {
            Persona persona = db.Persona.Where(p => p.id_persona == personaX.id_persona).FirstOrDefault();

            if(persona.nombre == null || persona.cargo == null)
            {
                var con = db.Persona.Where(p => p.id_persona == persona.id_persona).ToList().FirstOrDefault();
                if(con != null)
                {
                    ViewBag.Flag = "field_error";

                    ViewBag.nombre = con.nombre;
                    ViewBag.cargo = con.cargo;

                    return View(persona);
                }
            }

            string filename;
            string path;
            if (personaX.archivo != null)
            {
                filename = Path.GetFileName(personaX.archivo.FileName);
         
              
                if (persona.ruta_foto != "/SIDEVIC/Content/Files/img_personas/" + personaX.nombre + filename)
                {
                    path = Server.MapPath(Path.Combine("~/Content/Files/img_personas", persona.nombre + filename));
                    personaX.archivo.SaveAs(path);
                    path = "/SIDEVIC/Content/Files/img_personas/" + personaX.nombre + filename;
                    if(persona.ruta_foto != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(Path.Combine("~/Content/Files/img_personas", persona.ruta_foto))))
                        {
                            if (persona.ruta_foto != "/SIDEVIC/Content/img/profilepicture.jpg")
                            {
                                System.IO.File.Delete(Server.MapPath(Path.Combine("~/Content/Files/img_personas", persona.ruta_foto)));
                            }
                        }
                    }
                    
                }
                else
                {
                    path = persona.ruta_foto;
                }
            }
            else
            {
                path = persona.ruta_foto;
            }
            persona.ruta_foto = path;
            db.Entry(persona).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        // GET: Personas/Delete/5
       
        // POST: Personas/Delete/5

        public ActionResult DeleteConfirmed(int id)
        {
           
            Persona persona = db.Persona.Find(id);
            var users = db.Usuario.Where(u => u.id_persona == persona.id_persona).ToList().FirstOrDefault();
            var areas = db.Area.Where(a => a.id_persona == persona.id_persona).ToList().FirstOrDefault();
            if (users == null && areas == null)
            {
                if (System.IO.File.Exists(Server.MapPath(persona.ruta_foto)))
                {
                    if (persona.ruta_foto != "/Content/img/profilepicture.jpg")
                    {
                        System.IO.File.Delete(Server.MapPath(persona.ruta_foto));
                    }
                }
                db.Persona.Remove(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
