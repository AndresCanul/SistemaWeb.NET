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
    public class ValoracionController : ApiController
    {
        [HttpGet]
        [Route("Valoracion/ConsultarValoracion")]
        public Resultado ConsultarValoracion(long IdUsuario, long IdProducto)
        {
            var resultado = new Resultado();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.ConsultarValoracion(IdUsuario, IdProducto).FirstOrDefault();

                    if (dato != null)
                    {
                        resultado.Codigo = 0;
                        resultado.Detalle = string.Empty;
                        resultado.Valor = dato;
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
        [Route("Valoracion/AgregarValoracion")]
        public ResultadoValoracion AgregarValoracion(Valoracion entidad)
        {
            var resultado = new ResultadoValoracion();

            try
            {
                //Llamar a la base de datos
                using (var db = new InnovaTechDBEntities())
                {
                    var dato = db.AgregarValoracion(entidad.IdUsuario, entidad.IdProducto, entidad.Calificacion);

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
