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
        UsuarioModel usuarioModel = new UsuarioModel();
        UbicacionModel ubicacionModel = new UbicacionModel();
        RolModel rolModel = new RolModel();

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(Usuario entidad)
        {
            var resultado = usuarioModel.IniciarSesionUsuario(entidad);            

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
            CargarDistritos();
            CargarRoles();

            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuario(HttpPostedFileBase ImagenUsuario, Usuario entidad)
        {
            var respuesta = usuarioModel.RegistrarUsuario(entidad);

            if (respuesta.Codigo == 0)
            { 
                string extension = Path.GetExtension(Path.GetFileName(ImagenUsuario.FileName));
                string ruta = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\" + respuesta.Valor + extension;
                ImagenUsuario.SaveAs(ruta);

                entidad.IdUsuario = respuesta.Valor;
                entidad.ImagenUsuario = "/Imagenes/" + respuesta.Valor + extension;

                usuarioModel.ActualizarImagenUsuario(entidad);

                return RedirectToAction("IniciarSesion", "Inicio");
            }
            else
                CargarDistritos();
                CargarRoles();
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
            var respuesta = usuarioModel.RecuperarAccesoUsuario(entidad);

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
