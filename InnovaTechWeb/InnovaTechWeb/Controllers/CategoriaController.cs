using InnovaTechWeb.Entidades;
using InnovaTechWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnovaTechWeb.Controllers
{
    public class CategoriaController : Controller
    {
        CategoriaModel modelo = new CategoriaModel();

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
    }
}
