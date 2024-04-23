using InnovaTechWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace InnovaTechWeb.Controllers
{
    public class ValoracionController : Controller
    {
        ValoracionModel valoracionModel = new ValoracionModel();

        [HttpGet]
        public ActionResult ConsultarValoracion(long IdProducto)
        {
            var respuesta = valoracionModel.ConsultarValoracion(IdProducto);

            if (respuesta.Codigo == 0)
            {
                return Json(respuesta.Dato, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(respuesta.Detalle, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AgregarValoracion(long IdProducto)
        {
            var respuesta = valoracionModel.AgregarValoracion(IdProducto);

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