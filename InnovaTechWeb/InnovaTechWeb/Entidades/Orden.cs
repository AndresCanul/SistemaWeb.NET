using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechWeb.Entidades
{
    public class Orden
    {
        public long IdOrden { get; set; }

        public long IdUsuario { get; set; }

        public long IdProducto { get; set; }

        public long IdDetalleOrden { get; set; }

        public string NombreUsuario { get; set; }

        public string NombreProducto { get; set; }

        public int Cantidad { get; set; }

        public System.DateTime FechaOrden { get; set; }

        public decimal Precio { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Impuestos { get; set; }

        public decimal Total { get; set; }
    }

    public class ResultadoOrden
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public Orden Dato { get; set; }

        public List<Orden> Datos { get; set; }
    }
}