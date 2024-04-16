﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechWeb.Entidades
{
    public class Ubicacion
    {
        public long IdUbicacion { get; set; }

        public string NombreDistrito { get; set; }

        public long IdCanton { get; set; }

        public string NombreCanton { get; set; }

        public long IdProvincia { get; set; }

        public string NombreProvincia { get; set; }
    }
    public class ResultadoUbicacion
    {
        public int Codigo { get; set; }

        public string Detalle { get; set; }

        public List<Ubicacion> Datos { get; set; }

        public Ubicacion Dato { get; set; }
    }
}