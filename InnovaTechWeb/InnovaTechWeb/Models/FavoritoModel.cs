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
    public class FavoritoModel
    {
        public ResultadoFavorito ConsultarFavorito(long IdProducto)
        {
            using (var client = new HttpClient())
            {
                long IdUsuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Favorito/ConsultarFavorito?IdUsuario=" + IdUsuario + "&IdProducto=" + IdProducto;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoFavorito>().Result;
                else
                    return null;
            }
        }

        public ResultadoFavorito ConsultarFavoritos(long IdUsuario)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Favorito/ConsultarFavoritos?Id=" + IdUsuario;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoFavorito>().Result;
                else
                    return null;
            }
        }

        public Resultado AgregarFavorito(long IdProducto)
        {
            using (var client = new HttpClient())
            {
                Favorito entidad = new Favorito();
                entidad.IdProducto = IdProducto;

                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Favorito/AgregarFavorito";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PostAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public Resultado EliminarFavorito(long IdProducto)
        {
            using (var client = new HttpClient())
            {
                long IdUsuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Favorito/EliminarFavorito?IdUsuario=" + IdUsuario + "&IdProducto=" + IdProducto;
                var respuesta = client.DeleteAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }
    }
}