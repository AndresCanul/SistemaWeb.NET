using InnovaTechWeb.Entidades;
using InnovaTechWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace InnovaTechWeb.Controllers
{
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class ProductoController : Controller
    {
        ProductoModel productoModel = new ProductoModel();
        CategoriaModel categoriaModel = new CategoriaModel();

        [HttpGet]
        public ActionResult ConsultarProducto(long IdProducto)
        {
            var respuesta = productoModel.ConsultarProducto(IdProducto);

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
        public ActionResult ConsultarProductos()
        {
            var respuesta = productoModel.ConsultarProductos(true);

            if (respuesta.Codigo == 0)
                return View(respuesta.Datos);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Producto>());
            }
        }

        [HttpGet]
        public ActionResult RegistrarProducto()
        {
            CargarCategorias();
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarProducto(HttpPostedFileBase ImagenProducto, Producto entidad)
        {
            var respuesta = productoModel.RegistrarProducto(entidad);

            if (respuesta.Codigo == 0)
            {
                string extension = Path.GetExtension(Path.GetFileName(ImagenProducto.FileName));
                string ruta = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\" + respuesta.Valor + extension;
                ImagenProducto.SaveAs(ruta);

                entidad.IdProducto = Convert.ToInt64(respuesta.Valor);
                entidad.ImagenProducto = "/Imagenes/" + respuesta.Valor + extension;

                productoModel.ActualizarImagenProducto(entidad);

                return RedirectToAction("ConsultarProductos", "Producto");
            }
            else
            {
                CargarCategorias();
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarProducto(long IdProducto)
        {
            var respuesta = productoModel.ConsultarProducto(IdProducto);
            CargarCategorias();
            ViewBag.urlImagen = respuesta.Dato.ImagenProducto;
            return View(respuesta.Dato);
        }

        [HttpPost]
        public ActionResult ActualizarProducto(HttpPostedFileBase ImagenProducto, Producto entidad)
        {
            var respuesta = productoModel.ActualizarProducto(entidad);

            if (respuesta.Codigo == 0)
            {
                if (ImagenProducto != null)
                {
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + entidad.ImagenProducto);

                    string extension = Path.GetExtension(Path.GetFileName(ImagenProducto.FileName));
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\" + entidad.IdProducto + extension;
                    ImagenProducto.SaveAs(ruta);

                    entidad.ImagenProducto = "/Imagenes/" + entidad.IdProducto + extension;

                    productoModel.ActualizarImagenProducto(entidad);
                }

                return RedirectToAction("ConsultarProductos", "Producto");
            }
            else
            {
                CargarCategorias();
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeshabilitarProducto(Producto entidad)
        {
            var respuesta = productoModel.DeshabilitarProducto(entidad);

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
        public ActionResult EliminarProducto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EliminarProducto(long IdProducto)
        {
            var respuesta = productoModel.EliminarProducto(IdProducto);

            if (respuesta.Codigo == 0)
            {
                return RedirectToAction("ConsultarProductos", "Producto");
            }
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        private void CargarCategorias()
        {
            var respuesta = categoriaModel.ConsultarCategorias(false);
            var listaCategoria = new List<SelectListItem>();

            listaCategoria.Add(new SelectListItem { Text = "Seleccione una categoría", Value = "" });
            foreach (var item in respuesta.Datos)
                listaCategoria.Add(new SelectListItem { Text = item.NombreCategoria, Value = item.IdCategoria.ToString() });

            ViewBag.listaCategoria = listaCategoria;
        }
    }
}