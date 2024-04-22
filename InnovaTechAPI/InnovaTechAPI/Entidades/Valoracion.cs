using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechAPI.Entidades
{
    public class Valoracion
    {
        public long IdValoracion { get; set; }

        public long IdUsuario { get; set; }

        public long IdProducto { get; set; }

        public int Calificacion { get; set; }
    }

    public class ResultadoValoracion
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public object Dato { get; set; }

        public object Datos { get; set; }
    }
}