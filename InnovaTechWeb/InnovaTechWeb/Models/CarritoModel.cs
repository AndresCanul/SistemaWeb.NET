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
    public class CarritoModel
    {
        public ResultadoCarrito ConsultarAgregado(long IdProducto)
        {
            using (var client = new HttpClient())
            {
                long IdUsuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Carrito/ConsultarAgregado?IdUsuario=" + IdUsuario + "&IdProducto=" + IdProducto;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoCarrito>().Result;
                else
                    return null;
            }
        }

        public ResultadoCarrito ConsultarCarrito()
        {
            using (var client = new HttpClient())
            {
                long IdUsuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Carrito/ConsultarCarrito?IdUsuario=" + IdUsuario;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoCarrito>().Result;
                else
                    return null;
            }
        }

        public Resultado AgregarCarrito(Carrito entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Carrito/AgregarCarrito";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PostAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public Resultado ActualizarCarrito(Carrito entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Carrito/ActualizarCarrito";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PutAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public Resultado EliminarCarrito(long IdCarrito)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Carrito/EliminarCarrito?Id=" + IdCarrito;
                var respuesta = client.DeleteAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }
    }
}