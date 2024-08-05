using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMVC.Models;
using ProyectoMVC.Models.Tablas;
using ProyectoMVC.Models.VistaModelos;

namespace ProyectoMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            List<TablaUsuarioVistaModelo> lista = null;
            using (proyectomvcEntities1 db = new proyectomvcEntities1())
            {
                lista = (from d in db.user
                         where d.idState == 1
                         orderby d.email
                         select new TablaUsuarioVistaModelo
                         {
                             Email = d.email,
                             Id = d.id,
                             Edad = d.edad
                         }).ToList();
            }
            return View(lista);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UsuarioVistaModelo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new proyectomvcEntities1())
            {
                user oUser = new user();
                oUser.idState = 1;
                oUser.email = model.Email;
                oUser.edad = model.Edad;
                oUser.password = model.Password;

                db.user.Add(oUser);

                db.SaveChanges();
            }
            return Redirect(Url.Content("~/User/"));
        }

        public ActionResult Editar(int Id)
        {
            EditarUsuarioVistaModelo model = new EditarUsuarioVistaModelo();

            using (var db = new proyectomvcEntities1())
            {
                var objetoUser = db.user.Find(Id);
                model.Edad = (int)objetoUser.edad;
                model.Email = objetoUser.email;
                model.Id = objetoUser.id;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(EditarUsuarioVistaModelo  modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            using (var db = new proyectomvcEntities1())
            {
                var objetoUser = db.user.Find(modelo.Id);
                objetoUser.email = modelo.Email;
                objetoUser.edad = modelo.Edad;

                if(modelo.Password!=null && modelo.Password.Trim() != "")
                {
                    objetoUser.password = modelo.Password;
                }

                db.Entry(objetoUser).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }

            return Redirect(Url.Content("~/User/"));
        }

        [HttpPost]
        public ActionResult Eliminar(int Id)
        {


            using (var db = new proyectomvcEntities1())
            {
                var objetoUser = db.user.Find(Id);
                objetoUser.idState = 3; // Eliminar
                db.Entry(objetoUser).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }

            return Content("1");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UsuarioVistaModelo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new proyectomvcEntities1())
            {
                // Verificar si el usuario ya existe
                var existingUser = db.user.FirstOrDefault(u => u.email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
                    return View(model);
                }

                // Crear nuevo usuario
                user oUser = new user
                {
                    email = model.Email,
                    password = model.Password, // Asegúrate de almacenar contraseñas de manera segura en una implementación real
                    edad = model.Edad,
                    idState = 1 // Asignar estado activo
                };

                db.user.Add(oUser);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Access");
        }

    }
}