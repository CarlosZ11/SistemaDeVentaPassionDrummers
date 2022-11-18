using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion_GUI.Formularios
{
    public partial class DetalleVenta : Form
    {
        public DetalleVenta()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Venta oVenta = new CL_Venta().ObtenerVenta(txtBusqueda.Text);

            if (oVenta.IdVenta != 0)
            {
                txtFecha.Text = oVenta.FechaRegistro;
                txtTipoDocumento.Text = oVenta.TipoDocumento;
                txtUsuario.Text = oVenta.oUsuario.NombreCompleto;
                txtDocCliente.Text = oVenta.DocumentoCLiente;
                txtNombreCliente.Text = oVenta.NombreCliente;
                dgvData.Rows.Clear();

                foreach (Detalle_Venta dv in oVenta.oDetalle_Venta)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        dv.oProducto.Nombre,
                        dv.PrecioVenta,
                        dv.Cantidad,
                        dv.SubTotal
                    });
                }

                txtMontoTotal.Text = oVenta.MontoTotal.ToString("C", CultureInfo.CurrentCulture);
                txtMontoPago.Text = oVenta.MontoPago.ToString("C", CultureInfo.CurrentCulture);
                txtMontoCambio.Text = oVenta.MontoCambio.ToString("C", CultureInfo.CurrentCulture);

            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocCliente.Text = "";
            txtNombreCliente.Text = "";
            dgvData.Rows.Clear();
            txtMontoTotal.Text = "0.00";
            txtMontoPago.Text = "0.00";
            txtMontoCambio.Text = "0.00";
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Venta oVenta = new CL_Venta().ObtenerVenta(txtBusqueda.Text);

                if (oVenta.IdVenta != 0)
                {
                    txtFecha.Text = oVenta.FechaRegistro;
                    txtTipoDocumento.Text = oVenta.TipoDocumento;
                    txtUsuario.Text = oVenta.oUsuario.NombreCompleto;
                    txtDocCliente.Text = oVenta.DocumentoCLiente;
                    txtNombreCliente.Text = oVenta.NombreCliente;
                    dgvData.Rows.Clear();

                    foreach (Detalle_Venta dv in oVenta.oDetalle_Venta)
                    {
                        dgvData.Rows.Add(new object[]
                        {
                        dv.oProducto.Nombre,
                        dv.PrecioVenta,
                        dv.Cantidad,
                        dv.SubTotal
                        });
                    }

                    txtMontoTotal.Text = oVenta.MontoTotal.ToString("C", CultureInfo.CurrentCulture);
                    txtMontoPago.Text = oVenta.MontoPago.ToString("C", CultureInfo.CurrentCulture);
                    txtMontoCambio.Text = oVenta.MontoCambio.ToString("C", CultureInfo.CurrentCulture);

                }
            }
        }
    }
}
