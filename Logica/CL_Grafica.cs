using Datos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class CL_Grafica
    {
        private CD_Grafica objcd_Grafica = new CD_Grafica();

        //public DataTable ConsultarClientesProductos()
        //{
        //    return objcd_Grafica.ConsultarClientesProductos();
        //}

        public ArrayList ListaCliente()
        {
            return objcd_Grafica.ListaCliente();
        }

        public ArrayList NumeroProducto()
        {
            return objcd_Grafica.NumeroProducto();
        }

    }
}
