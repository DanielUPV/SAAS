using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIDEVIC.Filtros;
using SIDEVIC.Models;

namespace SIDEVIC.Controllers
{
    [NotLoggedUser]
    [CapturistaPermission]
    public class TemasController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Temas
        public ActionResult Index()
        {
            var tema = db.Tema.Include(t => t.Eje);
            return View(tema.ToList());
        }

        // GET: Temas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return HttpNotFound();
            }
            return View(tema);
        }

        // GET: Temas/Create
        public ActionResult Create()
        {
            ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre");
            return View();
        }

        // POST: Temas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tema,nombre,id_eje")] Tema tema)
        {
            if (ModelState.IsValid)
            {
                if (tema.nombre == null)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", tema.id_eje);
                    return View(tema);
                }


                db.Tema.Add(tema);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", tema.id_eje);
            return View(tema);
        }

        // GET: Temas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = tema.nombre;
            ViewBag.eje = tema.id_eje;
            ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", tema.id_eje);
            return View(tema);
        }

        // POST: Temas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tema,nombre,id_eje")] Tema tema)
        {
            if (ModelState.IsValid)
            {
                if (tema.nombre == null)
                {
                    ViewBag.Flag = "field_error";
                    var con = db.Tema.Where(t => t.id_tema == tema.id_tema).ToList().FirstOrDefault();
                    ViewBag.nombre = con.nombre;
                    ViewBag.eje = con.id_tema;

                    ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", tema.id_eje);
                    return View(tema);
                }

                db.Entry(tema).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", tema.id_eje);
            return View(tema);
        }

        // GET: Temas/Delete/5
       

        public ActionResult DeleteConfirmed(int id)
        {
            Tema tema = db.Tema.Find(id);
            var indicador = db.Indicador.Where(i => i.id_tema == tema.id_tema).ToList().FirstOrDefault();

            if(indicador == null)
            {
                db.Tema.Remove(tema);
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
