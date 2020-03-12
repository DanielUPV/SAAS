using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIDEVIC.Models;

namespace SIDEVIC.Controllers
{
    public class ArchivoesController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Archivoes
        public ActionResult Index()
        {
            var archivo = db.Archivo.Include(a => a.Indicador);
            return View(archivo.ToList());
        }

        // GET: Archivoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = db.Archivo.Find(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // GET: Archivoes/Create
        public ActionResult Create()
        {
            ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre");
            return View();
        }

        // POST: Archivoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_archivo,id_indicador,id_ruta")] Archivo archivo)
        {
            if (ModelState.IsValid)
            {
                db.Archivo.Add(archivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre", archivo.id_indicador);
            return View(archivo);
        }

        // GET: Archivoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = db.Archivo.Find(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre", archivo.id_indicador);
            return View(archivo);
        }

        // POST: Archivoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_archivo,id_indicador,id_ruta")] Archivo archivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(archivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre", archivo.id_indicador);
            return View(archivo);
        }

        // GET: Archivoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = db.Archivo.Find(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // POST: Archivoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Archivo archivo = db.Archivo.Find(id);
            db.Archivo.Remove(archivo);
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
