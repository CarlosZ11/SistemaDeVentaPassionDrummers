using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class ServicioClientes : IServicios<Cliente>
    {
        private RepositorioCliente repositorioCliente = new RepositorioCliente();

        public bool Editar(Cliente obj, out string Mensaje)
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
                return repositorioCliente.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            return repositorioCliente.Eliminar(obj, out Mensaje);
        }

        public List<Cliente> Listar()
        {
            return repositorioCliente.Listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
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
                return repositorioCliente.Registrar(obj, out Mensaje);
            }
        }
    }
}