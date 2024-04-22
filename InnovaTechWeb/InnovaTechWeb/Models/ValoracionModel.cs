using InnovaTechWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;

namespace InnovaTechWeb.Models
{
    public class ValoracionModel
    {
        public ResultadoValoracion ConsultarValoracion(long IdProducto)
        {
            using (var client = new HttpClient())
            {
                long IdUsuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Valoracion/ConsultarValoracion?IdUsuario=" + IdUsuario + "&IdProducto=" + IdProducto;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoValoracion>().Result;
                else
                    return null;
            }
        }

        public Resultado AgregarValoracion(Valoracion entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Valoracion/AgregarValoracion";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PostAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }
    }
}