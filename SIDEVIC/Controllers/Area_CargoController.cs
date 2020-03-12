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
    public class Area_CargoController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Area_Cargo
        public ActionResult Index()
        {
            var area_Cargo = db.Area_Cargo.Include(a => a.Persona);
            return View(area_Cargo.ToList());
        }

        // GET: Area_Cargo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area_Cargo area_Cargo = db.Area_Cargo.Find(id);
            if (area_Cargo == null)
            {
                return HttpNotFound();
            }
            return View(area_Cargo);
        }

        // GET: Area_Cargo/Create
        public ActionResult Create()
        {
            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre");
            return View();
        }

        // POST: Area_Cargo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_areas,nombre,id_persona")] Area_Cargo area_Cargo)
        {
            if (ModelState.IsValid)
            {
                if (area_Cargo.nombre == null)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre",area_Cargo.id_persona);
                    return View(area_Cargo);
                }


                db.Area_Cargo.Add(area_Cargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", area_Cargo.id_persona);
            return View(area_Cargo);
        }

        // GET: Area_Cargo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area_Cargo area_Cargo = db.Area_Cargo.Find(id);
            if (area_Cargo == null)
            {
                return HttpNotFound();
            }
            ViewBag.nombre = area_Cargo.nombre;
            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", area_Cargo.id_persona);
            return View(area_Cargo);
        }

        // POST: Area_Cargo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_areas,nombre,id_persona")] Area_Cargo area_Cargo)
        {
            if (ModelState.IsValid)
            {
                if(area_Cargo.nombre == null)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.nombre = area_Cargo.nombre;
                    ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", area_Cargo.id_persona);
                    return View(area_Cargo);
                }

                db.Entry(area_Cargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", area_Cargo.id_persona);
            return View(area_Cargo);
        }

        // GET: Area_Cargo/Delete/5
        
        public ActionResult DeleteConfirmed(int id)
        {
            Area_Cargo area_Cargo = db.Area_Cargo.Find(id);
            db.Area_Cargo.Remove(area_Cargo);
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
