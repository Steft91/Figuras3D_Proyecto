using System;
using System.Drawing;
using System.Windows.Forms;

namespace Figuras3D_Proyecto.Forms
{
    public class LightPickerForm : Form
    {
        // Resultado público
        public bool LightingEnabled { get; private set; }
        public float ShadowFactor { get; private set; }
        public float LightFactor { get; private set; }

        // UI
        private TabControl tabControl;
        private CheckBox chkEnabled;
        private TrackBar trkShadow;
        private TrackBar trkLight;
        private NumericUpDown nudShadow;
        private NumericUpDown nudLight;
        private Button btnOk;
        private Button btnCancel;
        private FlowLayoutPanel presetsPanel;

        // Callback opcional para previsualizar cambios en tiempo real
        private readonly Action<bool, float, float> onChange;

        // Rangos mapeados para TrackBar (enteros)
        private const int ShadowMinInt = 10;   // -> 0.10
        private const int ShadowMaxInt = 150;  // -> 1.50
        private const int LightMinInt = 100;   // -> 1.00
        private const int LightMaxInt = 250;   // -> 2.50

        public LightPickerForm(bool currentEnabled, float currentShadow, float currentLight, Action<bool, float, float> onChange = null)
        {
            this.onChange = onChange;
            InitializeComponent();

            // Inicializar valores
            chkEnabled.Checked = currentEnabled;
            SetShadow(currentShadow);
            SetLight(currentLight);
        }

        private void InitializeComponent()
        {
            // Estética coherente con ShapeExplorerForm
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Size = new Size(380, 260);
            // Aseguramos que por defecto se centre respecto al padre
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Seleccionar Iluminación";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.LightCyan;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            tabControl = new TabControl { Dock = DockStyle.Top, Height = 170, BackColor = Color.Transparent, Font = new Font("Segoe UI", 9F) };

            // Pestaña Presets (ahora primera)
            var tabPresets = new TabPage("Presets");
            tabPresets.BackColor = Color.LightCyan;
            presetsPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), AutoScroll = true, BackColor = Color.LightCyan };
            AddPresetButton("Apagado (sólo sombras)", false, 0.40f, 1.00f);
            AddPresetButton("Dull (suave)", true, 0.60f, 1.10f);
            AddPresetButton("Default", true, 0.55f, 1.25f);
            AddPresetButton("Brillante", true, 0.45f, 1.45f);
            AddPresetButton("Muy brillante", true, 0.35f, 1.70f);
            tabPresets.Controls.Add(presetsPanel);

            // Pestaña Valores (sliders + numeric)
            var tabValues = new TabPage("Valores");
            tabValues.BackColor = Color.LightCyan;

            var lblShadow = new Label { Text = "Factor Sombra (más bajo = más sombra)", Location = new Point(12, 8), AutoSize = true, ForeColor = Color.Black };
            trkShadow = new TrackBar { Minimum = ShadowMinInt, Maximum = ShadowMaxInt, TickFrequency = 10, SmallChange = 1, LargeChange = 5, Location = new Point(12, 28), Width = 320, BackColor = Color.LightCyan };
            nudShadow = new NumericUpDown { Minimum = 0.10M, Maximum = 1.50M, DecimalPlaces = 2, Increment = 0.01M, Location = new Point(240, 4), Width = 80 };
            trkShadow.Scroll += (s, e) => SyncShadowFromTrack();
            nudShadow.ValueChanged += (s, e) => SyncShadowFromNumeric();
            tabValues.Controls.Add(lblShadow);
            tabValues.Controls.Add(trkShadow);
            tabValues.Controls.Add(nudShadow);

            var lblLight = new Label { Text = "Factor Luz (más alto = más brillante)", Location = new Point(12, 84), AutoSize = true, ForeColor = Color.Black };
            trkLight = new TrackBar { Minimum = LightMinInt, Maximum = LightMaxInt, TickFrequency = 10, SmallChange = 1, LargeChange = 5, Location = new Point(12, 104), Width = 320, BackColor = Color.LightCyan };
            nudLight = new NumericUpDown { Minimum = 1.00M, Maximum = 2.50M, DecimalPlaces = 2, Increment = 0.01M, Location = new Point(240, 80), Width = 80 };
            trkLight.Scroll += (s, e) => SyncLightFromTrack();
            nudLight.ValueChanged += (s, e) => SyncLightFromNumeric();
            tabValues.Controls.Add(lblLight);
            tabValues.Controls.Add(trkLight);
            tabValues.Controls.Add(nudLight);

            // Pestaña Opciones (solo checkbox en esta versión, sin la info previa)
            var tabOptions = new TabPage("Opciones");
            tabOptions.BackColor = Color.LightCyan;
            chkEnabled = new CheckBox
            {
                Text = "Habilitar iluminación",
                AutoSize = true,
                Location = new Point(12, 12),
                ForeColor = Color.Black
            };
            chkEnabled.CheckedChanged += (s, e) => RaiseOnChange();
            tabOptions.Controls.Add(chkEnabled);

            tabControl.TabPages.Add(tabPresets);
            tabControl.TabPages.Add(tabValues);
            tabControl.TabPages.Add(tabOptions);

            // Botones OK / Cancel con estilo similar a ShapeExplorerForm
            btnOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Width = 110,
                Location = new Point(70, 185),
                BackColor = Color.PaleGoldenrod,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += BtnOk_Click;

            btnCancel = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Width = 110,
                Location = new Point(200, 185),
                BackColor = Color.PaleGoldenrod,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            this.Controls.Add(tabControl);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void AddPresetButton(string text, bool enabled, float shadow, float light)
        {
            var btn = new Button
            {
                Text = text,
                AutoSize = false,
                Width = 320,
                Height = 34,
                Margin = new Padding(4),
                BackColor = Color.PaleGoldenrod,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) =>
            {
                chkEnabled.Checked = enabled;
                SetShadow(shadow);
                SetLight(light);
                RaiseOnChange();
            };
            presetsPanel.Controls.Add(btn);
        }

        private void SetShadow(float value)
        {
            value = Math.Max(0.10f, Math.Min(1.50f, value));
            nudShadow.Value = (decimal)value;
            trkShadow.Value = FloatToIntShadow(value);
        }

        private void SetLight(float value)
        {
            value = Math.Max(1.00f, Math.Min(2.50f, value));
            nudLight.Value = (decimal)value;
            trkLight.Value = FloatToIntLight(value);
        }

        private void SyncShadowFromTrack()
        {
            var v = IntToFloatShadow(trkShadow.Value);
            if (nudShadow.Value != (decimal)v) nudShadow.Value = (decimal)v;
            RaiseOnChange();
        }

        private void SyncShadowFromNumeric()
        {
            var v = (float)nudShadow.Value;
            int iv = FloatToIntShadow(v);
            if (trkShadow.Value != iv) trkShadow.Value = iv;
            RaiseOnChange();
        }

        private void SyncLightFromTrack()
        {
            var v = IntToFloatLight(trkLight.Value);
            if (nudLight.Value != (decimal)v) nudLight.Value = (decimal)v;
            RaiseOnChange();
        }

        private void SyncLightFromNumeric()
        {
            var v = (float)nudLight.Value;
            int iv = FloatToIntLight(v);
            if (trkLight.Value != iv) trkLight.Value = iv;
            RaiseOnChange();
        }

        private static int FloatToIntShadow(float v) => (int)Math.Round(v * 100f);
        private static float IntToFloatShadow(int i) => i / 100f;
        private static int FloatToIntLight(float v) => (int)Math.Round(v * 100f);
        private static float IntToFloatLight(int i) => i / 100f;

        private void RaiseOnChange()
        {
            // Notificar preview si el host lo solicitó
            onChange?.Invoke(chkEnabled.Checked, (float)nudShadow.Value, (float)nudLight.Value);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            // Guardar resultados
            LightingEnabled = chkEnabled.Checked;
            ShadowFactor = (float)nudShadow.Value;
            LightFactor = (float)nudLight.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}