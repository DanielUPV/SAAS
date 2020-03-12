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
    public class AreasController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Areas
        public ActionResult Index()
        {
            var area = db.Area.Include(a => a.Persona);
            return View(area.ToList());
        }

        // GET: Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Area.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre");
            return View();
        }

        // POST: Areas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_area,nombre,direccion,id_persona,informacion,correo,telefono")] Area area)
        {
            if (ModelState.IsValid)
            {
                if (area.nombre == null || area.direccion == null || area.informacion == null || area.correo == null || area.telefono == null)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre",area.id_persona);
                    return View(area);
                }

                db.Area.Add(area);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", area.id_persona);
            return View(area);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Area.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }

            ViewBag.nombre = area.nombre;
            ViewBag.direccion = area.direccion;
            ViewBag.persona_val = area.id_persona;
            ViewBag.informacion = area.informacion;
            ViewBag.correo = area.correo;
            ViewBag.telefono = area.telefono;

            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", area.id_persona);
            return View(area);
        }

        // POST: Areas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_area,nombre,direccion,id_persona,informacion,correo,telefono")] Area area)
        {
            if (ModelState.IsValid)
            {
                if (area.nombre == null || area.direccion == null || area.informacion ==null || area.correo == null || area.telefono == null)
                {
                    ViewBag.Flag = "field_error";
                    var con = db.Area.Where(a => a.id_area == area.id_area).ToList().FirstOrDefault();
                    ViewBag.nombre = con.nombre;
                    ViewBag.direccion = con.direccion;
                    ViewBag.persona_val = con.id_persona;
                    ViewBag.informacion = con.informacion;
                    ViewBag.correo = con.correo;
                    ViewBag.telefono = con.telefono;

                    ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", area.id_persona);
                    return View(area);
                }

                db.Entry(area).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", area.id_persona);
            return View(area);
        }

       
        public ActionResult DeleteConfirmed(int id)
        {
            Area area = db.Area.Find(id);
            var indicador_area = db.Indicador_area.Where(ia => ia.id_area == area.id_area).ToList().FirstOrDefault();
            if(indicador_area == null)
            {
                db.Area.Remove(area);
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
