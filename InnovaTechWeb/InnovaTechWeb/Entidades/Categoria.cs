using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechWeb.Entidades
{
    public class Categoria
    {
        public long IdCategoria { get; set; }

        public string NombreCategoria { get; set; }

        public string DescripcionCategoria { get; set; }

        public bool Estado { get; set; }

        public string IconoCategoria { get; set; }
    }

    public class ResultadoCategoria
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public Categoria Dato { get; set; }

        public List<Categoria> Datos { get; set; }
    }
}