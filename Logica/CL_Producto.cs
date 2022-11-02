using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class CL_Producto
    {
        private CD_Producto objdc_Producto = new CD_Producto();

        public List<Producto> Listar()
        {
            return objdc_Producto.Listar();
        }

        public int Registrar(Producto obj, out String Mensaje)
        {
            Mensaje = String.Empty;

            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el código del Producto\n";
            }

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del Producto\n";
            }

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la descripcion del Producto\n";
            }

            if (Mensaje != String.Empty)
            {
                return 0;
            }
            else
            {
                return objdc_Producto.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(Producto obj, out String Mensaje)
        {
            Mensaje = String.Empty;

            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el código del Producto\n";
            }

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del Producto\n";
            }

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la descripcion del Producto\n";
            }

            if (Mensaje != String.Empty)
            {
                return false;
            }
            else
            {
                return objdc_Producto.Editar(obj, out Mensaje);
            }


        }

        public bool Eliminar(Producto obj, out String Mensaje)
        {
            return objdc_Producto.Eliminar(obj, out Mensaje);
        }
    }
}
