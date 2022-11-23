using Logica;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Presentacion_GUI.Formularios
{
    public partial class Graficas : Form
    {
        public Graficas()
        {
            InitializeComponent();
        }

        private void Graficas_Load(object sender, EventArgs e)
        {
            //DataTable dt = new CL_Grafica().ConsultarClientesProductos();

            //chart1.Titles.Add("Mejores Clientes");
            //chart1.ChartAreas[0].AxisX.Title = "Cantidad de productos comprados";
            ////chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;

            //foreach (DataRow row in dt.Rows)
            //{
            //    Series series = chart1.Series.Add(row["NombreCliente"].ToString());
            //    series.Points.Add(Convert.ToDouble(row["Cantidad de productos comprados"].ToString()));
            //    series.Label = row["Cantidad de productos comprados"].ToString();

            //}

            ArrayList listaCliente = new CL_Grafica().ListaCliente();
            ArrayList numeroProductos = new CL_Grafica().NumeroProducto();

            chart2.Series[0].Points.DataBindXY(listaCliente, numeroProductos);
            chart2.ChartAreas[0].AxisX.Title = "Clientes";
            chart2.ChartAreas[0].AxisY.Title = "Cantidad de productos";


        }
    }
}
