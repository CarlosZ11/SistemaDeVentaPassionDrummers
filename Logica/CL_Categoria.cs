using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class CL_Categoria
    {
        private CD_Categoria objdc_categoria = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objdc_categoria.Listar();
        }

        public int Registrar(Categoria obj, out String Mensaje)
        {
            Mensaje = String.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la descripcion de la Categoria\n";
            }

            if (Mensaje != String.Empty)
            {
                return 0;
            }
            else
            {
                return objdc_categoria.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(Categoria obj, out String Mensaje)
        {
            Mensaje = String.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario la descripcion de la Categoria\n";
            }

            if (Mensaje != String.Empty)
            {
                return false;
            }
            else
            {
                return objdc_categoria.Editar(obj, out Mensaje);
            }


        }

        public bool Eliminar(Categoria obj, out String Mensaje)
        {
            return objdc_categoria.Eliminar(obj, out Mensaje);
        }
    }
}
