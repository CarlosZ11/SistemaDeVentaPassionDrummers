using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class CL_Usuario
    {
        private CD_Usuario objdc_usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objdc_usuario.Listar();
        }

        public int Registrar(Usuario obj, out String Mensaje)
        {
            Mensaje =  String.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es necesario la clave del usuario\n";
            }

            if(Mensaje != String.Empty)
            {
                return 0;
            }
            else
            {
                return objdc_usuario.Registrar(obj, out Mensaje);
            }
            
        }

        public bool Editar(Usuario obj, out String Mensaje)
        {
            Mensaje = String.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es necesario la clave del usuario\n";
            }

            if (Mensaje != String.Empty)
            {
                return false;
            }
            else
            {
                return objdc_usuario.Editar(obj, out Mensaje);
            }

            
        }

        public bool Eliminar(Usuario obj, out String Mensaje)
        {
            return objdc_usuario.Eliminar(obj, out Mensaje);
        }
    }
}
