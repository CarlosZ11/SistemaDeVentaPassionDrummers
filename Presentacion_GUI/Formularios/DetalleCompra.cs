using Entidades;
using Logica;
using Presentacion_GUI.Utilidades;
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
    public partial class DetalleCompra : Form
    {
        public DetalleCompra()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Compra oCompra = new CL_Compra().ObtenerCompra(txtBusqueda.Text);

            if (oCompra.IdCompra != 0)
            {
                txtNumeroDocumento.Text = oCompra.NumeroDocumento;
                txtFecha.Text = oCompra.FechaRegistro;
                txtTipoDocumento.Text = oCompra.TipoDocumento;
                txtUsuario.Text = oCompra.oUsuario.NombreCompleto;
                txtDocProveedor.Text = oCompra.oProveedor.Documento;
                txtNombreProveedor.Text = oCompra.oProveedor.RazonSocial;

                dgvData.Rows.Clear();

                foreach (Detalle_Compra dc in oCompra.oDetalleCompra)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        dc.oProducto.Nombre,
                        dc.PrecioCompra,
                        dc.Cantidad,
                        dc.MontoTotal
                    });
                }
                txtMontoTotal.Text = oCompra.MontoTotal.ToString("C", CultureInfo.CurrentCulture);
            }

        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocProveedor.Text = "";
            txtNombreProveedor.Text = "";
            dgvData.Rows.Clear();
            txtMontoTotal.Text = "0.00";
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Compra oCompra = new CL_Compra().ObtenerCompra(txtBusqueda.Text);

                if (oCompra.IdCompra != 0)
                {
                    txtNumeroDocumento.Text = oCompra.NumeroDocumento;
                    txtFecha.Text = oCompra.FechaRegistro;
                    txtTipoDocumento.Text = oCompra.TipoDocumento;
                    txtUsuario.Text = oCompra.oUsuario.NombreCompleto;
                    txtDocProveedor.Text = oCompra.oProveedor.Documento;
                    txtNombreProveedor.Text = oCompra.oProveedor.RazonSocial;

                    dgvData.Rows.Clear();

                    foreach (Detalle_Compra dc in oCompra.oDetalleCompra)
                    {
                        dgvData.Rows.Add(new object[]
                        {
                        dc.oProducto.Nombre,
                        dc.PrecioCompra,
                        dc.Cantidad,
                        dc.MontoTotal
                        });
                    }
                    txtMontoTotal.Text = oCompra.MontoTotal.ToString("C", CultureInfo.CurrentCulture);
                }
            }
        }
    }
}
