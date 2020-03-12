using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    public class IndicadorsController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();
        public ActionResult Requerimientos(int? id = 0)
        {
            if(id != 0)
            {
                ViewBag.id = id;
                ViewBag.nombre = db.Indicador.Where(i => i.id_indicador == id).FirstOrDefault().nombre;
                return View(db.Requerimientos.Where(r => r.id_indicador == id).ToList());
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult CreateRequerimiento(int? id = 0)
        {
            if(id != 0)
            {
                ViewBag.id = id;
                var cons = db.Indicador.Where(i => i.id_indicador == id).FirstOrDefault();
                ViewBag.nombre = cons.nombre;
                ViewBag.tipo = cons.Tipo_indicador.nombre;
                return View();
            }
            else
            {
                return HttpNotFound();
            }


        }

        public ActionResult CreateRequerimientoPost(int? id = 0, string nombre = null)
        {
            if(id != 0 && nombre != null)
            {
                try { 
                    Requerimientos r = new Requerimientos();
                    r.nombre = nombre;
                    r.id_indicador = (int)id;
                    db.Requerimientos.Add(r);
                    db.SaveChanges();

                    var req = db.Requerimientos.Where(re => re.id_indicador == r.id_indicador && re.nombre == r.nombre).FirstOrDefault();
                    var indt = db.Indicador.Where(i => i.id_indicador == id).FirstOrDefault();
                    ViewBag.tipo = indt.Tipo_indicador.nombre;
                    if (indt.Tipo_indicador.nombre == "Gestion")
                    {
                        var reqC = new Requerimientos_cumple();
                        reqC.id_requerimientos = req.id_requerimientos;
                        reqC.cumple = 0;
                        db.Requerimientos_cumple.Add(reqC);
                        db.SaveChanges();
                    }
                    else
                    {
                        var reqC = new Requerimientos_desempeño();
                        reqC.id_requerimientos = req.id_requerimientos;
                        reqC.cantidad = 0;
                        db.Requerimientos_desempeño.Add(reqC);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Requerimientos", "Indicadors", new { id = id });

                }
                catch (Exception e)
                {
                    return RedirectToAction("Requerimientos", "Indicadors", new { id = id });

                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult DeleteRequerimiento(int? id = 0)
        {
            Requerimientos req = db.Requerimientos.Find(id);

            try
            {
                if (req.Indicador.Tipo_indicador.nombre == "Gestion")
                {
                    var reqC = new Requerimientos_cumple();
                    reqC = db.Requerimientos_cumple.Where(r => r.id_requerimientos == id).FirstOrDefault();
                    db.Requerimientos_cumple.Remove(reqC);
                    db.SaveChanges();
                }
                else
                {
                    var reqD = new Requerimientos_desempeño();
                    reqD = db.Requerimientos_desempeño.Where(r => r.id_requerimientos == id).FirstOrDefault();
                    db.Requerimientos_desempeño.Remove(reqD);
                    db.SaveChanges();
                }
                db.Requerimientos.Remove(req);
                db.SaveChanges();
            }
            catch (Exception e) { 
            }
               
            return RedirectToAction("Requerimientos", "Indicadors", new { id = req.id_indicador });
            

        }

        public ActionResult EditRequerimiento(int? id = 0)
        {
            if (id != 0)
            {
                var cons = db.Requerimientos.Where(r => r.id_requerimientos == id).FirstOrDefault();
                ViewBag.id = cons.id_indicador;
                ViewBag.tipo = cons.Indicador.Tipo_indicador.nombre;


                var requerimiento = db.Requerimientos.Find(id);
                return View(requerimiento);
            }
            else
            {
                return HttpNotFound();
            }
        }
            
        [HttpPost]
        public ActionResult EditRequerimiento([Bind(Include = "id_requerimientos,id_indicador,nombre")] Requerimientos requerimientos)
        {
            db.Entry(requerimientos).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Requerimientos", "Indicadors", new { id = requerimientos.id_indicador });

        }

        // GET: Indicadors
        public ActionResult Index()
        {
            var indicador = db.Indicador.Include(i => i.Frecuencia).Include(i => i.Subprograma).Include(i => i.Tipo_indicador).Include(i => i.Tema);
            return View(indicador.ToList());
        }

        // GET: Indicadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicador.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            return View(indicador);
        }

        // GET: Indicadors/Create
        public ActionResult Create()
        {
            ViewBag.id_frecuencia = new SelectList(db.Frecuencia, "id_frecuencia", "frecuencia1");
            ViewBag.id_subprograma = new SelectList(db.Subprograma, "id_sub", "nombre");
            ViewBag.id_tipo_indicador = new SelectList(db.Tipo_indicador, "id_tipo_indicador", "nombre");
            ViewBag.id_tema = new SelectList(db.Tema, "id_tema", "nombre");
            return View();
        }

        // POST: Indicadors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_indicador,nombre,id_tipo_indicador,id_tema,descripcion,id_frecuencia,fecha_inicio_año,diagnostico,pmd,id_subprograma,formula,dimension,medio_verificacion,meta,r_inaceptable,r_bajo_aceptable,r_aceptable")] Indicador indicador)
        {
            if (ModelState.IsValid)
            {
                if (indicador.nombre == null || indicador.descripcion == null || indicador.fecha_inicio_año ==  null || indicador.diagnostico == null || indicador.pmd == null || indicador.formula == null || indicador.dimension == null || indicador.medio_verificacion == null || indicador.meta == null || indicador.r_inaceptable == null || indicador.r_bajo_aceptable == null || indicador.r_bajo_aceptable == null)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_frecuencia = new SelectList(db.Frecuencia, "id_frecuencia", "frecuencia1");
                    ViewBag.id_subprograma = new SelectList(db.Subprograma, "id_sub", "nombre");
                    ViewBag.id_tipo_indicador = new SelectList(db.Tipo_indicador, "id_tipo_indicador", "nombre");
                    ViewBag.id_tema = new SelectList(db.Tema, "id_tema", "nombre");
                    return View(indicador);
                }
                db.Indicador.Add(indicador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_frecuencia = new SelectList(db.Frecuencia, "id_frecuencia", "frecuencia1", indicador.id_frecuencia);
            ViewBag.id_subprograma = new SelectList(db.Subprograma, "id_sub", "nombre", indicador.id_subprograma);
            ViewBag.id_tipo_indicador = new SelectList(db.Tipo_indicador, "id_tipo_indicador", "nombre", indicador.id_tipo_indicador);
            ViewBag.id_tema = new SelectList(db.Tema, "id_tema", "nombre", indicador.id_tema);
            return View(indicador);
        }

        // GET: Indicadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indicador indicador = db.Indicador.Find(id);
            if (indicador == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_frecuencia = new SelectList(db.Frecuencia, "id_frecuencia", "frecuencia1", indicador.id_frecuencia);
            ViewBag.id_subprograma = new SelectList(db.Subprograma, "id_sub", "nombre", indicador.id_subprograma);
            ViewBag.id_tipo_indicador = new SelectList(db.Tipo_indicador, "id_tipo_indicador", "nombre", indicador.id_tipo_indicador);
            ViewBag.id_tema = new SelectList(db.Tema, "id_tema", "nombre", indicador.id_tema);
            return View(indicador);
        }

        // POST: Indicadors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_indicador,nombre,id_tipo_indicador,id_tema,descripcion,id_frecuencia,fecha_inicio_año,diagnostico,pmd,id_subprograma,formula,dimension,medio_verificacion,meta,r_inaceptable,r_bajo_aceptable,r_aceptable")] Indicador indicador)
        {
            if (ModelState.IsValid)
            {
                if (indicador.nombre == null || indicador.descripcion == null || indicador.fecha_inicio_año == null || indicador.diagnostico == null || indicador.pmd == null || indicador.formula == null || indicador.dimension == null || indicador.medio_verificacion == null || indicador.meta == null || indicador.r_inaceptable == null || indicador.r_bajo_aceptable == null || indicador.r_bajo_aceptable == null)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_frecuencia = new SelectList(db.Frecuencia, "id_frecuencia", "frecuencia1");
                    ViewBag.id_subprograma = new SelectList(db.Subprograma, "id_sub", "nombre");
                    ViewBag.id_tipo_indicador = new SelectList(db.Tipo_indicador, "id_tipo_indicador", "nombre");
                    ViewBag.id_tema = new SelectList(db.Tema, "id_tema", "nombre");
                    return View(indicador);
                }
                db.Entry(indicador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_frecuencia = new SelectList(db.Frecuencia, "id_frecuencia", "frecuencia1", indicador.id_frecuencia);
            ViewBag.id_subprograma = new SelectList(db.Subprograma, "id_sub", "nombre", indicador.id_subprograma);
            ViewBag.id_tipo_indicador = new SelectList(db.Tipo_indicador, "id_tipo_indicador", "nombre", indicador.id_tipo_indicador);
            ViewBag.id_tema = new SelectList(db.Tema, "id_tema", "nombre", indicador.id_tema);
            return View(indicador);
        }

        // GET: Indicadors/Delete/5
        
        public ActionResult DeleteConfirmed(int id)
        {
            Indicador indicador = db.Indicador.Find(id);
            try
            {
                db.Indicador.Remove(indicador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
           
        }

        public ActionResult Documento(int? id = 0)
        {
            if(id != 0)
            {
                docModel dm = new docModel();
                var cons = db.Archivo.Where(ar => ar.id_indicador == id).FirstOrDefault();
                if (cons != null)
                {
                    dm.id_indicador = cons.id_indicador;
                    dm.nombre_indicador = db.Indicador.Where(i => i.id_indicador == id).FirstOrDefault().nombre;
                    ViewBag.ruta = cons.id_ruta;
                    return View(dm);
                }
                var indi = db.Indicador.Where(i => i.id_indicador == id).FirstOrDefault();
                dm.id_indicador = indi.id_indicador;
                dm.nombre_indicador = indi.nombre;
                return View(dm);
                
                
            }
            return HttpNotFound();

        }
        [HttpPost]
        public ActionResult Documento(docModel dm)
        {
            //Use Namespace called :  System.IO  
            string filename;
            string path;
            if (dm.archivo != null)
            {
                filename = Path.GetFileName(dm.archivo.FileName);
                path = Server.MapPath(Path.Combine("~/Content/Files/Docs", dm.nombre_indicador + Path.GetExtension(dm.archivo.FileName)));
                dm.archivo.SaveAs(path);
                path = "/SIDEVIC/Content/Files/docs/" + dm.nombre_indicador + Path.GetExtension(dm.archivo.FileName);
            }
            else
            {
                path = "/SIDEVIC/Content/Files/Docs/ejemplo.jpg";
            }

            Archivo ar = new Archivo();
            var cons = db.Archivo.Where(a => a.id_indicador == dm.id_indicador).FirstOrDefault();
            if (cons != null)
            {
                ar = cons;
                ar.id_ruta = path;
                db.Entry(ar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            ar.id_indicador = dm.id_indicador;
            ar.id_ruta = path;
            db.Archivo.Add(ar);
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
