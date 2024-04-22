using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechAPI.Entidades
{
    public class Usuario
    {
        public long IdUsuario { get; set; }

        public long IdUbicacion { get; set; }

        public long IdRol { get; set; }

        public string NombreUsuario { get; set; }

        public string ApellidoUsuario { get; set; }

        public string NombreRol { get; set; }

        public string NombreDistrito { get; set; }

        public int Edad { get; set; }

        public string Correo { get; set; }

        public bool Estado { get; set; }

        public bool Temporal { get; set; }

        public Nullable<System.DateTime> Vencimiento { get; set; }

        public string ImagenUsuario { get; set; }

        public string Clave { get; set; }

        public string ClaveNueva { get; set; }
    }

    public class ResultadoUsuario
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public object Dato { get; set; }

        public object Datos { get; set; }
    }
}