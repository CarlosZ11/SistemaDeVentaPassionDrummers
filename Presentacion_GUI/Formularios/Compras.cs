using Entidades;
using Presentacion_GUI.Modales;
using Presentacion_GUI.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion_GUI.Formularios
{
    public partial class Compras : Form
    {
        private Usuario _Usuario;

        public Compras(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void Compras_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new OpcionCombo() { valor = "Boleta", texto = "Boleta" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { valor = "Factura", texto = "Factura" });
            cboTipoDocumento.DisplayMember = "texto";
            cboTipoDocumento.ValueMember = "valor";
            cboTipoDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdProveedor.Text = "0";
            txtIdProducto.Text = "0";

        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();

                if(result == DialogResult.OK)
                {
                    txtIdProveedor.Text = modal._Proveedor.Id.ToString();
                    txtDocProveedor.Text = modal._Proveedor.Documento;
                    txtNombreProveedor.Text = modal._Proveedor.RazonSocial;
                }
                else
                {
                    txtDocProveedor.Select();
                }
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._Producto.IdProducto.ToString();
                    txtCodProducto.Text = modal._Producto.Codigo;
                    txtProducto.Text = modal._Producto.Nombre;
                    txtPrecioCompra.Select();
                }
                else
                {
                    txtCodProducto.Select();
                }
            }
        }
    }
}
