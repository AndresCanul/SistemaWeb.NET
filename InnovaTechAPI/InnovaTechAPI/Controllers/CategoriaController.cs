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
    public class CategoriaController : ApiController
    {
        [HttpGet]
        [Route("Categoria/ConsultarCategoria")]
        public ResultadoCategoria ConsultarCategoria(long IdCategoria)
        {
            var resultado = new ResultadoCategoria();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ConsultarCategoria(IdCategoria).FirstOrDefault();

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
        [Route("Categoria/ConsultarCategorias")]
        public ResultadoCategoria ConsultarCategorias(bool MostrarTodos)
        {
            var resultado = new ResultadoCategoria();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var datos = db.ConsultarCategorias(MostrarTodos).ToList();

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
        [Route("Categoria/CrearCategoria")]
        public Resultado CrearCategoria(Categoria entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.CrearCategoria(entidad.NombreCategoria, entidad.DescripcionCategoria, entidad.IconoCategoria);

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "La Categoria ya se ha creado anteriormente";
                    }
                }
            }

            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "La Categoria no se ha creaado";
            }

            return resultado;
        }

        [HttpPut]
        [Route("Categoria/ActualizarCategoria")]
        public Resultado ActualizarCategoria(Categoria entidad)
        {
            var resultado = new Resultado();

            try
            {
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ActualizarCategoria(entidad.IdCategoria, entidad.NombreCategoria, entidad.DescripcionCategoria, entidad.IconoCategoria);

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "Los datos introducidos de la Categoria presentan errores";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "La Categoria no se ha actualizado";
            }

            return resultado;
        }

        [HttpPut]
        [Route("Categoria/DeshabilitarCategoria")]
        public Resultado DeshabilitarCategoria(Categoria entidad)
        {
            var resultado = new Resultado();

            try
            {
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.DeshabilitarCategoria(entidad.IdCategoria, entidad.Estado);

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "Los datos de la Categoria presentan errores";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "La Categoria no se ha deshabilitado";
            }

            return resultado;
        }

        [HttpDelete]
        [Route("Categoria/EliminarCategoria")]
        public Resultado EliminarCategoria(long IdCategoria)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.EliminarCategoria(IdCategoria);

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "La Categoria no existe";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "La Categoria no se pudo eliminar";
            }

            return resultado;
        }
    }
}
