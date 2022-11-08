using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FontAwesome.Sharp;
using Presentacion_GUI.Formularios;
using Color = System.Drawing.Color;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using Entidades;

namespace Presentacion_GUI.Formularios
{
    public partial class Inicio : Form
    {
        //Campos
        private IconButton currentBtn; //Botón actual
        private Panel leftBorderBtn; //Borde izquierdo del botón
        private Form currentChildForm; //formulario hijo actual
        private static Usuario usuarioActual;
        private static IconButton MenuActivo = null;
        private static Form FormularioActivo = null;

        //Constructor
        public Inicio(Usuario objUsuario)
        {
            InitializeComponent();
            usuarioActual = objUsuario;

            //Validar Usuario
            if (usuarioActual.oRol.IdRol != 1)
            {
                //btnUsuarios.Enabled = false;
                btnUsuarios.Visible = false;
                //btnAdministracion.Enabled = false;
                btnAdministracion.Visible = false;
                //btnReportes.Enabled = false;
                btnReportes.Visible = false;
                btnEmpresa.Visible = false;
                
            }

            PersonalizarDiseño();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 50); //Asignamos un tamaño
            panelMenu.Controls.Add(leftBorderBtn);
            //formulario
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        

        //Estructuras
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
            public static Color color7 = Color.FromArgb(0, 243, 214);
            public static Color color8 = Color.FromArgb(170, 78, 170);
        }

        //Métodos

        public void PersonalizarDiseño()
        {
            panelSubmenuAdmin.Visible = false;
            panelSubmenuVentas.Visible = false;
            panelSubmenuCompras.Visible = false;
        }

        public void OcultarSubMenu()
        {
            if (panelSubmenuAdmin.Visible == true)
            {
                panelSubmenuAdmin.Visible = false;
            }
            if (panelSubmenuVentas.Visible == true)
            {
                panelSubmenuVentas.Visible = false;
            }
            if (panelSubmenuCompras.Visible == true)
            {
                panelSubmenuCompras.Visible = false;
            }
        }

        public void MostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                OcultarSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void ActivarBoton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DesactivarBoton();
                //Botón
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(47, 7, 76);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Borde izquierdo del botón
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //Icono del formulario segundario actual
                iconFormulario.IconChar = currentBtn.IconChar;
                //TituloFormulario.Text = currentBtn.Text;
                iconFormulario.IconColor = color;

            }
        }

        private void DesactivarBoton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(233, 222, 255);
                currentBtn.ForeColor = Color.FromArgb(47, 7, 76);
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(47, 7, 76);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //Fin
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.BackColor = Color.FromArgb(175, 161, 205);
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            TituloFormulario.Text = childForm.Text;
        }

        public void AbrirFormulario(IconButton menu, Form formulario)
        {
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.FromArgb(175, 161, 205);
            panelDesktop.Controls.Add(formulario);
            panelDesktop.Tag = formulario;
            formulario.BringToFront();
            formulario.Show();
            TituloFormulario.Text = formulario.Text;

        }


        private void btnVentas_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubmenuVentas);
            ActivarBoton(sender, RGBColors.color3);
            TituloFormulario.Text = btnVentas.Text;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            ActivarBoton(sender, RGBColors.color5);
            OpenChildForm(new Clientes());
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            ActivarBoton(sender, RGBColors.color6);
            OpenChildForm(new Proveedores());
        }

        private void btnAdministracion_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubmenuAdmin);
            ActivarBoton(sender, RGBColors.color2);
            TituloFormulario.Text = btnAdministracion.Text;
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubmenuCompras);
            ActivarBoton(sender, RGBColors.color4);
            TituloFormulario.Text = btnCompras.Text;
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            ActivarBoton(sender, RGBColors.color7);
            OpenChildForm(new Reportes());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            ActivarBoton(sender, RGBColors.color1);
            //OpenChildForm(new Usuarios());
            AbrirFormulario((IconButton)sender, new Usuarios());
        }

        private void btnAcercaDe_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            ActivarBoton(sender, RGBColors.color8);
            OpenChildForm(new NegocioPD());
        }

        //Arrastrar formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnSubMenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            AbrirFormulario(btnVentas, new Ventas());
            TituloFormulario.Text = btnSubMenuRegistrarVenta.Text;
            iconFormulario.IconChar = btnSubMenuRegistrarVenta.IconChar;
        }

        private void btnSubMenuVerDetalleVentas_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            AbrirFormulario(btnVentas, new DetalleVenta());
            TituloFormulario.Text = btnSubMenuVerDetalleVentas.Text;
            iconFormulario.IconChar = btnSubMenuVerDetalleVentas.IconChar;
        }

        private void btnSubMenuCategoriaAdmin_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            AbrirFormulario(btnAdministracion, new Categorias());
            TituloFormulario.Text = btnSubMenuCategoriaAdmin.Text;
            iconFormulario.IconChar = btnSubMenuCategoriaAdmin.IconChar;
        }

        private void btnSubMenuProductosAdmin_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            AbrirFormulario(btnAdministracion, new Productos());
            TituloFormulario.Text = btnSubMenuProductosAdmin.Text;
            iconFormulario.IconChar = btnSubMenuProductosAdmin.IconChar;
        }

        private void btnSubMenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            AbrirFormulario(btnCompras, new Compras(usuarioActual));
            TituloFormulario.Text = btnSubMenuRegistrarCompra.Text;
            iconFormulario.IconChar = btnSubMenuRegistrarCompra.IconChar;
        }

        private void btnSubMenuVerDetalleCompra_Click(object sender, EventArgs e)
        {
            OcultarSubMenu();
            AbrirFormulario(btnCompras, new DetalleCompra());
            TituloFormulario.Text = btnSubMenuVerDetalleCompra.Text;
            iconFormulario.IconChar = btnSubMenuVerDetalleCompra.IconChar;
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            Size = new Size(1163, 631);
            MostrarSubMenu(panelSubmenuVentas);
            OpenChildForm(new Ventas());
            TituloFormulario.Text = btnSubMenuRegistrarVenta.Text;
            ActivarBoton(btnVentas, RGBColors.color3);
            iconFormulario.IconChar = btnSubMenuRegistrarVenta.IconChar;
            lblUsuario.Text = usuarioActual.NombreCompleto;
            //btnMaximize.Visible = false;
        }
    }
}
