using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechAPI.Entidades
{
    public class Carrito
    {
        public long IdCarrito { get; set; }

        public long IdUsuario { get; set; }

        public long IdProducto { get; set; }

        public System.DateTime FechaCarrito { get; set; }

        public int Cantidad { get; set; }

        public decimal Impuestos { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public decimal PrecioUnitario { get; set; }
    }

    public class ResultadoCarrito
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public object Dato { get; set; }

        public object Datos { get; set; }
    }
}