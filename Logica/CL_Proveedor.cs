using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class CL_Proveedor
    {
        private CD_Proveedor objdc_Proveedor = new CD_Proveedor();

        public List<Proveedor> Listar()
        {
            return objdc_Proveedor.Listar();
        }

        public int Registrar(Proveedor obj, out String Mensaje)
        {
            Mensaje = String.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }

            if (obj.RazonSocial == "")
            {
                Mensaje += "Es necesaria la razon social del Proveedor\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo del Proveedor\n";
            }

            if (Mensaje != String.Empty)
            {
                return 0;
            }
            else
            {
                return objdc_Proveedor.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(Proveedor obj, out String Mensaje)
        {
            Mensaje = String.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del Proveedor\n";
            }

            if (obj.RazonSocial == "")
            {
                Mensaje += "Es necesaria la razon social del Proveedor\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es necesario el correo del Proveedor\n";
            }

            if (Mensaje != String.Empty)
            {
                return false;
            }
            else
            {
                return objdc_Proveedor.Editar(obj, out Mensaje);
            }


        }

        public bool Eliminar(Proveedor obj, out String Mensaje)
        {
            return objdc_Proveedor.Eliminar(obj, out Mensaje);
        }
    }
}
