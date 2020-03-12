using SIDEVIC.Filtros;
using SIDEVIC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIDEVIC.Controllers
{
    [NotLoggedUser]
    [DirectorPermission]
    public class IndicadoresDirectorController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("/Content/Files"), filename);
            file.SaveAs(path);
            return View();
        }

        // GET: IndicadoresAlcalde
        public ActionResult ProgramasMejora(int? id = 0, string inicio = null, string fin = null)
        {
            var area = new Area();
            area.id_area = 0;
            area.nombre = "--Seleccione un area--";
            var areas = new List<Area>();
            areas.Add(area);

            var indis = new List<Indicadores>();
            Indicadores indi;
            var indicadoresC = db.Evaluacion.Where(ev => ev.evaluacion1 == "AMARILLO" || ev.evaluacion1 == "ROJO").ToList();
            ViewBag.id = id;
            ViewBag.inicio = inicio;
            ViewBag.fin = fin;

            int idarea = (int)Session["idarea"];
            if (id == 0 && inicio == null && fin == null)
            {
                var areasI = db.Indicador_area.Where(ia => ia.id_area == idarea).ToList();
                if (areasI != null)
                {
                    foreach (var ai in areasI)
                    {
                        indi = new Indicadores();
                        var ev = db.Evaluacion.Where(evx => evx.Indicador.id_indicador == ai.Indicador.id_indicador && (evx.evaluacion1 == "AMARILLO" || evx.evaluacion1 == "ROJO")).ToList().FirstOrDefault();
                        if (ev != null)
                        {
                            indi.eje = ai.Indicador.Tema.Eje.nombre;
                            indi.tema = ai.Indicador.Tema.nombre;
                            indi.indicador = ai.Indicador.nombre;
                            indi.evaluacion = ev.evaluacion1;
                            indi.comentarios = "";
                            indi.accion = "";
                            indi.responsable = "";
                            indi.inicio = ev.fecha.Day.ToString() + "/" + ev.fecha.Month.ToString() + "/" + ev.fecha.Year.ToString();
                            indi.fin = ev.fecha.Day.ToString() + "/" + ev.fecha.Month.ToString() + "/" + ev.fecha.Year.ToString();
                            indi.presupuesto = "";
                            indi.producto = "";
                            indis.Add(indi);
                        }

                    }
                }

                foreach (var ar in db.Area.ToList())
                {
                    areas.Add(ar);
                }
                ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
                return View(indis);
            }

            if (id != 0)
            {
                var areasI = db.Indicador_area.Where(ia => ia.id_area == id).ToList();
                if (areasI != null)
                {
                    foreach (var ai in areasI)
                    {
                        indi = new Indicadores();
                        var ev = db.Evaluacion.Where(evx => evx.Indicador.id_indicador == ai.Indicador.id_indicador && (evx.evaluacion1 == "AMARILLO" || evx.evaluacion1 == "ROJO")).ToList().FirstOrDefault();
                        if (ev != null)
                        {
                            indi.eje = ai.Indicador.Tema.Eje.nombre;
                            indi.tema = ai.Indicador.Tema.nombre;
                            indi.indicador = ai.Indicador.nombre;
                            indi.evaluacion = ev.evaluacion1;
                            indi.comentarios = "";
                            indi.accion = "";
                            indi.responsable = "";
                            indi.inicio = ev.fecha.Day.ToString() + "/" + ev.fecha.Month.ToString() + "/" + ev.fecha.Year.ToString();
                            indi.fin = ev.fecha.Day.ToString() + "/" + ev.fecha.Month.ToString() + "/" + ev.fecha.Year.ToString();
                            indi.presupuesto = "";
                            indi.producto = "";
                            indis.Add(indi);
                        }

                    }
                }
                if (inicio != null && fin != null)
                {
                    indis = indis.Where(indsw => DateTime.Parse(indsw.inicio) >= DateTime.Parse(inicio) && DateTime.Parse(indsw.fin) <= DateTime.Parse(fin)).ToList();
                }
                foreach (var ar in db.Area.ToList())
                {
                    areas.Add(ar);
                }
                ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
                return View(indis);
            }

            foreach (var ind in indicadoresC)
            {
                indi = new Indicadores();
                indi.eje = ind.Indicador.Tema.Eje.nombre;
                indi.tema = ind.Indicador.Tema.nombre;
                indi.indicador = ind.Indicador.nombre;
                indi.evaluacion = ind.evaluacion1;
                indi.comentarios = "";
                indi.accion = "";
                indi.responsable = "";
                indi.inicio = ind.fecha.Day.ToString() + "/" + ind.fecha.Month.ToString() + "/" + ind.fecha.Year.ToString();
                indi.fin = ind.fecha.Day.ToString() + "/" + ind.fecha.Month.ToString() + "/" + ind.fecha.Year.ToString();
                indi.presupuesto = "";
                indi.producto = "";
                indis.Add(indi);
            }
            if (inicio != null && fin != null)
            {
                indis = indis.Where(indsw => DateTime.Parse(indsw.inicio) >= DateTime.Parse(inicio) && DateTime.Parse(indsw.fin) <= DateTime.Parse(fin)).ToList();
            }
            foreach (var ar in db.Area.ToList())
            {
                areas.Add(ar);
            }
            ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
            return View(indis);

        }

        public ActionResult TableroControl(int? id = 0, string inicio = null, string fin = null)
        {
            var area = new Area();
            area.id_area = 0;
            area.nombre = "--Seleccione un area--";
            var areas = new List<Area>();
            areas.Add(area);

            var indisT = new List<IndicadoresTablero>();
            IndicadoresTablero indiT;
            var evInd = db.Evaluacion.ToList();
            ViewBag.id = id;
            ViewBag.inicio = inicio;
            ViewBag.fin = fin;
            int idarea = (int)Session["idarea"];
            if (id == 0 && inicio == null && fin == null)
            {
                foreach (var evi in evInd)
                {
                    var arei = db.Indicador_area.Where(ar => ar.id_indicador == evi.Indicador.id_indicador && ar.id_area == idarea).ToList().FirstOrDefault();
                    indiT = new IndicadoresTablero();
                    indiT.id = evi.id_evaluacion;
                    indiT.eje = evi.Indicador.Tema.Eje.nombre;
                    indiT.tema = evi.Indicador.Tema.nombre;
                    indiT.indicador = evi.Indicador.nombre;
                    indiT.area = arei.Area.nombre;
                    indiT.tipo = evi.Indicador.Tipo_indicador.nombre;
                    indiT.inicio = evi.fecha.Day.ToString() + "/" + evi.fecha.Month.ToString() + "/" + evi.fecha.Year.ToString();
                    indiT.fin = evi.fecha.Day.ToString() + "/" + evi.fecha.Month.ToString() + "/" + evi.fecha.Year.ToString();
                    indiT.evaluacion = evi.evaluacion1;

                    indisT.Add(indiT);
                }

                foreach (var ar in db.Area.ToList())
                {
                    areas.Add(ar);
                }
                ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
                return View(indisT);
            }
            if (id != 0)
            {
                var arei = db.Indicador_area.Where(ar => ar.id_area == id).ToList();

                foreach (var evi in arei)
                {
                    var evind = db.Evaluacion.Where(e => e.id_indicador == evi.id_indicador).ToList().FirstOrDefault();
                    if (evind != null)
                    {
                        indiT = new IndicadoresTablero();
                        indiT.id = evind.id_evaluacion;
                        indiT.eje = evi.Indicador.Tema.Eje.nombre;
                        indiT.tema = evi.Indicador.Tema.nombre;
                        indiT.indicador = evi.Indicador.nombre;
                        indiT.area = evi.Area.nombre;
                        indiT.tipo = evi.Indicador.Tipo_indicador.nombre;
                        indiT.inicio = evind.fecha.Day.ToString() + "/" + evind.fecha.Month.ToString() + "/" + evind.fecha.Year.ToString();
                        indiT.fin = evind.fecha.Day.ToString() + "/" + evind.fecha.Month.ToString() + "/" + evind.fecha.Year.ToString();
                        indiT.evaluacion = evind.evaluacion1;

                        indisT.Add(indiT);
                    }

                }
                if (inicio != null && fin != null)
                {
                    indisT = indisT.Where(indsw => DateTime.Parse(indsw.inicio) >= DateTime.Parse(inicio) && DateTime.Parse(indsw.fin) <= DateTime.Parse(fin)).ToList();
                }
                foreach (var ar in db.Area.ToList())
                {
                    areas.Add(ar);
                }
                ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
                return View(indisT);
            }
            foreach (var evi in evInd)
            {
                var arei = db.Indicador_area.Where(ar => ar.id_indicador == evi.Indicador.id_indicador).ToList().FirstOrDefault();
                indiT = new IndicadoresTablero();
                indiT.id = evi.id_evaluacion;
                indiT.eje = evi.Indicador.Tema.Eje.nombre;
                indiT.tema = evi.Indicador.Tema.nombre;
                indiT.indicador = evi.Indicador.nombre;
                indiT.area = arei.Area.nombre;
                indiT.tipo = evi.Indicador.Tipo_indicador.nombre;
                indiT.inicio = evi.fecha.Day.ToString() + "/" + evi.fecha.Month.ToString() + "/" + evi.fecha.Year.ToString();
                indiT.fin = evi.fecha.Day.ToString() + "/" + evi.fecha.Month.ToString() + "/" + evi.fecha.Year.ToString();
                indiT.evaluacion = evi.evaluacion1;

                indisT.Add(indiT);
            }
            if (inicio != null && fin != null)
            {
                indisT = indisT.Where(indsw => DateTime.Parse(indsw.inicio) >= DateTime.Parse(inicio) && DateTime.Parse(indsw.fin) <= DateTime.Parse(fin)).ToList();
            }
            foreach (var ar in db.Area.ToList())
            {
                areas.Add(ar);
            }

            ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
            return View(indisT);
        }

        public ActionResult Resumen(int? id = 0, string inicio = null, string fin = null)
        {
            var area = new Area();
            area.id_area = 0;
            area.nombre = "--Seleccione un area--";
            var areas = new List<Area>();
            areas.Add(area);
            IndResumen lista_d;
            IndResumen lista_g;
            var datos = new DatosResumen();
            datos.lista_desempeño = new List<IndResumen>();
            datos.lista_gestion = new List<IndResumen>();
            ViewBag.id = id;
            ViewBag.inicio = inicio;
            ViewBag.fin = fin;
            int total_g = 0;
            int total_d = 0;
            int v_g = 0;
            int a_g = 0;
            int r_g = 0;
            int v_d = 0;
            int a_d = 0;
            int r_d = 0;

            ViewBag.ruta = "/SIDEVIC/Content/img/profilepicture.jpg";


            if (id != 0 && inicio != null && fin != null)
            {
                var dire = db.Area.Where(a => a.id_area == id).ToList().FirstOrDefault();
                datos.titular = dire.Persona.nombre;
                datos.ubicacion = dire.direccion;
                datos.correo = dire.correo;
                datos.telefono = dire.telefono;
                if (dire.Persona.ruta_foto != null)
                {
                    ViewBag.ruta = dire.Persona.ruta_foto;
                }
                var inda = db.Indicador_area.Where(i => i.id_area == id).ToList();
                foreach (var i in inda)
                {
                    var evs = db.Evaluacion.Where(e => e.id_indicador == i.id_indicador).ToList().FirstOrDefault();

                    if (evs != null && DateTime.Parse(evs.fecha.Day + "/" + evs.fecha.Month + "/" + evs.fecha.Year) >= DateTime.Parse(inicio) && DateTime.Parse(evs.fecha.Day + "/" + evs.fecha.Month + "/" + evs.fecha.Year) <= DateTime.Parse(fin))
                    {
                        if (evs.Indicador.Tipo_indicador.nombre == "Gestion")
                        {
                            if (datos.lista_gestion.Where(lg => lg.tema == evs.Indicador.Tema.nombre).FirstOrDefault() == null)
                            {
                                lista_g = new IndResumen();
                                lista_g.eje = evs.Indicador.Tema.Eje.nombre;
                                lista_g.tema = evs.Indicador.Tema.nombre;
                                var tot = db.Evaluacion.Where(e => e.Indicador.Tema.nombre == lista_g.tema && e.Indicador.Tipo_indicador.nombre == "Gestion").ToList();
                                var totv = db.Evaluacion.Where(e => e.Indicador.Tema.nombre == lista_g.tema && e.Indicador.Tipo_indicador.nombre == "Gestion" && e.evaluacion1 == "VERDE").ToList();
                                var tota = db.Evaluacion.Where(e => e.Indicador.Tema.nombre == lista_g.tema && e.Indicador.Tipo_indicador.nombre == "Gestion" && e.evaluacion1 == "AMARILLO").ToList();
                                var totr = db.Evaluacion.Where(e => e.Indicador.Tema.nombre == lista_g.tema && e.Indicador.Tipo_indicador.nombre == "Gestion" && e.evaluacion1 == "ROJO").ToList();
                                var ctot = tot.Where(to => DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) >= DateTime.Parse(inicio) && DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) <= DateTime.Parse(fin)).Count();
                                var ctotv = totv.Where(to => DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) >= DateTime.Parse(inicio) && DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) <= DateTime.Parse(fin)).Count();
                                var ctota = tota.Where(to => DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) >= DateTime.Parse(inicio) && DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) <= DateTime.Parse(fin)).Count();
                                var ctotr = totr.Where(to => DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) >= DateTime.Parse(inicio) && DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) <= DateTime.Parse(fin)).Count();

                                lista_g.total = ctot;
                                lista_g.verdes = ctotv;
                                lista_g.amarillos = ctota;
                                lista_g.rojos = ctotr;
                                datos.lista_gestion.Add(lista_g);

                            }
                            total_g++;
                            if (evs.evaluacion1 == "VERDE")
                            {
                                v_g++;
                            }
                            if (evs.evaluacion1 == "AMARILLO")
                            {
                                a_g++;
                            }
                            if (evs.evaluacion1 == "ROJO")
                            {
                                r_g++;
                            }
                        }
                        if (evs.Indicador.Tipo_indicador.nombre == "Desempeno")
                        {
                            if (datos.lista_desempeño.Where(lg => lg.tema == evs.Indicador.Tema.nombre).FirstOrDefault() == null)
                            {
                                lista_d = new IndResumen();
                                lista_d.eje = evs.Indicador.Tema.Eje.nombre;
                                lista_d.tema = evs.Indicador.Tema.nombre;
                                var tot = db.Evaluacion.Where(e => e.Indicador.Tema.nombre == lista_d.tema && e.Indicador.Tipo_indicador.nombre == "Desempeno").ToList();
                                var totv = db.Evaluacion.Where(e => e.Indicador.Tema.nombre == lista_d.tema && e.Indicador.Tipo_indicador.nombre == "Desempeno" && e.evaluacion1 == "VERDE").ToList();
                                var tota = db.Evaluacion.Where(e => e.Indicador.Tema.nombre == lista_d.tema && e.Indicador.Tipo_indicador.nombre == "Desempeno" && e.evaluacion1 == "AMARILLO").ToList();
                                var totr = db.Evaluacion.Where(e => e.Indicador.Tema.nombre == lista_d.tema && e.Indicador.Tipo_indicador.nombre == "Desempeno" && e.evaluacion1 == "ROJO").ToList();
                                var ctot = tot.Where(to => DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) >= DateTime.Parse(inicio) && DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) <= DateTime.Parse(fin)).Count();
                                var ctotv = totv.Where(to => DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) >= DateTime.Parse(inicio) && DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) <= DateTime.Parse(fin)).Count();
                                var ctota = tota.Where(to => DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) >= DateTime.Parse(inicio) && DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) <= DateTime.Parse(fin)).Count();
                                var ctotr = totr.Where(to => DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) >= DateTime.Parse(inicio) && DateTime.Parse(to.fecha.Day + "/" + to.fecha.Month + "/" + to.fecha.Year) <= DateTime.Parse(fin)).Count();

                                lista_d.total = ctot;
                                lista_d.verdes = ctotv;
                                lista_d.amarillos = ctota;
                                lista_d.rojos = ctotr;
                                datos.lista_desempeño.Add(lista_d);
                            }
                            total_d++;
                            if (evs.evaluacion1 == "VERDE")
                            {
                                v_d++;
                            }
                            if (evs.evaluacion1 == "AMARILLO")
                            {
                                a_d++;
                            }
                            if (evs.evaluacion1 == "ROJO")
                            {
                                r_d++;
                            }
                        }
                    }
                }


                datos.cantidad_gestion = total_g;
                datos.cantidad_desempeño = total_d;
                datos.cantidad_v_g = v_g;
                datos.cantidad_a_g = a_g;
                datos.cantidad_r_g = r_g;

                datos.cantidad_v_d = v_d;
                datos.cantidad_a_d = a_d;
                datos.cantidad_r_d = r_d;
                foreach (var ar in db.Area.ToList())
                {
                    areas.Add(ar);
                }

                ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
                return View(datos);
            }
            foreach (var ar in db.Area.ToList())
            {
                areas.Add(ar);
            }

            ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
            return View(datos);
        }

        public ActionResult Diagnostico(int? id = 0, int? fecha_s = 0)
        {
            var area = new Area();
            area.id_area = 0;
            area.nombre = "--Seleccione un area--";
            var areas = new List<Area>();
            areas.Add(area);
            var fechas = new List<lista_fechas>();
            var fecha = new lista_fechas();
            var cont = 0;
            fecha.id = cont;
            fecha.fecha = "--Seleccione una fecha--";
            fechas.Add(fecha);
            if (id == 0 && fecha_s == 0)
            {
                foreach (var ar in db.Area.ToList())
                {
                    areas.Add(ar);
                }

                ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
                ViewBag.id_fecha = new SelectList(fechas, "id", "fecha", fecha_s);
                return View();
            }
            if (id != 0)
            {
                foreach (var fch in db.Indicador_area.Where(i => i.id_area == id).ToList())
                {
                    var ev = db.Evaluacion.Where(e => e.id_indicador == fch.id_indicador && e.Indicador.diagnostico == true).ToList().FirstOrDefault();
                    if (ev != null)
                    {
                        cont++;
                        fecha = new lista_fechas();
                        fecha.id = cont;
                        fecha.fecha = ev.fecha.Day.ToString() + "/" + ev.fecha.Month.ToString() + "/" + ev.fecha.Year.ToString();
                        fechas.Add(fecha);
                    }
                }

                if (fecha_s != 0)
                {
                    var fecha_a = fechas.Find(f => f.id == fecha_s);

                    if (fecha_a != null)
                    {
                        var fecha_a_c = DateTime.Parse(fecha_a.fecha);
                        var ruta = db.Archivo.Where(a => a.id_indicador == db.Evaluacion.Where(e => e.fecha == fecha_a_c).ToList().FirstOrDefault().id_indicador).ToList().FirstOrDefault().id_ruta;
                        ViewBag.ruta = ruta;
                    }

                }
                foreach (var ar in db.Area.ToList())
                {
                    areas.Add(ar);
                }
                ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
                ViewBag.id_fecha = new SelectList(fechas, "id", "fecha", fecha_s);

                return View();
            }
            ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
            ViewBag.id_fecha = new SelectList(fechas, "id", "fecha", fecha_s);

            return View();
        }

        public ActionResult DetallesProgramasPMD(int? id = 0)
        {
            DetallesPrograma dprog = new DetallesPrograma();
            dprog.programa = new Programa();
            dprog.programa.Eje = new Eje();
            dprog.programa.Area = new Area();
            dprog.subprogramas = new List<Subprograma>();
            var area = new Area();
            area.id_area = 0;
            area.nombre = "--Seleccione un area--";
            var areas = new List<Area>();
            areas.Add(area);
            foreach (var ar in db.Area.ToList())
            {
                areas.Add(ar);
            }
            ViewBag.id_area = new SelectList(areas, "id_area", "nombre", id);
            ViewBag.id = id;
            if (id == 0)
            {
                return View(dprog);
            }
            if (id != 0)
            {
                var prog = db.Programa.Where(p => p.id_area == id).ToList().FirstOrDefault();
                if (prog != null)
                {
                    dprog.programa = prog;
                    foreach (var subs in db.Subprograma.Where(s => s.id_programa == prog.id_programa).ToList())
                    {
                        dprog.subprogramas.Add(subs);
                    }
                }
                return View(dprog);
            }
            return View(dprog);
        }

        public ActionResult IndicadoresDesempeño()
        {
            int id_us = (int)Session["idus"];
            var per = db.Usuario.Where(u => u.id_usuario == id_us).FirstOrDefault();
            var ar = db.Area.Where(a => a.Persona.id_persona == per.id_persona).FirstOrDefault();
            var indicadores = db.Indicador_area.Where(i => i.id_area == ar.id_area && i.Indicador.Tipo_indicador.nombre == "Desempeno");

            var lista = new List<DatosIndicador>();
            foreach(var i in indicadores)
            {
                var elemento = new DatosIndicador();
                elemento.id = i.id_indicador;
                elemento.nombre = i.Indicador.nombre;
                elemento.tema = i.Indicador.Tema.nombre;
                var ev = db.Evaluacion.Where(e => e.id_indicador == i.id_indicador).OrderBy(e => e.fecha).OrderByDescending(e => e.id_evaluacion).FirstOrDefault();
                if(ev == null)
                {
                    elemento.ult_ev = "N/A";
                }
                else
                {
                    elemento.ult_ev = ev.evaluacion1;
                    elemento.dato = ev.dato;

                }
                lista.Add(elemento);
            }
            return View(lista);
        }

        public ActionResult DatosRequerimientos(int? id = 0, string datos="")
        {
            var req = db.Requerimientos_desempeño.Where(r => r.Requerimientos.id_indicador == id).ToList();
            var lista = new DatosRequerimientos();
            lista.lista_requerimientos = new List<Requerimientos_desempeño>();
            if (id != 0)
            {
                if(datos == "")
                {
                    lista.lista_requerimientos = req;
                    ViewBag.id = id;
                    ViewBag.cant = req.Count();
                    return View(lista);
                }
                else
                {
                    lista.lista_requerimientos = req;
                    ViewBag.id = id;
                    ViewBag.cant = req.Count();

                    string[] arreglo_datos = datos.Split(',');
                    int cont = 0;
                    DataTable dt = new DataTable();
                    string formula = db.Indicador.Where(i => i.id_indicador == id).FirstOrDefault().formula;
                    foreach(var items in req)
                    {
                        formula = formula.Replace(items.Requerimientos.nombre.Substring(items.Requerimientos.nombre.IndexOf('.') + 2), arreglo_datos[cont]);
                        cont++;
                    }
             

                    try
                    {
                        ViewBag.resultado = dt.Compute(formula, "");
                    }
                    catch (Exception e)
                    {
                        ViewBag.resultado = "error de sintaxis";
                    }
                    var indir = db.Indicador.Where(i => i.id_indicador == id).FirstOrDefault();
                    if (Convert.ToString(ViewBag.resultado) != "error de sintaxis")
                    {
                        if ((Boolean)dt.Compute(ViewBag.resultado+indir.r_aceptable, ""))
                        {
                            ViewBag.convert = "aceptable";
                            Evaluacion ev = new Evaluacion();
                            ev.id_indicador = (int)id;
                            ev.fecha = DateTime.Now;
                            ev.id_usuario = (int)Session["idus"];
                            ev.presupuesto = "";
                            ev.producto = "";
                            ev.evaluacion1 = "VERDE";
                            ev.dato = (ViewBag.resultado).ToString();
                            ev.dato = Math.Round(Convert.ToDouble(ev.dato),2).ToString();
                            db.Evaluacion.Add(ev);

                            db.SaveChanges();
                            return RedirectToAction("IndicadoresDesempeño", "IndicadoresDirector");
                        }
                        else
                        {
                            
                            String eval = indir.r_bajo_aceptable;
                            do
                            {
                                eval = eval.Replace("v", (ViewBag.resultado).ToString());
                            } while (eval.IndexOf('v') != -1);
                            if ((Boolean)dt.Compute(eval, ""))
                            {
                                ViewBag.convert = "bajo_aceptable";
                                Evaluacion ev = new Evaluacion();
                                ev.id_indicador = (int)id;
                                ev.fecha = DateTime.Now;
                                ev.id_usuario = (int)Session["idus"];
                                ev.presupuesto = "";
                                ev.producto = "";
                                ev.evaluacion1 = "AMARILLO";
                                ev.dato =  (ViewBag.resultado).ToString();
                                ev.dato = Math.Round(Convert.ToDouble(ev.dato), 2).ToString();
                                db.Evaluacion.Add(ev);
                                db.SaveChanges();
                                return RedirectToAction("IndicadoresDesempeño", "IndicadoresDirector");
                            }
                            else
                            {
                                if ((Boolean)dt.Compute(ViewBag.resultado + indir.r_inaceptable, ""))
                                {
                                    ViewBag.convert = "inaceptable";
                                    Evaluacion ev = new Evaluacion();
                                    ev.id_indicador = (int)id;
                                    ev.fecha = DateTime.Now;
                                    ev.id_usuario = (int)Session["idus"];
                                    ev.presupuesto = "";
                                    ev.producto = "";
                                    ev.evaluacion1 = "ROJO";
                                    ev.dato = (ViewBag.resultado).ToString();
                                    ev.dato = Math.Round(Convert.ToDouble(ev.dato), 2).ToString();

                                    db.Evaluacion.Add(ev);
                                    db.SaveChanges();
                                    return RedirectToAction("IndicadoresDesempeño", "IndicadoresDirector");
                                }
                            }
                        }
                    }
                    ViewBag.convert = formula;
                    return View(lista);
                }
               
            }
            return View();
        }

      
    }
}