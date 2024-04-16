using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechAPI.Entidades
{
    public class Inventario
    {
        public long IdInventario { get; set; }

        public int Stock { get; set; }

        public bool Incrementar { get; set; }
    }

    public class ResultadoInventario
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public object Dato { get; set; }

        public object Datos { get; set; }
    }
}