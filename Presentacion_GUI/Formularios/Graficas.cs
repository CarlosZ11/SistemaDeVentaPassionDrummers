using Datos;
using DocumentFormat.OpenXml.EMMA;
using Entidades;
using Logica;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Presentacion_GUI.Formularios
{
    public partial class Graficas : Form
    {
        private CD_Grafica modelo;

        public Graficas()
        {
            InitializeComponent();
            modelo = new CD_Grafica();
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            modelo.fechaInicio = dtpStartDate.Value;
            dtpEndDate.Value = DateTime.Now;
            modelo.fechaFin = dtpEndDate.Value;
            btnLast7Days.Select();
            CargarDatos();

        }

        private void CargarDatos()
        {
            var refreshData = modelo.CargarDatos(dtpStartDate.Value, dtpEndDate.Value);
            if (refreshData == true)
            {
                lblNumOrders.Text = modelo.NumeroVentas.ToString();
                lblTotalRevenue.Text = "$" + modelo.TotalIngresos.ToString();

                lblNumCustomers.Text = modelo.NumeroClientes.ToString();
                lblNumSuppliers.Text = modelo.NumeroProveedores.ToString();
                lblNumProducts.Text = modelo.NumeroProductos.ToString();

                chartGrossRevenue.DataSource = modelo.ListaIngresosBrutos;
                chartGrossRevenue.Series[0].XValueMember = "Fecha";
                chartGrossRevenue.Series[0].YValueMembers = "cantidadTotal";
                chartGrossRevenue.DataBind();

                chartTopProducts.DataSource = modelo.ProductosMasVendidos;
                chartTopProducts.Series[0].XValueMember = "Key";
                chartTopProducts.Series[0].YValueMembers = "Value";
                chartTopProducts.DataBind();

                dgvUnderstock.DataSource = modelo.ProductosBajoStock;
                dgvUnderstock.Columns[0].HeaderText = "Producto";
                dgvUnderstock.Columns[1].HeaderText = "Unidades";
                Console.WriteLine("Vista cargada :)");
            }
            else Console.WriteLine("Vista no cargada, misma consulta");
        }

        private void DisableCustomDates()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOkCustomDate.Visible = false;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Now;
            CargarDatos();
            DisableCustomDates();
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            CargarDatos();
            DisableCustomDates();
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            CargarDatos();
            DisableCustomDates();
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Now;
            CargarDatos();
            DisableCustomDates();
        }

        private void btnCustomDate_Click(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;
            btnOkCustomDate.Visible = true;
        }

        private void btnOkCustomDate_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}
