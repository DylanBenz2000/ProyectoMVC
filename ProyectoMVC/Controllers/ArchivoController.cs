using ProyectoMVC.Models.VistaModelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMVC.Controllers
{
    public class ArchivoController : Controller
    {
        // GET: Archivo
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            return View();
        }
        public ActionResult Save(ArchivoVistaModelo model)
        {
            string RutaSitio = Server.MapPath("~/");
            string PathArchivo1 = Path.Combine(RutaSitio+"/Files/archivo1.png");
            string PathArchivo2 = Path.Combine(RutaSitio+"/Files/archivo2.png");
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            model.Archivo1.SaveAs(PathArchivo1);
            model.Archivo1.SaveAs(PathArchivo2);

            @TempData["Message"] = "Se cargaron los archivos exitosamente";
            return RedirectToAction("Index");
        }
    }
}