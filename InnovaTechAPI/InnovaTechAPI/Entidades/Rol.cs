using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechAPI.Entidades
{
    public class Rol
    {
        public long IdRol { get; set; }

        public string NombreRol { get; set; }

        public string DescripcionRol { get; set; }

        public bool Estado { get; set; }

        public string IconoRol { get; set; }
    }

    public class ResultadoRol
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public object Dato { get; set; }

        public object Datos { get; set; }
    }
}