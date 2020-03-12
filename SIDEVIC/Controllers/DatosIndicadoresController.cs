using SIDEVIC.Filtros;
using SIDEVIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIDEVIC.Controllers
{   
    [NotLoggedUser]
    public class DatosIndicadoresController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: DatosIndicadores
        public ActionResult FichaTecnica(int? id = 0)
        {
            Evaluacion ev = new Evaluacion();
            ev.Indicador = new Indicador();
            ev.Indicador.Tema = new Tema();
            ev.Indicador.Tema.Eje = new Eje();
            ev.Indicador.Tipo_indicador = new Tipo_indicador();
            ev.Indicador.Frecuencia = new Frecuencia();
            if(id != 0)
            {
                var ind = db.Evaluacion.Where(e => e.id_evaluacion == id).ToList().FirstOrDefault();
                if(ind != null)
                {
                    var resp = db.Indicador_area.Where(a => a.id_indicador == ind.id_indicador).ToList().FirstOrDefault();
                    ViewBag.responsable = resp.Area.Persona.nombre;
                    var li = new List<String>();
                    if(ind.Indicador.Tipo_indicador.nombre == "Gestion")
                    {
                        var lista = db.Requerimientos_cumple.Where(r => r.Requerimientos.id_indicador == ind.id_indicador).ToList();
                        foreach(var item in lista)
                        {
                            li.Add(item.Requerimientos.nombre+" : "+item.cumple);
                        }
                        ViewBag.lista = li;
                    }
                    if (ind.Indicador.Tipo_indicador.nombre == "Desempeno")
                    {
                        var lista = db.Requerimientos_desempeño.Where(r => r.Requerimientos.id_indicador == ind.id_indicador).ToList();
                        foreach (var item in lista)
                        {
                            li.Add(item.Requerimientos.nombre + " : " + item.cantidad);
                        }
                        ViewBag.lista = li;
                    }
                    ev = ind;
                    return View(ev);
                }
            }
            return View(ev);
        }
    }
}