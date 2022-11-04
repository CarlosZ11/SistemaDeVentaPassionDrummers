using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class CL_Cliente
    {
        private CD_Cliente objdc_Cliente = new CD_Cliente();

        public List<Cliente> Listar()
        {
            return objdc_Cliente.Listar();
        }

        public int Registrar(Cliente obj, out String Mensaje)
        {
            Mensaje = String.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Cliente\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo del Cliente\n";
            }

            if (Mensaje != String.Empty)
            {
                return 0;
            }
            else
            {
                return objdc_Cliente.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(Cliente obj, out String Mensaje)
        {
            Mensaje = String.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Cliente\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo del Cliente\n";
            }

            if (Mensaje != String.Empty)
            {
                return false;
            }
            else
            {
                return objdc_Cliente.Editar(obj, out Mensaje);
            }


        }

        public bool Eliminar(Cliente obj, out String Mensaje)
        {
            return objdc_Cliente.Eliminar(obj, out Mensaje);
        }
    }
}
