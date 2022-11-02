using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public String Documento { get; set; }
        public String NombreCOmpleto { get; set; }
        public String Correo { get; set; }
        public String Telefono { get; set; }
        public bool Estado { get; set; }
        public String FechaRegistro { get; set; }
    }
}
