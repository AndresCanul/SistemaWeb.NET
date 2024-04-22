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
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class CarritoController : Controller
    {
        CarritoModel modelo = new CarritoModel();

        [HttpGet]
        public ActionResult ConsultarAgregado(long IdProducto)
        {
            var respuesta = modelo.ConsultarAgregado(IdProducto);

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
        public ActionResult ConsultarCarrito()
        {
            var respuesta = modelo.ConsultarCarrito();

            if (respuesta.Codigo == 0)
                return View(respuesta.Datos);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Carrito>());
            }
        }

        [HttpPost]
        public ActionResult AgregarCarrito(long IdProducto)
        {
            Carrito entidad = new Carrito();
            entidad.IdProducto = IdProducto;

            var respuesta = modelo.AgregarCarrito(entidad);

            if (respuesta.Codigo == 0)
            {
                ActualizarVariablesCarrito();
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(respuesta.Detalle, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ActualizarCarrito(long IdCarrito, int Cantidad)
        {
            Carrito entidad = new Carrito();
            entidad.IdProducto = IdCarrito;
            entidad.Cantidad = Cantidad;

            var respuesta = modelo.AgregarCarrito(entidad);

            if (respuesta.Codigo == 0)
            {
                ActualizarVariablesCarrito();
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(respuesta.Detalle, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult EliminarCarrito(long IdProducto)
        {
            var respuesta = modelo.EliminarCarrito(IdProducto);

            if (respuesta.Codigo == 0)
            {
                ActualizarVariablesCarrito();
                return RedirectToAction("ConsultaCarrito", "Carrito");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        private void ActualizarVariablesCarrito()
        {
            var datos = modelo.ConsultarCarrito();

            if (datos.Codigo == 0)
            {
                Session["Cantidad"] = datos.Datos.AsEnumerable().Sum(x => x.Cantidad);
                Session["SubTotal"] = datos.Datos.AsEnumerable().Sum(x => x.SubTotal);
                Session["Total"] = datos.Datos.AsEnumerable().Sum(x => x.Total);
            }
            else
            {
                Session["Cantidad"] = 0;
                Session["SubTotal"] = 0;
                Session["Total"] = 0;
            }
        }
    }
}