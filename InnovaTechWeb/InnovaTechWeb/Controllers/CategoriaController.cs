using InnovaTechWeb.Entidades;
using InnovaTechWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnovaTechWeb.Controllers
{
    public class CategoriaController : Controller
    {
        CategoriaModel modelo = new CategoriaModel();

        [HttpGet]
        public ActionResult ConsultarCategoria(long IdCategoria)
        {
            var respuesta = modelo.ConsultarCategoria(IdCategoria);

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
        public ActionResult ConsultarCategorias()
        {
            var respuesta = modelo.ConsultarCategorias(true);

            if (respuesta.Codigo == 0)
                return View(respuesta.Datos);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Producto>());
            }
        }

        [HttpGet]
        public ActionResult CrearCategoria()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult CrearCategoria(Categoria entidad)
        {
            var respuesta = modelo.CrearCategoria(entidad);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("ConsultarCategorias", "Categoria");
            }

            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarCategoria(long IdCategoria)
        {
            var respuesta = modelo.ConsultarCategoria(IdCategoria);
            return View(respuesta.Dato);
        }

        [HttpPost]
        public ActionResult ActualizarCategoria(Categoria entidad)
        {
            var respuesta = modelo.ActualizarCategoria(entidad);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("ConsultarCategorias", "Categoria");
            }

            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeshabilitarCategoria(Categoria entidad)
        {
            var respuesta = modelo.DeshabilitarCategoria(entidad);

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
        public ActionResult EliminarCategoria()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EliminarCategoria(long IdCategoria)
        {
            var respuesta = modelo.EliminarCategoria(IdCategoria);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("ConsultarCategorias", "Categoria");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }
    }
}
