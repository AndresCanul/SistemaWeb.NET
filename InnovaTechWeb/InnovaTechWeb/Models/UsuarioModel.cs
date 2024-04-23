using InnovaTechWeb.Entidades;
using System.Configuration;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;


namespace InnovaTechWeb.Models
{
    public class UsuarioModel
    {
        public ResultadoUsuario ConsultarUsuario(long IdUsuario)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/ConsultarUsuario?IdUsuario=" + IdUsuario;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoUsuario>().Result;
                else
                    return null;
            }
        }

        public ResultadoUsuario VisualizarPerfil()
        {
            using (var client = new HttpClient())
            {
                long IdUsuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/VisualizarPerfil?IdUsuario=" + IdUsuario;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoUsuario>().Result;
                else
                    return null;
            }
        }

        public ResultadoUsuario ConsultarUsuarios()
        {
            using (var client = new HttpClient())
            {
                long IdUsuario = long.Parse(HttpContext.Current.Session["IdUsuario"].ToString());
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/ConsultarUsuarios?IdUsuario=" + IdUsuario;
                var respuesta = client.GetAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoUsuario>().Result;
                else
                    return null;
            }
        }

        public Resultado RegistrarUsuario(Usuario entidad)
        {            
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/RegistrarUsuario";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PostAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public ResultadoUsuario IniciarSesionUsuario(Usuario entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/IniciarSesionUsuario";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PostAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<ResultadoUsuario>().Result;
                else
                    return null;
            }
        }

        public Resultado RecuperarAccesoUsuario(Usuario entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/RecuperarAccesoUsuario";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PostAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public Resultado ActualizarUsuario(Usuario entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/ActualizarUsuario";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PutAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public Resultado ActualizarPerfil(Usuario entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/ActualizarPerfil";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PutAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public Resultado ActualizarImagenUsuario(Usuario entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/ActualizarImagenUsuario";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PutAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public Resultado CambiarClave(Usuario entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/CambiarClave";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PutAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public Resultado DeshabilitarUsuario(Usuario entidad)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/DeshabilitarUsuario";
                JsonContent jsonEntidad = JsonContent.Create(entidad);
                var respuesta = client.PutAsync(url, jsonEntidad).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }

        public Resultado EliminarUsuario(long IdUsuario)
        {
            using (var client = new HttpClient())
            {
                string url = ConfigurationManager.AppSettings["urlWebApi"] + "Usuario/EliminarUsuario?IdUsuario=" + IdUsuario;
                var respuesta = client.DeleteAsync(url).Result;

                if (respuesta.IsSuccessStatusCode)
                    return respuesta.Content.ReadFromJsonAsync<Resultado>().Result;
                else
                    return null;
            }
        }
    }
}