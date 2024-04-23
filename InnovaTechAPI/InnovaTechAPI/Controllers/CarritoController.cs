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
    public class CarritoController : ApiController
    {
        [HttpGet]
        [Route("Carrito/ConsultarAgregado")]
        public Resultado ConsultarAgregado(long IdUsuario, long IdProducto)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ConsultarAgregado(IdUsuario, IdProducto).FirstOrDefault();

                    if (dato != null)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                        resultado.Valor = dato.Value;
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
        [Route("Carrito/ConsultarCarrito")]
        public ResultadoCarrito ConsultarCarrito(long IdUsuario)
        {
            var resultado = new ResultadoCarrito();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var datos = db.ConsultarCarrito(IdUsuario).ToList();

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
        [Route("Carrito/AgregarCarrito")]
        public Resultado AgregarCarrito(Carrito entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.AgregarCarrito(entidad.IdUsuario, entidad.IdProducto);

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
        [Route("Carrito/ActualizarCarrito")]
        public Resultado ActualizarCarrito(Carrito entidad)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ActualizarCarrito(entidad.IdCarrito, entidad.Cantidad);

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
        [Route("Carrito/EliminarCarrito")]
        public Resultado EliminarCarrito(long IdCarrito)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.EliminarProducto(IdCarrito);

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
