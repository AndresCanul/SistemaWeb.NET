using InnovaTechWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;

namespace InnovaTechWeb.Models
{
    public class OrdenModel
    {
        public ResultadoOrden ConsultarDetalleFacturas(long IdOrden)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Orden/ConsultarDetalleFacturas?IdOrden=" + IdOrden;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoOrden>().Result;
                else
                    return null;
            }
        }

        public ResultadoOrden ConsultarFacturas()
        {
            using (var client = new HttpClient())
            {
                long IdUsuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Orden/ConsultarFacturas?IdUsuario=" + IdUsuario;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoOrden>().Result;
                else
                    return null;
            }
        }

        public Resultado RegistrarOrden()
        {
            using (var client = new HttpClient())
            {
                Orden entidad = new Orden();
                entidad.IdUsuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());

                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Orden/RegistrarOrden";
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