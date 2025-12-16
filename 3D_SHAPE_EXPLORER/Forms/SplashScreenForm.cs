using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Figuras3D_Proyecto
{
    public partial class SplashScreenForm : Form
    {
        public SplashScreenForm()
        {
            InitializeComponent();

            // Mostrar en el centro de la pantalla
            this.StartPosition = FormStartPosition.CenterScreen;

        }


        private void SplashScreenForm_Load(object sender, EventArgs e)
        {
            // TEXTO
            lblTitulo.Text = "Proyecto Segundo Parcial - Figuras en 3D";

            // FUENTE
            lblTitulo.Font = new Font("Stork Delivery", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.AutoSize = true;
           

            // PANEL DE CONTRASTE
           // panelTitulo.BackColor = Color.FromArgb(224, 216, 176, 0); // caqui semitransparente

            // Ajustar panel al texto
            panelTitulo.Width = lblTitulo.Width + 40;
            panelTitulo.Height = lblTitulo.Height + 30;

            // Centrar panel
            panelTitulo.Left = (ClientSize.Width - panelTitulo.Width) / 2;
            panelTitulo.Top = (ClientSize.Height - panelTitulo.Height) / 2;

            // Centrar label dentro del panel
            lblTitulo.Left = (panelTitulo.Width - lblTitulo.Width) / 2;
            lblTitulo.Top = (panelTitulo.Height - lblTitulo.Height) / 2;

            // Centrar botón Play debajo del título
            btnPlay.Left = (ClientSize.Width - btnPlay.Width) / 2;
            btnPlay.Top = panelTitulo.Bottom + 20;

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            var mainForm = new ShapeExplorerForm();

            // Cuando se cierre el main, cerrar toda la app
            mainForm.FormClosed += (s, ev) => Application.Exit();

            mainForm.Show();
            this.Hide();
        }
    }
}
