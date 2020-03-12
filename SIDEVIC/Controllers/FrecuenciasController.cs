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
    [AdminPermission]
    public class FrecuenciasController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Frecuencias
        public ActionResult Index()
        {
            return View(db.Frecuencia.ToList());
        }

        // GET: Frecuencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frecuencia frecuencia = db.Frecuencia.Find(id);
            if (frecuencia == null)
            {
                return HttpNotFound();
            }
            return View(frecuencia);
        }

        // GET: Frecuencias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Frecuencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_frecuencia,frecuencia1")] Frecuencia frecuencia)
        {
            if (ModelState.IsValid)
            {
                if (frecuencia.frecuencia1 == null)
                {
                    ViewBag.Flag = "field_error";
                    return View(frecuencia);
                }

                db.Frecuencia.Add(frecuencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(frecuencia);
        }

        // GET: Frecuencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frecuencia frecuencia = db.Frecuencia.Find(id);
            if (frecuencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.frecuencia = frecuencia.frecuencia1;
            return View(frecuencia);
        }

        // POST: Frecuencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_frecuencia,frecuencia1")] Frecuencia frecuencia)
        {
            if (ModelState.IsValid)
            {
                if (frecuencia.frecuencia1 == null)
                {
                    ViewBag.Flag = "field_error";
                    var con = db.Frecuencia.Where(f => f.id_frecuencia == frecuencia.id_frecuencia).ToList().FirstOrDefault();
                    ViewBag.frecuencia = con.frecuencia1;
                    return View(frecuencia);
                }
                db.Entry(frecuencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(frecuencia);
        }

       

        public ActionResult DeleteConfirmed(int id)
        {
            Frecuencia frecuencia = db.Frecuencia.Find(id);
            var indi = db.Indicador.Where(i => i.id_frecuencia == frecuencia.id_frecuencia).ToList().FirstOrDefault();
            if(indi == null)
            {
                db.Frecuencia.Remove(frecuencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
          
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
