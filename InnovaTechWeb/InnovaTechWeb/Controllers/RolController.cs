using InnovaTechWeb.Entidades;
using InnovaTechWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnovaTechWeb.Controllers
{
    public class RolController : Controller
    {
        RolModel modelo = new RolModel();

        [HttpGet]
        public ActionResult ConsultarRol(long IdRol)
        {
            var respuesta = modelo.ConsultarRol(IdRol);

            if (respuesta.Codigo == 0)
            {
                return Json(respuesta.Dato, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(respuesta.Detalle, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ConsultarRoles()
        {
            var respuesta = modelo.ConsultarRoles(true);

            if (respuesta.Codigo == 0)
                return View(respuesta.Datos);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Producto>());
            }
        }

        [HttpGet]
        public ActionResult CrearRol()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearRol(Rol entidad)
        {
            var respuesta = modelo.CrearRol(entidad);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("ConsultarRoles", "Rol");
            }

            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarRol(long IdRol)
        {
            var respuesta = modelo.ConsultarRol(IdRol);
            return View(respuesta.Dato);
        }

        [HttpPost]
        public ActionResult ActualizarRol(Rol entidad)
        {
            var respuesta = modelo.ActualizarRol(entidad);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("ConsultarRoles", "Rol");
            }

            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeshabilitarRol(Rol entidad)
        {
            var respuesta = modelo.DeshabilitarRol(entidad);

            if (respuesta.Codigo == 0)
            {
                return Json(respuesta.Valor, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(respuesta.Detalle, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult EliminarRol()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EliminarRol(long IdRol)
        {
            var respuesta = modelo.EliminarRol(IdRol);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("ConsultarRoles", "Rol");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }
    }
}