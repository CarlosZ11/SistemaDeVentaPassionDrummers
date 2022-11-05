using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class ServicioProductos : IServicios<Producto>
    {
        private RepositorioProducto repositorioProducto = new RepositorioProducto();

        public bool Editar(Producto obj, out string Mensaje)
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
                return repositorioProducto.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Producto obj, out string Mensaje)
        {
            return repositorioProducto.Eliminar(obj, out Mensaje);
        }

        public List<Producto> Listar()
        {
            return repositorioProducto.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
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
                return repositorioProducto.Registrar(obj, out Mensaje);
            }
        }
    }
}