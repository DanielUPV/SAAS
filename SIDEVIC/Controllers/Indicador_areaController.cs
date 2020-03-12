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
    public class Indicador_areaController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Indicador_area
        public ActionResult Index()
        {
            var indicador_area = db.Indicador_area.Include(i => i.Area).Include(i => i.Indicador);
            return View(indicador_area.ToList());
        }

        // GET: Indicador_area/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador_area indicador_area = db.Indicador_area.Find(id);
            if (indicador_area == null)
            {
                return HttpNotFound();
            }
            return View(indicador_area);
        }

        // GET: Indicador_area/Create
        public ActionResult Create()
        {
            ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre");
            ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre");
            return View();
        }

        // POST: Indicador_area/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_indicador,id_area,id_indicador_area")] Indicador_area indicador_area)
        {
            if (ModelState.IsValid)
            {
                var con = db.Indicador_area.Where(ia => ia.id_indicador == indicador_area.id_indicador && ia.id_area == indicador_area.id_area).ToList().FirstOrDefault();
                if(con == null)
                {
                    db.Indicador_area.Add(indicador_area);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", indicador_area.id_area);
                    ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre", indicador_area.id_indicador);
                    return View(indicador_area);
                }
              
            }

            ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", indicador_area.id_area);
            ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre", indicador_area.id_indicador);
            return View(indicador_area);
        }

        // GET: Indicador_area/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador_area indicador_area = db.Indicador_area.Find(id);
            if (indicador_area == null)
            {
                return HttpNotFound();
            }
            ViewBag.indicador = indicador_area.id_indicador;
            ViewBag.area = indicador_area.id_area;
            ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", indicador_area.id_area);
            ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre", indicador_area.id_indicador);
            return View(indicador_area);
        }

        // POST: Indicador_area/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_indicador,id_area,id_indicador_area")] Indicador_area indicador_area)
        {
            if (ModelState.IsValid)
            {
                var con = db.Indicador_area.Where(ia => ia.id_indicador == indicador_area.id_indicador && ia.id_area == indicador_area.id_area).ToList().FirstOrDefault();
                if (con != null)
                {
                    if(con.id_indicador_area == indicador_area.id_indicador_area)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Flag = "field_error";
                        ViewBag.indicador = indicador_area.id_indicador;
                        ViewBag.area = indicador_area.id_area;
                        ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", indicador_area.id_area);
                        ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre", indicador_area.id_indicador);
                        return View(indicador_area);
                    }
                }

                db.Entry(indicador_area).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_area = new SelectList(db.Area, "id_area", "nombre", indicador_area.id_area);
            ViewBag.id_indicador = new SelectList(db.Indicador, "id_indicador", "nombre", indicador_area.id_indicador);
            return View(indicador_area);
        }


      
        public ActionResult DeleteConfirmed(int id)
        {
            Indicador_area indicador_area = db.Indicador_area.Find(id);
            db.Indicador_area.Remove(indicador_area);
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
