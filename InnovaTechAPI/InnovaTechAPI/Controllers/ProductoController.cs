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
    public class ProductoController : ApiController
    {
        [HttpGet]
        [Route("Producto/ConsultarProducto")]
        public ResultadoProducto ConsultarProducto(long IdProducto)
        {
            var resultado = new ResultadoProducto();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ConsultarProducto(IdProducto).FirstOrDefault();

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
        [Route("Producto/ConsultarProductos")]
        public ResultadoProducto ConsultarProductos(bool MostrarTodos)
        {
            var resultado = new ResultadoProducto();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var datos = db.ConsultarProductos(MostrarTodos).ToList();

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
        [Route("Producto/RegistrarProducto")]
        public Resultado RegistrarProducto(Producto entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.RegistrarProducto(entidad.IdCategoria, entidad.NombreProducto, entidad.PrecioUnitario, entidad.Color, entidad.Stock).FirstOrDefault();

                    if (dato > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                        resultado.Valor = dato.Value;
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
        [Route("Producto/ActualizarProducto")]
        public Resultado ActualizarProducto(Producto entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ActualizarProducto(entidad.IdProducto, entidad.IdCategoria, entidad.NombreProducto, entidad.PrecioUnitario, entidad.Color, entidad.Stock);

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
        [Route("Producto/ActualizarInventario")]
        public Resultado ActualizarInventario(Producto entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var resp = db.ActualizarInventario(entidad.IdProducto, entidad.Incrementar);

                    if (resp > 0)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                    }
                    else
                    {
                        resultado.Codigo = -1;
                        resultado.Detalle = "El stock del producto no se ha modificado";
                    }
                }
            }
            catch (Exception)
            {
                resultado.Codigo = -1;
                resultado.Detalle = "El stock del producto no se pudo actualizar";
            }

            return resultado;
        }

        [HttpPut]
        [Route("Producto/ActualizarImagenProducto")]
        public Resultado ActualizarImagenProducto(Producto entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var resp = db.ActualizarImagenProducto(entidad.IdProducto, entidad.ImagenProducto);

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
        [Route("Producto/DeshabilitarProducto")]
        public Resultado DeshabilitarProducto(Producto entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var resp = db.DeshabilitarProducto(entidad.IdProducto, entidad.Estado);

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

        [HttpDelete]
        [Route("Producto/EliminarProducto")]
        public Resultado EliminarProducto(long IdProducto)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.EliminarProducto(IdProducto);

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
