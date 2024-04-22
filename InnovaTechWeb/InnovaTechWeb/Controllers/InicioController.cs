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
    public class InicioController : Controller
    {
        UsuarioModel modelo = new UsuarioModel();

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(Usuario entidad)
        {
            var resultado = modelo.IniciarSesionUsuario(entidad);            

            if (resultado.Codigo == 0)
            {
                Session["IdUsuario"] = resultado.Dato.IdUsuario;
                Session["NombreUsuario"] = resultado.Dato.NombreUsuario;
                Session["ApellidoUsuario"] = resultado.Dato.ApellidoUsuario;
                Session["RolUsuario"] = resultado.Dato.IdRol;
                Session["NombreRol"] = resultado.Dato.NombreRol;
                return RedirectToAction("PantallaPrincipal", "Inicio");
            }

            else
            {
                ViewBag.MsjPantalla = resultado.Detalle;
                return View();
            }
        }

        [HttpGet]
        public ActionResult RegistrarUsuario()
        {
            CargarUbicaciones();

            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuario(HttpPostedFileBase ImagenUsuario, Usuario entidad)
        {
            var respuesta = modelo.RegistrarUsuario(entidad);

            if (respuesta.Codigo == 0)
            { 
                string extension = Path.GetExtension(Path.GetFileName(ImagenUsuario.FileName));
                string ruta = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\" + respuesta.Valor + extension;
                ImagenUsuario.SaveAs(ruta);

                entidad.IdUsuario = (long)respuesta.Valor;
                entidad.ImagenUsuario = "/Imagenes/" + respuesta.Valor + extension;

                modelo.ActualizarImagenUsuario(entidad);

                return RedirectToAction("IniciarSesion", "Inicio");
            }
            else
                CargarUbicaciones();
                ViewBag.MsjPantalla = respuesta.Detalle;
                return View();
        }

        [HttpGet]
        public ActionResult RecuperarAccesoUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarAccesoUsuario(Usuario entidad)
        {
            var respuesta = modelo.RecuperarAccesoUsuario(entidad);

            if (respuesta.Codigo == 0)
                return RedirectToAction("IniciarSesion", "Inicio");
            else
                ViewBag.MsjPantalla = respuesta.Detalle;
            return View();
        }

        [HttpGet]
        public ActionResult PantallaPrincipal()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("IniciarSesion", "Inicio");
        }

        private void CargarUbicaciones()
        {
            UbicacionModel ubicacion = new UbicacionModel();

            var respuesta = ubicacion.ConsultarUbicaciones();
            var ubicaciones = new List<SelectListItem>();

            ubicaciones.Add(new SelectListItem { Text = "Seleccione...", Value = "" });
            foreach (var item in respuesta.Datos)
                ubicaciones.Add(new SelectListItem { Text = item.NombreDistrito, Value = item.IdUbicacion.ToString() });

            ViewBag.Ubicaciones = ubicaciones;
        }
    }
}
