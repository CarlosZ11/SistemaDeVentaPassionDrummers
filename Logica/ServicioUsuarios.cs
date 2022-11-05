using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class ServicioUsuarios : IServicios<Usuario>
    {

        private RepositorioUsuario repositorioUsuario = new RepositorioUsuario();

        public bool Editar(Usuario obj, out string Mensaje)
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
                return repositorioUsuario.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return repositorioUsuario.Eliminar(obj, out Mensaje);
        }

        public List<Usuario> Listar()
        {
            return repositorioUsuario.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
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
                return 0;
            }
            else
            {
                return repositorioUsuario.Registrar(obj, out Mensaje);
            }
        }
    }
}