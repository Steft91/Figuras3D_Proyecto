using Figuras3D_Proyecto.Models;
using Figuras3D_Proyecto.Services;
using Figuras3D_Proyecto.Utils;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Figuras3D_Proyecto.Models.Shape3D;

namespace Figuras3D_Proyecto
{
    public partial class ShapeExplorerForm : Form
    {
        private SceneManager sceneManager = new SceneManager();
        private ShapeRenderer renderer = new ShapeRenderer();
        private KeyboardController inputController;
        private string lastSelected = "";
        private Color currentPaintColor = Color.Yellow;
        private ContextMenuStrip colorMenu;
        private MouseClickHandler mouseClickHandler;

        public ShapeExplorerForm()
        {
            InitializeComponent();

            gunacmbFigures.KeyDown += ComboBox_BlockArrows;
            gunacmbMode.KeyDown += ComboBox_BlockArrows;

            gunacmbFigures.TabStop = false;
            gunacmbMode.TabStop = false;



            gunacmbFigures.SelectedIndexChanged += GunacmbFigures_SelectedIndexChanged;
            gunacmbMode.SelectedIndexChanged += GunacmbMode_SelectedIndexChanged;

            SetupComboBoxFigures();
            SetupComboBoxMode();

            sceneManager.Initialize();
            inputController = new KeyboardController(this, picCanvas, sceneManager);

            picCanvas.Paint += PanelCanvas_Paint;
            mouseClickHandler = new MouseClickHandler(sceneManager, picCanvas,
                            gunarbtnVertexes, gunarbtnChangeLighting, gunarbtnChangeMaterial, gunarbtnPaintFigures, currentPaintColor);
            picCanvas.MouseClick += picCanvas_MouseClick;
            this.Load += ShapeExplorerForm_Load;

        }

        private void ComboBox_BlockArrows(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Right)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }




        private void PanelCanvas_Paint(object sender, PaintEventArgs e)
        {
            bool isInEditMode = gunarbtnVertexes.Checked || gunarbtnChangeMaterial.Checked || gunarbtnChangeLighting.Checked;
            renderer.Draw(e.Graphics, sceneManager.Shapes, picCanvas.Size, sceneManager, isInEditMode, currentPaintColor);

        }

        private void ShapeExplorerForm_Load(object sender, EventArgs e)
        {
            gunarbtnVertexes.Visible = false;
            gunarbtnChangeMaterial.Visible = false;
            gunarbtnChangeLighting.Visible = false;
            gunarbtnPaintFigures.Visible = false;
 
            gunacmbMode.SelectedIndex = 0;
            gunbtnSelectColor.Visible = false;

            string imagePath = Path.Combine(Application.StartupPath, "Resources", "paint_bucket.png");
            if (File.Exists(imagePath))
            {
                gunbtnSelectColor.Image = Image.FromFile(imagePath);
                gunbtnSelectColor.ImageSize = new Size(24, 24); // Tamaño del ícono
                gunbtnSelectColor.Text = "";                    // Quitar texto
                gunbtnSelectColor.TextAlign = HorizontalAlignment.Center;
                gunbtnSelectColor.ImageAlign = HorizontalAlignment.Center;
                gunbtnSelectColor.FillColor = Color.Transparent; // Opcional: sin fondo
                gunbtnSelectColor.BorderThickness = 0;           // Opcional: sin bordes
            }

        }




        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            
            mouseClickHandler.HandleMouseClick(e.Location);
        }

        

        private void SetupComboBoxMode()
        {
            gunacmbMode.Items.Clear();
            gunacmbMode.Items.Add(new ComboBoxItem("👀 Modo de Movimiento", "object"));
            gunacmbMode.Items.Add(new ComboBoxItem("✏️ Modo de Edición", "edit"));
            gunacmbMode.SelectedIndex = 0;
            gunacmbMode.Font = new Font("Segoe UI Emoji", 11);
        }

        private void SetupComboBoxFigures()
        {
            gunacmbFigures.Items.Clear();
            gunacmbFigures.Items.Add(new ComboBoxItem("🧩 Seleccione una figura...", ""));
            gunacmbFigures.Items.Add(new ComboBoxItem("🧊 Cubo", "Cube"));
            gunacmbFigures.Items.Add(new ComboBoxItem("🧱 Cilindro", "Cylinder"));
            gunacmbFigures.Items.Add(new ComboBoxItem("⚪ Esfera", "Sphere"));
            gunacmbFigures.Items.Add(new ComboBoxItem("🔺 Pirámide", "Pyramid"));
            gunacmbFigures.SelectedIndex = 0;
            gunacmbFigures.Font = new Font("Segoe UI Emoji", 11);
        }

        private void GunacmbFigures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gunacmbFigures.SelectedItem is ComboBoxItem selectedItem)
            {
                string value = selectedItem.Value;

                if (string.IsNullOrEmpty(value))
                    return;

                //if (value == lastSelected) return;

                lastSelected = value;
                var shape = ShapeFactory.Create(value);
                if (shape != null)
                {
                    //ShapeFactory.CentrarYGuardarOriginales(shape);
                    sceneManager.AddShape(shape);
                    picCanvas.Invalidate();
                }
            }

            picCanvas.Focus();
        }

        private void GunacmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gunacmbMode.SelectedItem is ComboBoxItem selectedItem)
            {
                string modeValue = selectedItem.Value;

                if (modeValue == "edit")
                {

                    gunarbtnVertexes.Visible = true;
                    gunarbtnChangeLighting.Visible = true;
                    gunarbtnChangeMaterial.Visible = true;
                    gunarbtnPaintFigures.Visible = true;

                    gunbtnSelectColor.Visible = gunarbtnPaintFigures.Checked;
                }
                else
                {

                    gunarbtnVertexes.Visible = false;
                    gunarbtnChangeMaterial.Visible = false;
                    gunarbtnPaintFigures.Visible = false;
                    gunarbtnChangeLighting.Visible = false;

                    gunarbtnVertexes.Checked = false;
                    gunarbtnChangeMaterial.Checked = false;
                    gunarbtnChangeLighting.Checked = false;
                    gunarbtnPaintFigures.Checked = false;
                    gunarbtnChangeLighting.Checked = false;
                    gunarbtnChangeMaterial.Checked = false;

                    sceneManager.SelectedVertexIndex = null;
                    sceneManager.SelectedEdge = null;
                    sceneManager.SelectedFace = null;
                    gunbtnSelectColor.Visible = false;
                    gunarbtnChangeMaterial.Visible = false;
                    gunarbtnChangeLighting.Visible = false;
                }
            }
        }

        private void gunarbtnPaintFigures_CheckedChanged(object sender, EventArgs e)
        {
            gunbtnSelectColor.Visible = gunarbtnPaintFigures.Checked;
        }

        private void gunbtnSelectColor_Click(object sender, EventArgs e)
        {
            var colores = new List<Color> { Color.Black, Color.White, Color.Orange, Color.Gold, Color.Green, Color.Teal,
                                     Color.MidnightBlue, Color.DarkGray, Color.HotPink, Color.MediumPurple };

            var colorForm = new ColorPickerForm(colores);
            var buttonLocation = gunbtnSelectColor.PointToScreen(Point.Empty);
            colorForm.Location = new Point(buttonLocation.X, buttonLocation.Y + gunbtnSelectColor.Height);

            if (colorForm.ShowDialog() == DialogResult.OK)
            {
                currentPaintColor = colorForm.SelectedColor;
                gunbtnSelectColor.BackColor = currentPaintColor;

                mouseClickHandler.UpdatePaintColor(currentPaintColor);

            }
        }

        private void gunarbtnChangeMaterial_CheckedChanged(object sender, EventArgs e)
        {
            if (!gunarbtnChangeMaterial.Checked)
                return;

            var selected = sceneManager.Shapes.FirstOrDefault(s => s.IsSelected);
            if (selected == null) return;

            // Cicla materiales
            if (selected.Material == MaterialType.Solid)
            {
                selected.Material = MaterialType.Rough;
            }
            else if (selected.Material == MaterialType.Rough)
            {
                selected.Material = MaterialType.Striped;
            }
            else if (selected.Material == MaterialType.Striped)
            {
                selected.Material = MaterialType.Smooth;
            }
            else
            {
                selected.Material = MaterialType.Solid;
            }


            picCanvas.Invalidate();
        }



    }
}
