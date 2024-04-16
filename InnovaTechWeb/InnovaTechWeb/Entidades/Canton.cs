using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechWeb.Entidades
{
    public class Canton
    {
        public long IdCanton { get; set; }

        public long IdProvincia { get; set; }

        public string NombreCanton { get; set; }
    }
}