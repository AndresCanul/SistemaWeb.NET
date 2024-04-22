using InnovaTechAPI.Entidades;
using InnovaTechAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InnovaTechAPI.Controllers
{
    public class RolController : ApiController
    {
        [HttpGet]
        [Route("Rol/ConsultarRol")]
        public ResultadoRol ConsultarRol(long IdRol)
        {
            var resultado = new ResultadoRol();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ConsultarRol(IdRol).FirstOrDefault();

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
        [Route("Rol/ConsultarRoles")]
        public ResultadoRol ConsultarRoles(bool MostrarTodos)
        {
            var resultado = new ResultadoRol();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var datos = db.ConsultarRoles(MostrarTodos).ToList();

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
        [Route("Rol/CrearRol")]
        public Resultado CrearRol(Rol entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.CrearRol(entidad.NombreRol, entidad.DescripcionRol, entidad.IconoRol).FirstOrDefault();

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
        [Route("Rol/ActualizarRol")]
        public Resultado ActualizarRol(Rol entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ActualizarRol(entidad.IdRol, entidad.NombreRol, entidad.DescripcionRol, entidad.IconoRol);

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
        [Route("Rol/DeshabilitarRol")]
        public Resultado DeshabilitarRol(Rol entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.DeshabilitarRol(entidad.IdRol, entidad.Estado);

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
        [Route("Rol/EliminarRol")]
        public Resultado EliminarRol(long IdRol)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.EliminarRol(IdRol);

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
