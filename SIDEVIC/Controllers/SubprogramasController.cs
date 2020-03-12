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
    public class SubprogramasController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Subprogramas
        public ActionResult Index()
        {
            var subprograma = db.Subprograma.Include(s => s.Programa);
            return View(subprograma.ToList());
        }

        // GET: Subprogramas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subprograma subprograma = db.Subprograma.Find(id);
            if (subprograma == null)
            {
                return HttpNotFound();
            }
            return View(subprograma);
        }

        // GET: Subprogramas/Create
        public ActionResult Create()
        {
            ViewBag.id_programa = new SelectList(db.Programa, "id_programa", "nombre");
            return View();
        }

        // POST: Subprogramas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_sub,nombre,descripcion,meta,monto,id_programa")] Subprograma subprograma)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Subprograma.Add(subprograma);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_programa = new SelectList(db.Programa, "id_programa", "nombre", subprograma.id_programa);
                    return View(subprograma);
                }
               
            }

            ViewBag.id_programa = new SelectList(db.Programa, "id_programa", "nombre", subprograma.id_programa);
            return View(subprograma);
        }

        // GET: Subprogramas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subprograma subprograma = db.Subprograma.Find(id);
            if (subprograma == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_programa = new SelectList(db.Programa, "id_programa", "nombre", subprograma.id_programa);
            return View(subprograma);
        }

        // POST: Subprogramas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_sub,nombre,descripcion,meta,monto,id_programa")] Subprograma subprograma)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(subprograma).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_programa = new SelectList(db.Programa, "id_programa", "nombre", subprograma.id_programa);
                    return View(subprograma);
                }
            }
            ViewBag.id_programa = new SelectList(db.Programa, "id_programa", "nombre", subprograma.id_programa);
            return View(subprograma);
        }

        // GET: Subprogramas/Delete/5
        
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Subprograma subprograma = db.Subprograma.Find(id);
                db.Subprograma.Remove(subprograma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
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
