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
    public class UbicacionModel
    {
        public ResultadoUbicacion ConsultarUbicaciones()
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Ubicacion/ConsultarUbicaciones";
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoUbicacion>().Result;
                else
                    return null;
            }
        }

        public ResultadoUbicacion ConsultarDistritos()
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Ubicacion/ConsultarDistritos";
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoUbicacion>().Result;
                else
                    return null;
            }
        }

        public ResultadoUbicacion ConsultarCanton(long IdUbicacion)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Ubicacion/ConsultarCanton?IdUbicacion=" + IdUbicacion;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoUbicacion>().Result;
                else
                    return null;
            }
        }

        public ResultadoUbicacion ConsultarProvincia(long IdCanton)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Rol/ConsultarProvincia?IdCanton=" + IdCanton;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoUbicacion>().Result;
                else
                    return null;
            }
        }
    }
}