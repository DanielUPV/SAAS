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
    public class EjesController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Ejes
        public ActionResult Index()
        {
            return View(db.Eje.ToList());
        }

        // GET: Ejes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eje eje = db.Eje.Find(id);
            if (eje == null)
            {
                return HttpNotFound();
            }
            return View(eje);
        }

        // GET: Ejes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ejes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_eje,nombre,informacion")] Eje eje)
        {
            if (ModelState.IsValid)
            {
                if (eje.nombre == null || eje.informacion == null)
                {
                    ViewBag.Flag = "field_error";
                    return View(eje);
                }


                db.Eje.Add(eje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eje);
        }

        // GET: Ejes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eje eje = db.Eje.Find(id);
            if (eje == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = eje.nombre;
            ViewBag.informacion = eje.informacion;
            ViewBag.informacionS = eje.informacion; 
            return View(eje);
        }

        // POST: Ejes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_eje,nombre,informacion")] Eje eje)
        {
            if (ModelState.IsValid)
            {
                if (eje.nombre == null || eje.informacion == null)
                {
                    ViewBag.Flag = "field_error";
                    var con = db.Eje.Where(e => e.id_eje == eje.id_eje).ToList().FirstOrDefault();
                    ViewBag.nombre = con.nombre;
                    ViewBag.informacion = con.informacion;
                    ViewBag.informacionS = eje.informacion;
                    return View(eje);
                }

                db.Entry(eje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eje);
        }

    
        // POST: Ejes/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Eje eje = db.Eje.Find(id);
            db.Eje.Remove(eje);
            db.SaveChanges();
            return RedirectToAction("Index");
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
