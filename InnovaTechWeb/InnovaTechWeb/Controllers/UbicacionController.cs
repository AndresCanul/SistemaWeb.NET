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
    public class UbicacionController : Controller
    {
        UbicacionModel modelo = new UbicacionModel();

        [HttpGet]
        public ActionResult ConsultarUbicaciones()
        {
            var respuesta = modelo.ConsultarUbicaciones();

            if (respuesta.Codigo == 0)
                return View(respuesta.Datos);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Ubicacion>());
            }
        }

        [HttpGet]
        public ActionResult ConsultarDistritos()
        {
            var respuesta = modelo.ConsultarDistritos();

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
        public ActionResult ConsultarCanton(long IdUbicacion)
        {
            var respuesta = modelo.ConsultarCanton(IdUbicacion);

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
        public ActionResult ConsultarProvincia(long IdCanton)
        {
            var respuesta = modelo.ConsultarProvincia(IdCanton);

            if (respuesta.Codigo == 0)
            {
                return Json(respuesta.Dato, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(respuesta.Detalle, JsonRequestBehavior.AllowGet);
            }
        }
    }
}