using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechWeb.Entidades
{
    public class Rol
    {
        public long IdRol { get; set; }

        public string NombreRol { get; set; }

        public string DescripcionRol { get; set; }

        public bool Estado { get; set; }

        public string ImagenRol { get; set; }
    }

    public class ResultadoRol
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public List<Rol> Datos { get; set; }

        public Rol Dato { get; set; }
    }
}