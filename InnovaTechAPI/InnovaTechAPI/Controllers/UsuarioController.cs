using InnovaTechAPI.Entidades;
using InnovaTechAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InnovaTechAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        Utilitarios modelo = new Utilitarios();

        [HttpGet]
        [Route("Usuario/ConsultarUsuario")]
        public ResultadoUsuario ConsultarUsuario(long IdUsuario)
        {
            var resultado = new ResultadoUsuario();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ConsultarUsuario(IdUsuario).FirstOrDefault();

                    if (dato != null)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                        resultado.Dato = dato;
                    }

                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "No se encontraron resultados";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "Se presento un error en el sistema";
            }

            return resultado;
        }

        [HttpGet]
        [Route("Usuario/VisualizarPerfil")]
        public ResultadoUsuario VisualizarPerfil(long IdUsuario)
        {
            var resultado = new ResultadoUsuario();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.VisualizarPerfil(IdUsuario).FirstOrDefault();

                    if (dato != null)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                        resultado.Dato = dato;
                    }

                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "No se encontraron resultados";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "Se presento un error en el sistema";
            }

            return resultado;
        }

        [HttpGet]
        [Route("Usuario/ConsultarUsuarios")]
        public ResultadoUsuario ConsultarUsuarios(long IdUsuario)
        {
            var resultado = new ResultadoUsuario();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var datos = db.ConsultarUsuarios(IdUsuario).ToList();

                    if (datos.Count > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                        resultado.Datos = datos;

                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "No se encontraron resultados";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "Se presento un error en el sistema";
            }

            return resultado;
        }

        [HttpPost]
        [Route("Usuario/RegistrarUsuario")]
        public Resultado RegistrarUsuario(Usuario entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var respuesta = db.RegistrarUsuario(entidad.IdUbicacion, entidad.NombreUsuario, entidad.ApellidoUsuario, entidad.Edad, entidad.Correo, entidad.Clave).FirstOrDefault();

                    if (respuesta > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                        resultado.Valor = respuesta.Value;
                    }

                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "Su informacion ya se encuentra registrada";
                    }
                }
            }

            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "su informacion no se pudo registrar";
            }

            return resultado;
        }

        [HttpPost]
        [Route("Usuario/IniciarSesionUsuario")]
        public ResultadoUsuario IniciarSesionUsuario(Usuario entidad)
        {
            var resultado = new ResultadoUsuario();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.IniciarSesionUsuario(entidad.Correo, entidad.Clave).FirstOrDefault();

                    if (dato != null)
                    {
                        if (dato.Temporal && DateTime.Now > dato.Vencimiento)
                        {
                            resultado.Codigo = -1;
                            resultado.Detalle = "Su contraseña temporal ha caducado";
                        }
                        else
                        {
                            resultado.Codigo = 0;
                            resultado.Detalle = string.Empty;
                            resultado.Dato = dato;
                        }
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "No se pudo validar su informacion de ingreso";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "Se presento un error en el sistema";
            }

            return resultado;
        }

        [HttpPost]
        [Route("Usuario/RecuperarAccesoUsuario")]
        public Resultado RecuperarAccesoUsuario(Usuario entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.GenerarClave(entidad.Correo).FirstOrDefault();

                    if (dato != null && dato.Vencimiento != null)
                    {
                        // Mandar un correo
                        string ruta = AppDomain.CurrentDomain.BaseDirectory + "Password.html";
                        string contenido = File.ReadAllText(ruta);

                        contenido = contenido.Replace("@@Nombre", dato.NombreUsuario);
                        contenido = contenido.Replace("@@Clave", dato.Clave);

                        string vencimiento = dato.Vencimiento.ToString("dd/MM/yyyy hh:mm:ss tt");
                        contenido = contenido.Replace("@@Vencimiento", dato.Vencimiento.ToString("dd/MM/yyyy hh:mm:ss tt"));

                        modelo.EnviarCorreo(dato.Correo, "Acceso Temporal", contenido);

                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "No se pudo validar su informacion";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "Se presento un error en el sistema";
            }

            return resultado;
        }

        [HttpPut]
        [Route("Usuario/ActualizarUsuario")]
        public Resultado ActualizarUsuario(Usuario entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ActualizarUsuario(entidad.IdUsuario, entidad.IdUbicacion, entidad.IdRol, entidad.NombreUsuario, entidad.ApellidoUsuario, entidad.Edad, entidad.Correo);

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "Su informacion ya se encuentra registrada";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "su informacion no se pudo registrar";
            }

            return resultado;
        }

        [HttpPut]
        [Route("Usuario/ActualizarPerfil")]
        public Resultado ActualizarPerfil(Usuario entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ActualizarPerfil(entidad.IdUsuario, entidad.IdUbicacion, entidad.NombreUsuario, entidad.ApellidoUsuario, entidad.Edad, entidad.Correo);

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "Su informacion ya se encuentra registrada";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "su informacion no se pudo registrar";
            }

            return resultado;
        }

        [HttpPut]
        [Route("Usuario/ActualizarImagenUsuario")]
        public Resultado ActualizarImagenUsuario(Usuario entidad)
        {
            var resultado = new Resultado();

            try
            {

                using (var db = new InnovaTechDBEntities())
                {
                    var resp = db.ActualizarImagenProducto(entidad.IdUsuario, entidad.ImagenUsuario);

                    if (resp > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }

                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "Su informacion ya se encuentra registrada";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "su informacion no se pudo registrar";
            }

            return resultado;
        }

        [HttpPut]
        [Route("Usuario/CambiarClave")]
        public Resultado CambiarClave(Usuario entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.CambiarClave(entidad.IdUsuario, entidad.Clave, entidad.ClaveNueva);

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "Su informacion ya se encuentra registrada";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "su informacion no se pudo registrar";
            }

            return resultado;
        }

        [HttpPut]
        [Route("Usuario/DeshabilitarUsuario")]
        public Resultado DeshabilitarUsuario(Usuario entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.DeshabilitarUsuario(entidad.IdUsuario, entidad.Estado);

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "Su informacion ya se encuentra registrada";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "su informacion no se pudo registrar";
            }

            return resultado;
        }

        [HttpDelete]
        [Route("Usuario/EliminarUsuario")]
        public Resultado EliminarUsuario(long IdUsuario)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.EliminarUsuario(IdUsuario);

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "Su informacion ya se encuentra registrada";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "su informacion no se pudo registrar";
            }

            return resultado;
        }

    }
}
