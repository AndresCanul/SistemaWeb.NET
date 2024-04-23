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
    [FiltroSeguridad]
    [OutputCache(NoStore = true, VaryByParam = "*", Duration = 0)]
    public class UsuarioController : Controller
    {
        UsuarioModel usuarioModel = new UsuarioModel();
        UbicacionModel ubicacionModel = new UbicacionModel();
        RolModel rolModel = new RolModel();

        [HttpGet]
        public ActionResult ConsultarUsuario(long IdUsuario)
        {
            var respuesta = usuarioModel.ConsultarUsuario(IdUsuario);

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
        public ActionResult VisualizarPerfil()
        {
            var respuesta = usuarioModel.VisualizarPerfil();

            if (respuesta.Codigo == 0)
                return View(respuesta.Dato);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new Usuario());
            }
        }

        [HttpGet]
        public ActionResult ConsultarUsuarios()
        {
            var respuesta = usuarioModel.ConsultarUsuarios();

            if (respuesta.Codigo == 0)
                return View(respuesta.Datos);
            else
            {
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View(new List<Usuario>());
            }
        }                

        [HttpGet]
        public ActionResult ActualizarUsuario(long IdUsuario)
        {
            var respuesta = usuarioModel.ConsultarUsuario(IdUsuario);
            CargarRoles();
            CargarDistritos();
            return View(respuesta.Dato);
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(HttpPostedFileBase ImagenUsuario, Usuario entidad)
        {
            var respuesta = usuarioModel.ActualizarUsuario(entidad);

            if (respuesta.Codigo == 0)
            {
                string extension = Path.GetExtension(Path.GetFileName(ImagenUsuario.FileName));
                string ruta = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\" + respuesta.Valor + extension;
                ImagenUsuario.SaveAs(ruta);

                entidad.IdUsuario = (long)respuesta.Valor;
                entidad.ImagenUsuario = "/Imagenes/" + respuesta.Valor + extension;

                usuarioModel.ActualizarImagenUsuario(entidad);
                return RedirectToAction("ConsultarUsuarios", "Usuario");
            }

            else
            {
                CargarRoles();
                CargarDistritos();
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarPerfil()
        {
            var resp = usuarioModel.VisualizarPerfil();
            CargarRoles();
            CargarDistritos();
            return View(resp.Dato);
        }

        [HttpPost]
        public ActionResult ActualizarPerfil(HttpPostedFileBase ImagenUsuario, Usuario entidad)
        {
            var respuesta = usuarioModel.ActualizarPerfil(entidad);

            if (respuesta.Codigo == 0)
            {
                string extension = Path.GetExtension(Path.GetFileName(ImagenUsuario.FileName));
                string ruta = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\" + respuesta.Valor + extension;
                ImagenUsuario.SaveAs(ruta);

                entidad.IdUsuario = (long)respuesta.Valor;
                entidad.ImagenUsuario = "/Imagenes/" + respuesta.Valor + extension;

                usuarioModel.ActualizarImagenUsuario(entidad);

                Session["NombreUsuario"] = entidad.NombreUsuario;
                return RedirectToAction("VisualizarPerfil", "Usuario");
            }

            else
            {
                CargarRoles();
                CargarDistritos();
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeshabilitarUsuario(Usuario entidad)
        {
            var respuesta = usuarioModel.DeshabilitarUsuario(entidad);

            if (respuesta.Codigo == 0)
            {
                return Json(respuesta.Valor, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(respuesta.Detalle, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EliminarUsuario(long IdUsuario)
        {
            var respuesta = usuarioModel.EliminarUsuario(IdUsuario);

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

        private void CargarRoles()
        {
            var respuesta = rolModel.ConsultarRoles(false);
            var listaRoles = new List<SelectListItem>();

            listaRoles.Add(new SelectListItem { Text = "Seleccione...", Value = "" });
            foreach (var item in respuesta.Datos)
                listaRoles.Add(new SelectListItem { Text = item.NombreRol, Value = item.IdRol.ToString() });

            ViewBag.ListaRoles = listaRoles;
        }

        private void CargarDistritos()
        {
            var respuesta = ubicacionModel.ConsultarUbicaciones();
            var listaDistritos = new List<SelectListItem>();

            listaDistritos.Add(new SelectListItem { Text = "Seleccione...", Value = "" });
            foreach (var item in respuesta.Datos)
                listaDistritos.Add(new SelectListItem { Text = item.NombreDistrito, Value = item.IdUbicacion.ToString() });

            ViewBag.ListaDistritos = listaDistritos;
        }
    }


}