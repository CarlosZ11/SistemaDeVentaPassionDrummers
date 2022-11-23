using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CD_Grafica
    {
        //public DataTable ConsultarClientesProductos()
        //{
        //    DataTable tablaCP = new DataTable();

        //    using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
        //    {
        //        try
        //        {
        //            StringBuilder query = new StringBuilder();
        //            query.AppendLine("select v.NombreCliente,");
        //            query.AppendLine("sum(cantidad) [Cantidad de productos comprados]");
        //            query.AppendLine("from VENTA v");
        //            query.AppendLine("inner join CLIENTE c on c.NombreCompleto = v.NombreCliente");
        //            query.AppendLine("inner join DETALLE_VENTA dv on dv.IdVenta = v.IdVenta");
        //            query.AppendLine("group by v.NombreCliente");

        //            SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
        //            SqlDataAdapter data = new SqlDataAdapter(cmd);

        //            oconexion.Open();

        //            data.Fill(tablaCP);

        //        }
        //        catch (Exception ex)
        //        {

        //            tablaCP = new DataTable();
        //        }
        //    }
        //    return tablaCP;
        //}

        //public ArrayList ListaCliente()
        //{
        //    ArrayList Cliente = new ArrayList();
        //    //ArrayList NumeroProductos = new ArrayList();

        //    using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_ComprasPorClientes", oconexion);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        oconexion.Open();

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                Cliente.Add(dr.GetString(0));
        //                //NumeroProductos.Add(dr.GetInt32(1));
        //            }
        //        }
        //    }
        //    return Cliente;
        //}

        //public ArrayList NumeroProducto()
        //{
        //    //ArrayList Cliente = new ArrayList();
        //    ArrayList NumeroProductos = new ArrayList();

        //    using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_ComprasPorClientes", oconexion);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        oconexion.Open();

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                //Cliente.Add(dr.GetString(0));
        //                NumeroProductos.Add(dr.GetInt32(1));
        //            }
        //        }
        //    }
        //    return NumeroProductos;
        //}

        public List<ComprasPorClientesDTO> ObtenerDetalleCompra()
        {
            List<ComprasPorClientesDTO> oLista = new List<ComprasPorClientesDTO>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();

                    SqlCommand cmd = new SqlCommand("SP_ComprasPorClientes", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new ComprasPorClientesDTO()
                            {
                                NombreCliente = dr.GetString(0),
                                NumeroProductos = dr.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<ComprasPorClientesDTO>();
            }
            return oLista;

        }

        public List<InventarioDeProductosDTO> ObtenerInventarioProductos()
        {
            List<InventarioDeProductosDTO> oLista = new List<InventarioDeProductosDTO>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();

                    String query = "SELECT Nombre,Stock FROM PRODUCTO";
                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new InventarioDeProductosDTO()
                            {
                                NombreProducto = dr.GetString(0),
                                Stock = dr.GetInt32(1)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<InventarioDeProductosDTO>();
            }
            return oLista;

        }

    }
}
