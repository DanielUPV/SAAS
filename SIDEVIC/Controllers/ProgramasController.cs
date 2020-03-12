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
    public class ProgramasController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Programas
        public ActionResult Index()
        {
            var programa = db.Programa.Include(p => p.Area).Include(p => p.Eje).Include(p => p.Tipo_presupuesto);
            return View(programa.ToList());
        }

        // GET: Programas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programa programa = db.Programa.Find(id);
            if (programa == null)
            {
                return HttpNotFound();
            }
            return View(programa);
        }

        // GET: Programas/Create
        public ActionResult Create()
        {
            ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre");
            ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre");
            ViewBag.id_tipo_presupuesto = new SelectList(db.Tipo_presupuesto, "id_tipo_presupuesto", "tipo");
            return View();
        }

        // POST: Programas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_programa,nombre,objetivo,problematica,beneficiarios,id_eje,estrategias,lineas_accion,id_area,presupuesto,porcentaje_1,procentaje_2,procentaje_3,procentaje_4,id_tipo_presupuesto")] Programa programa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Programa.Add(programa);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", programa.id_area);
                    ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", programa.id_eje);
                    ViewBag.id_tipo_presupuesto = new SelectList(db.Tipo_presupuesto, "id_tipo_presupuesto", "tipo", programa.id_tipo_presupuesto);
                    return View(programa);
                }
                
            }

            ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", programa.id_area);
            ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", programa.id_eje);
            ViewBag.id_tipo_presupuesto = new SelectList(db.Tipo_presupuesto, "id_tipo_presupuesto", "tipo", programa.id_tipo_presupuesto);
            return View(programa);
        }

        // GET: Programas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Programa programa = db.Programa.Find(id);
            if (programa == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", programa.id_area);
            ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", programa.id_eje);
            ViewBag.id_tipo_presupuesto = new SelectList(db.Tipo_presupuesto, "id_tipo_presupuesto", "tipo", programa.id_tipo_presupuesto);
            return View(programa);
        }

        // POST: Programas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_programa,nombre,objetivo,problematica,beneficiarios,id_eje,estrategias,lineas_accion,id_area,presupuesto,porcentaje_1,procentaje_2,procentaje_3,procentaje_4,id_tipo_presupuesto")] Programa programa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(programa).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", programa.id_area);
                    ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", programa.id_eje);
                    ViewBag.id_tipo_presupuesto = new SelectList(db.Tipo_presupuesto, "id_tipo_presupuesto", "tipo", programa.id_tipo_presupuesto);
                    return View(programa);
                }
                
            }
            ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", programa.id_area);
            ViewBag.id_eje = new SelectList(db.Eje, "id_eje", "nombre", programa.id_eje);
            ViewBag.id_tipo_presupuesto = new SelectList(db.Tipo_presupuesto, "id_tipo_presupuesto", "tipo", programa.id_tipo_presupuesto);
            return View(programa);
        }

        // GET: Programas/Delete/5
       
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Programa programa = db.Programa.Find(id);
                db.Programa.Remove(programa);
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
