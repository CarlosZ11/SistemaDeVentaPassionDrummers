namespace Presentacion_GUI.Formularios
{
    partial class Graficas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chrtProductosCompradosPorClientes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chrtInventarioDeProductos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chrtProductosCompradosPorClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtInventarioDeProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // chrtProductosCompradosPorClientes
            // 
            this.chrtProductosCompradosPorClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            chartArea1.Name = "ChartArea1";
            this.chrtProductosCompradosPorClientes.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(222)))), ((int)(((byte)(255)))));
            legend1.Enabled = false;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend1.Name = "Legend1";
            this.chrtProductosCompradosPorClientes.Legends.Add(legend1);
            this.chrtProductosCompradosPorClientes.Location = new System.Drawing.Point(16, 15);
            this.chrtProductosCompradosPorClientes.Margin = new System.Windows.Forms.Padding(4);
            this.chrtProductosCompradosPorClientes.Name = "chrtProductosCompradosPorClientes";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.chrtProductosCompradosPorClientes.Series.Add(series1);
            this.chrtProductosCompradosPorClientes.Size = new System.Drawing.Size(631, 251);
            this.chrtProductosCompradosPorClientes.TabIndex = 1;
            this.chrtProductosCompradosPorClientes.Text = "chart2";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(7)))), ((int)(((byte)(76)))));
            title1.Name = "Title1";
            title1.Text = "Cantidad de productos comprados por cliente";
            this.chrtProductosCompradosPorClientes.Titles.Add(title1);
            // 
            // chrtInventarioDeProductos
            // 
            chartArea2.Name = "ChartArea1";
            this.chrtInventarioDeProductos.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chrtInventarioDeProductos.Legends.Add(legend2);
            this.chrtInventarioDeProductos.Location = new System.Drawing.Point(16, 341);
            this.chrtInventarioDeProductos.Name = "chrtInventarioDeProductos";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chrtInventarioDeProductos.Series.Add(series2);
            this.chrtInventarioDeProductos.Size = new System.Drawing.Size(631, 300);
            this.chrtInventarioDeProductos.TabIndex = 2;
            this.chrtInventarioDeProductos.Text = "chart1";
            // 
            // Graficas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 718);
            this.Controls.Add(this.chrtInventarioDeProductos);
            this.Controls.Add(this.chrtProductosCompradosPorClientes);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Graficas";
            this.Text = "Graficas";
            this.Load += new System.EventHandler(this.Graficas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chrtProductosCompradosPorClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtInventarioDeProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtProductosCompradosPorClientes;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtInventarioDeProductos;
    }
}