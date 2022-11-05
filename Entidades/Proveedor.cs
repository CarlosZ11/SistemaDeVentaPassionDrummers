using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Proveedor : Persona
    {
        //public int IdProveedor { get; set; }
        //public String Documento { get; set; }
        public String RazonSocial { get; set; }
        //public String Correo { get; set; }
        public String Telefono { get; set; }
        public bool Estado { get; set; }
        public String FechaRegistro { get; set; }
    }
}
