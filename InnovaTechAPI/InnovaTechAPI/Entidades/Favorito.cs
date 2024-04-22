using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechAPI.Entidades
{
    public class Favorito
    {
        public long IdFavorito { get; set; }

        public long IdUsuario { get; set; }

        public long IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public string NombreCategoria { get; set; }

        public decimal PrecioCategoria { get; set; }

        public int Stock { get; set; }

        public string Color { get; set; }

        public bool Estado { get; set; }

        public string ImagenProducto { get; set; }
    }

    public class ResultadoFavorito
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public object Dato { get; set; }

        public object Datos { get; set; }
    }
}