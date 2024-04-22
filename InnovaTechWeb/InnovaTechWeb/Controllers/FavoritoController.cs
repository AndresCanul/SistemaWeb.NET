using InnovaTechWeb.Entidades;
using InnovaTechWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace InnovaTechWeb.Controllers
{
    public class FavoritoController : Controller
    {
        FavoritoModel modelo = new FavoritoModel();

        [HttpGet]
        public ActionResult ConsultarFavorito(long IdProducto)
        {
            var respuesta = modelo.ConsultarFavorito(IdProducto);

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
        public ActionResult ConsultarFavoritos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarFavorito(long IdProducto)
        {
            var respuesta = modelo.AgregarFavorito(IdProducto);

            if (respuesta.Codigo == 0)
            {
                return View();
            }

            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult EliminarFavorito(long IdProducto)
        {
            var respuesta = modelo.EliminarFavorito(IdProducto);

            if (respuesta.Codigo == 0)
            {
                return View();
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }
    }
}