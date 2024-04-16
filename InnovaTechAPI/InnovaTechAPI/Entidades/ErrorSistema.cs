using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovaTechAPI.Entidades
{
    public class ErrorSistema
    {
        public long IdError { get; set; }
        public int CodigoError { get; set; }
        public string MensajeError { get; set; }
    }
}