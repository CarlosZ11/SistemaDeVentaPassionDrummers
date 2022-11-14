using Entidades;
using Logica;
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

namespace Presentacion_GUI.Modales
{
    public partial class mdCliente : Form
    {
        ServicioClientes servicioClientes = new ServicioClientes();
        public Cliente _Cliente { get; set; }   
        public mdCliente()
        {
            InitializeComponent();
        }

        private void mdCliente_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                cboBusqueda.Items.Add(new OpcionCombo() { valor = columna.Name, texto = columna.HeaderText });
            }
            cboBusqueda.DisplayMember = "texto";
            cboBusqueda.ValueMember = "valor";
            cboBusqueda.SelectedIndex = 0;

            //Mostrar todos los clientes
            List<Cliente> lista = servicioClientes.Listar();

            foreach (Cliente item in lista)
            {
                if (item.Estado)
                {
                    dgvData.Rows.Add(new object[] { item.Documento, item.NombreCompleto });
                }
            }
        }
    }
}
