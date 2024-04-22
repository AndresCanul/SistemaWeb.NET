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
    public class OrdenController : Controller
    {
        OrdenModel modelo = new OrdenModel();
        CarritoModel carritoModel = new CarritoModel();

        [HttpGet]
        public ActionResult ConsultarDetalleFacturas(long IdOrden)
        {
            var respuesta = modelo.ConsultarDetalleFacturas(IdOrden);

            if (respuesta.Codigo == 0)
            {
                return View(respuesta.Datos);
            }

            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Orden>());
            }
        }

        [HttpGet]
        public ActionResult ConsultarFacturas()
        {
            var respuesta = modelo.ConsultarFacturas();

            if (respuesta.Codigo == 0)
            {
                return View(respuesta.Datos);
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Orden>());
            }
        }

        [HttpPost]
        public ActionResult RegistrarOrden()
        {
            var respuesta = modelo.RegistrarOrden();

            if (respuesta.Codigo == 0)
            {
                ActualizarVariablesCarrito();
                return RedirectToAction("PantallaPrincipal", "Inicio");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;

                var items = carritoModel.ConsultarCarrito();
                return View("ConsultaCarrito", items.Datos);
            }
        }

        private void ActualizarVariablesCarrito()
        {
            var datos = carritoModel.ConsultarCarrito();

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