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
    public class UsuariosController : Controller
    {
        private snowball_sidevicbdEntities db = new snowball_sidevicbdEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Persona).Include(u => u.Rol);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre");
            ViewBag.id_rol = new SelectList(db.Rol, "id_rol", "tipo");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_usuario,username,contraseña,id_rol,id_persona")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                if(usuario.username == null || usuario.contraseña == null)
                {
                    ViewBag.Flag = "field_error";
                    ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", usuario.id_persona);
                    ViewBag.id_rol = new SelectList(db.Rol, "id_rol", "tipo", usuario.id_rol);
                    return View(usuario);
                }


                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", usuario.id_persona);
            ViewBag.id_rol = new SelectList(db.Rol, "id_rol", "tipo", usuario.id_rol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.username = usuario.username;
            ViewBag.contrasena = usuario.contraseña;
            ViewBag.id_persona_val = usuario.id_persona;
            ViewBag.id_rol_val = usuario.id_rol;
            ViewBag.id_usuario = usuario.id_usuario;

            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", usuario.id_persona);
            ViewBag.id_rol = new SelectList(db.Rol, "id_rol", "tipo", usuario.id_rol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_usuario,username,contraseña,id_rol,id_persona")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.username == null || usuario.contraseña == null)
                {
                    ViewBag.Flag = "field_error";
                    var con = db.Usuario.Where(u => u.id_usuario == usuario.id_usuario).ToList().FirstOrDefault();
                    ViewBag.username = con.username;
                    ViewBag.contrasena = con.contraseña;
                    ViewBag.id_persona_val = con.id_persona;
                    ViewBag.id_rol_val = con.id_rol;

                    ViewBag.id_usuario = usuario.id_usuario;

                    ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", usuario.id_persona);
                    ViewBag.id_rol = new SelectList(db.Rol, "id_rol", "tipo", usuario.id_rol);
                    return View(usuario);
                }
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_persona = new SelectList(db.Persona, "id_persona", "nombre", usuario.id_persona);
            ViewBag.id_rol = new SelectList(db.Rol, "id_rol", "tipo", usuario.id_rol);
            return View(usuario);
        }


        // POST: Usuarios/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
