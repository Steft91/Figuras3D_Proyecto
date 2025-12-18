using Figuras3D_Proyecto.Forms;
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
using System.Diagnostics;

namespace Figuras3D_Proyecto
{
    public partial class ShapeExplorerForm : Form
    {
        private SceneManager sceneManager = new SceneManager();
        private ShapeRenderer renderer = new ShapeRenderer();
        private MaterialType currentMaterial = MaterialType.Solid;
        private KeyboardController inputController;
        private string lastSelected = "";
        private Color currentPaintColor = Color.Yellow;
        private ContextMenuStrip colorMenu;
        private MouseClickHandler mouseClickHandler;

        // -------------------- NUEVO: control cámara (orbital) --------------------
        private bool _camDragging = false;
        private bool _camPanMode = false; // botón derecho
        private Point _camLastMouse;

        // Para evitar que la cámara se mueva cuando estás editando vértices/caras/material
        private bool IsEditMode =>
            gunarbtnVertexes.Checked || gunarbtnChangeMaterial.Checked || gunarbtnChangeLighting.Checked || gunarbtnPaintFigures.Checked;
        // ------------------------------------------------------------------------
        private Timer _freeCamTimer;
        private bool _kI, _kJ, _kK, _kL, _kU, _kO, _kShift;

        private const float FreeCamBaseSpeed = 18f;     // ajusta a gusto
        private const float FreeCamFastMult = 3.0f;     // Shift
        public ShapeExplorerForm()
        {
            InitializeComponent();
            HideAllSelectors();

            // Teclas: que el Form capture keys incluso si el foco está en otros controles
            //

            gunacmbFigures.KeyDown += ComboBox_BlockArrows;
            gunacmbMode.KeyDown += ComboBox_BlockArrows;

            gunacmbFigures.TabStop = false;
            gunacmbMode.TabStop = false;

            CreateMaterialButton();


            gunacmbFigures.SelectedIndexChanged += GunacmbFigures_SelectedIndexChanged;
            gunacmbMode.SelectedIndexChanged += GunacmbMode_SelectedIndexChanged;

            SetupComboBoxFigures();
            SetupComboBoxMode();

            sceneManager.Camera.SetOrbitalDefault();

            inputController = new KeyboardController(this, picCanvas, sceneManager);

            picCanvas.Paint += PanelCanvas_Paint;
            mouseClickHandler = new MouseClickHandler(sceneManager, picCanvas,
                            gunarbtnVertexes, gunarbtnChangeLighting, gunarbtnChangeMaterial, gunarbtnPaintFigures, currentPaintColor);

            // Click edición (selección de vértices/caras/material/pintura)
            picCanvas.MouseClick += picCanvas_MouseClick;

            // ----------------- NUEVO: eventos cámara -----------------
            picCanvas.MouseDown += picCanvas_MouseDown;
            picCanvas.MouseUp += picCanvas_MouseUp;
            picCanvas.MouseMove += picCanvas_MouseMove;
            picCanvas.MouseWheel += picCanvas_MouseWheel;

            // Importante para wheel
            picCanvas.TabStop = true;
            // ---------------------------------------------------------
            this.KeyUp += ShapeExplorerForm_KeyUp;

            // Timer para movimiento continuo en cámara libre
            _freeCamTimer = new Timer();
            _freeCamTimer.Interval = 16; // ~60 FPS
            _freeCamTimer.Tick += timer1_Tick;
            _freeCamTimer.Start();

            this.Load += ShapeExplorerForm_Load;

            // Suscribir el clic del botón de iluminación (lógica añadida)
            this.gunbtnSelectLight.Click += gunbtnSelectLight_Click;
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

        private void CreateMaterialButton()
        {
            gunbtnSelectMaterial = new Guna.UI2.WinForms.Guna2Button();
            gunbtnSelectMaterial.Size = new Size(36, 36);
            gunbtnSelectMaterial.Text = "M"; // o "" si usas ícono
            gunbtnSelectMaterial.Visible = false;

            // Ubícalo cerca del botón de color (ajusta coordenadas)
            gunbtnSelectMaterial.Location = new Point(gunbtnSelectColor.Right + 8, gunbtnSelectColor.Top);

            gunbtnSelectMaterial.Click += gunbtnSelectMaterial_Click;
            Controls.Add(gunbtnSelectMaterial);
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

            // Actualizar texto inicial del botón de luz para reflejar estado actual
            UpdateLightButtonText();
        }

        private void UpdateLightButtonText()
        {
            gunbtnSelectLight.Text = sceneManager.LightingEnabled ? $"💡 Iluminación (On) L:{sceneManager.LightFactor:0.00} S:{sceneManager.ShadowFactor:0.00}" 
                                                                   : $"💡 Iluminación (Off) L:{sceneManager.LightFactor:0.00} S:{sceneManager.ShadowFactor:0.00}";
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
            if (!gunarbtnPaintFigures.Checked) return;

            HideAllSelectors();
            gunbtnSelectColor.Visible = true;

        }

        private void gunarbtnChangeLight_CheckedChanged(object sender, EventArgs e)
        {
            if (!gunarbtnChangeLighting.Checked) return;

            HideAllSelectors();
            gunbtnSelectLight.Visible = true;
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

            HideAllSelectors();
            gunbtnSelectMaterial.Visible = true;

            // IMPORTANTE: si ya estás usando picker, el “ciclado” acá puede molestarte.
            // Si quieres, coméntalo. Lo dejo tal cual tu versión actual.
            var selected = sceneManager.Shapes.FirstOrDefault(s => s.IsSelected);
            if (selected == null) return;

            if (selected.Material == MaterialType.Rough)
                selected.Material = MaterialType.Striped;
            else if (selected.Material == MaterialType.Striped)
                selected.Material = MaterialType.Smooth;
            else
                selected.Material = MaterialType.Solid;

            picCanvas.Invalidate();
        }

        private void gunbtnSelectMaterial_Click(object sender, EventArgs e)
        {
            var matForm = new MaterialPickerForm(currentMaterial);
            var buttonLocation = gunbtnSelectMaterial.PointToScreen(Point.Empty);
            matForm.Location = new Point(buttonLocation.X, buttonLocation.Y + gunbtnSelectMaterial.Height);

            if (matForm.ShowDialog() == DialogResult.OK)
            {
                currentMaterial = matForm.SelectedMaterial;

                // opcional: actualizar texto del botón
                gunbtnSelectMaterial.Text = currentMaterial.ToString().Substring(0, 1);

                mouseClickHandler.UpdateMaterial(currentMaterial);
                picCanvas.Invalidate();
            }
        }

        // NUEVO: manejador del botón de seleccionar iluminación
        private void gunbtnSelectLight_Click(object sender, EventArgs e)
        {
            var lp = new Forms.LightPickerForm(sceneManager.LightingEnabled, sceneManager.ShadowFactor, sceneManager.LightFactor);
            var buttonLocation = gunbtnSelectLight.PointToScreen(Point.Empty);
            lp.StartPosition = FormStartPosition.Manual;
            lp.Location = new Point(buttonLocation.X, buttonLocation.Y + gunbtnSelectLight.Height);

            if (lp.ShowDialog() == DialogResult.OK)
            {
                // Aplicar cambios al SceneManager
                sceneManager.LightingEnabled = lp.LightingEnabled;
                sceneManager.ShadowFactor = lp.ShadowFactor;
                sceneManager.LightFactor = lp.LightFactor;

                UpdateLightButtonText();

                picCanvas.Invalidate();
            }
        }

        private void HideAllSelectors()
        {
            gunbtnSelectColor.Visible = false;
            gunbtnSelectMaterial.Visible = false;
            gunbtnSelectLight.Visible = false;
        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            // Si estás editando, no arrancar cámara
            if (IsEditMode) return;

            // Solo si existe la cámara (si no, ignora)
            if (sceneManager.Camera == null) return;

            _camDragging = true;
            _camLastMouse = e.Location;
            _camPanMode = (e.Button == MouseButtons.Right);

            picCanvas.Focus();
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            _camDragging = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sceneManager.Camera == null) return;
            if (IsEditMode) return;

            var cam = sceneManager.Camera;
            if (cam.Mode != CameraMode.Free) return;

            // Si no hay ninguna tecla apretada, no hacer nada
            if (!(_kI || _kK || _kJ || _kL || _kU || _kO)) return;

            float speed = FreeCamBaseSpeed * (_kShift ? FreeCamFastMult : 1f);

            // Vectores de movimiento en base a yaw/pitch
            // forward (dirección a la que mira)
            float cy = (float)Math.Cos(cam.Yaw);
            float sy = (float)Math.Sin(cam.Yaw);
            float cp = (float)Math.Cos(cam.Pitch);
            float sp = (float)Math.Sin(cam.Pitch);

            // Forward en tu sistema (coherente con Projection3D)
            var forward = new Point3D(cp * sy, sp, cp * cy);

            // Right = forward x up (up = 0,1,0)
            var right = new Point3D(forward.Z, 0, -forward.X);

            // Normalizar right (por si acaso)
            float rlen = (float)Math.Sqrt(right.X * right.X + right.Z * right.Z);
            if (rlen > 1e-6f)
            {
                right = new Point3D(right.X / rlen, 0, right.Z / rlen);
            }

            float dx = 0, dy = 0, dz = 0;

            if (_kI) { dx += forward.X * speed; dy += forward.Y * speed; dz += forward.Z * speed; }
            if (_kK) { dx -= forward.X * speed; dy -= forward.Y * speed; dz -= forward.Z * speed; }

            if (_kL) { dx += right.X * speed; dz += right.Z * speed; }
            if (_kJ) { dx -= right.X * speed; dz -= right.Z * speed; }

            if (_kU) { dy += speed; }
            if (_kO) { dy -= speed; }

            // Mover cámara: Position y Target juntos (cámara "libre" desplazándose)
            cam.Position = new Point3D(cam.Position.X + dx, cam.Position.Y + dy, cam.Position.Z + dz);
            cam.Target = new Point3D(cam.Target.X + dx, cam.Target.Y + dy, cam.Target.Z + dz);

            picCanvas.Invalidate();
        }

        private void ShapeExplorerForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) _kShift = false;

            if (e.KeyCode == Keys.I) _kI = false;
            if (e.KeyCode == Keys.K) _kK = false;
            if (e.KeyCode == Keys.J) _kJ = false;
            if (e.KeyCode == Keys.L) _kL = false;
            if (e.KeyCode == Keys.U) _kU = false;
            if (e.KeyCode == Keys.O) _kO = false;
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_camDragging) return;
            if (sceneManager.Camera == null) return;

            // Solo aplicamos cámara en modo Orbital (por ahora)
            if (sceneManager.Camera.Mode != CameraMode.Orbital) return;

            int dx = e.X - _camLastMouse.X;
            int dy = e.Y - _camLastMouse.Y;
            _camLastMouse = e.Location;

            if (_camPanMode)
                sceneManager.Camera.Pan(dx, dy);
            else
                sceneManager.Camera.Orbit(dx, dy);

            picCanvas.Invalidate();
        }

        private void picCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sceneManager.Camera == null) return;
            if (IsEditMode) return;

            sceneManager.Camera.Zoom(e.Delta);
            picCanvas.Invalidate();
        }

        private void gunabtnCamaraOrbital_Click(object sender, EventArgs e)
        {
            sceneManager.Camera.SetOrbitalDefault();
            picCanvas.Invalidate();
        }

        private void gunabtnCamaraFrontal_Click(object sender, EventArgs e)
        {
            sceneManager.Camera.SetFixedFront();
            picCanvas.Invalidate();
        }

        private void gunabtnCamaraLibre_Click(object sender, EventArgs e)
        {
            sceneManager.Camera.SetFreeDefault();
            picCanvas.Invalidate();
        }

        private void ShapeExplorerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (sceneManager.Camera == null) return;

            // En edición no cambiamos cámara con teclas para no interferir
            if (IsEditMode) return;

            // 1 = fija, 2 = orbital, 3 = libre
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                sceneManager.Camera.SetFixedFront();
                picCanvas.Invalidate();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                sceneManager.Camera.SetOrbitalDefault();
                picCanvas.Invalidate();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                sceneManager.Camera.SetFreeDefault();
                picCanvas.Invalidate();
                e.Handled = true;
            }
            if (sceneManager.Camera == null) return;

            // En edición no movemos cámara
            if (IsEditMode) return;

            // 1 = fija, 2 = orbital, 3 = libre
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                sceneManager.Camera.SetFixedFront();
                picCanvas.Invalidate();
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                sceneManager.Camera.SetOrbitalDefault();
                picCanvas.Invalidate();
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                sceneManager.Camera.SetFreeDefault();
                picCanvas.Invalidate();
                e.Handled = true;
                return;
            }

            // -------- Free camera keys --------
            if (e.KeyCode == Keys.ShiftKey) _kShift = true;

            // Solo guardar estado si estás en Free
            if (sceneManager.Camera.Mode != CameraMode.Free) return;

            if (e.KeyCode == Keys.I) _kI = true;
            if (e.KeyCode == Keys.K) _kK = true;
            if (e.KeyCode == Keys.J) _kJ = true;
            if (e.KeyCode == Keys.L) _kL = true;
            if (e.KeyCode == Keys.U) _kU = true;
            if (e.KeyCode == Keys.O) _kO = true;

        }
    }
}
