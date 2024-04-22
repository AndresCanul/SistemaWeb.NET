using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechAPI.Entidades
{
    public class Producto
    {
        public long IdProducto { get; set; }

        public long IdInventario { get; set; }

        public long IdCategoria { get; set; }

        public string NombreProducto { get; set; }

        public string NombreCategoria { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int Stock { get; set; }

        public string Color { get; set; }

        public bool Estado { get; set; }

        public bool Incrementar { get; set; }

        public string ImagenProducto { get; set; }
    }

    public class ResultadoProducto
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public object Dato { get; set; }

        public object Datos { get; set; }
    }
}