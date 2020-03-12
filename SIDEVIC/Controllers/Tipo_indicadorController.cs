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
    public class Tipo_indicadorController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Tipo_indicador
        public ActionResult Index()
        {
            return View(db.Tipo_indicador.ToList());
        }

        // GET: Tipo_indicador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_indicador tipo_indicador = db.Tipo_indicador.Find(id);
            if (tipo_indicador == null)
            {
                return HttpNotFound();
            }
            return View(tipo_indicador);
        }

        // GET: Tipo_indicador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo_indicador/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_indicador,nombre")] Tipo_indicador tipo_indicador)
        {
            if (ModelState.IsValid)
            {
                if(tipo_indicador.nombre == null)
                {
                    ViewBag.Flag = "field_error";
                    return View(tipo_indicador);
                }


                db.Tipo_indicador.Add(tipo_indicador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_indicador);
        }

        // GET: Tipo_indicador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_indicador tipo_indicador = db.Tipo_indicador.Find(id);
            if (tipo_indicador == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = tipo_indicador.nombre;
            return View(tipo_indicador);
        }

        // POST: Tipo_indicador/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_indicador,nombre")] Tipo_indicador tipo_indicador)
        {
            if (ModelState.IsValid)
            {
                if(tipo_indicador.nombre == null)
                {
                    ViewBag.Flag = "field_error";
                    var con = db.Tipo_indicador.Where(t => t.id_tipo_indicador == tipo_indicador.id_tipo_indicador).ToList().FirstOrDefault();
                    ViewBag.nombre = con.nombre;
                    return View(tipo_indicador);
                }
                db.Entry(tipo_indicador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_indicador);
        }

        // GET: Tipo_indicador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_indicador tipo_indicador = db.Tipo_indicador.Find(id);
            if (tipo_indicador == null)
            {
                return HttpNotFound();
            }
            return View(tipo_indicador);
        }

        // POST: Tipo_indicador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_indicador tipo_indicador = db.Tipo_indicador.Find(id);
            db.Tipo_indicador.Remove(tipo_indicador);
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
