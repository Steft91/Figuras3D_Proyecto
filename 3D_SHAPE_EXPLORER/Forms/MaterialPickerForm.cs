using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static Figuras3D_Proyecto.Models.Shape3D;

namespace Figuras3D_Proyecto.Forms
{
    public partial class MaterialPickerForm : Form
    {
        public MaterialType SelectedMaterial { get; private set; } = MaterialType.Solid;
        private readonly Action<MaterialType> onMaterialSelected;

        public MaterialPickerForm(MaterialType current, Action<MaterialType> onMaterialSelected = null)
        {
            SelectedMaterial = current;
            this.onMaterialSelected = onMaterialSelected;

            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Size = new Size(460, 190);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.LightCyan;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Usamos TableLayoutPanel para mantener la misma estructura pero con "cards"
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Padding = new Padding(12),
                BackColor = Color.LightCyan
            };

            panel.ColumnStyles.Clear();
            for (int i = 0; i < 4; i++)
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));

            AddMaterialCard(panel, "Solid", MaterialType.Solid);
            AddMaterialCard(panel, "Rough", MaterialType.Rough);
            AddMaterialCard(panel, "Striped", MaterialType.Striped);
            AddMaterialCard(panel, "Smooth", MaterialType.Smooth);
            // Si quieres VeryShiny, añade otra columna en panel y llama AddMaterialCard con VeryShiny.

            Controls.Add(panel);
        }

        private void AddMaterialCard(TableLayoutPanel parent, string text, MaterialType material)
        {
            // Card container
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(6),
                BackColor = Color.Transparent
            };

            // Preview panel donde dibujamos la "superficie"
            var preview = new Panel
            {
                Size = new Size(96, 56),
                Location = new Point(6, 6),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Pintado del swatch según material
            preview.Paint += (s, e) => DrawMaterialPreview(e.Graphics, preview.ClientRectangle, material);

            // Selección por click en el preview
            preview.Cursor = Cursors.Hand;
            preview.Click += (_, __) => SelectMaterial(material);

            // Botón de selección estilizado
            var btn = new Guna2Button
            {
                Text = text,
                Width = 110,
                Height = 30,
                Location = new Point(6, preview.Bottom + 8),
                FillColor = Color.PaleGoldenrod,
                BorderRadius = 12,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            btn.Click += (_, __) => SelectMaterial(material);

            // Texto de ayuda pequeño
            var lbl = new Label
            {
                Text = "",
                AutoSize = false,
                Size = new Size(110, 0),
                Location = new Point(6, btn.Bottom + 2),
                BackColor = Color.Transparent
            };

            // Añadir controles al card
            card.Controls.Add(preview);
            card.Controls.Add(btn);
            card.Controls.Add(lbl);

            // Añadir card al TableLayoutPanel en la siguiente celda libre
            parent.Controls.Add(card);
        }

        private void SelectMaterial(MaterialType material)
        {
            SelectedMaterial = material;
            try { onMaterialSelected?.Invoke(material); } catch { }
            DialogResult = DialogResult.OK;
            Close();
        }

        // Dibuja un swatch representativo para cada material
        private void DrawMaterialPreview(Graphics g, Rectangle r, MaterialType material)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            // base color sample (gris neutro)
            Color baseColor = Color.FromArgb(200, 200, 200);

            switch (material)
            {
                case MaterialType.Rough:
                    {
                        // Relleno base
                        using (var brush = new SolidBrush(MultiplyColor(baseColor, 0.9f)))
                            g.FillRectangle(brush, r);

                        // Hachurado fuerte
                        using (var hb = new HatchBrush(HatchStyle.DottedGrid, MultiplyColor(Color.DarkGray, 0.95f), Color.Transparent))
                        {
                            g.FillRectangle(hb, r);
                        }

                        // Pequeñas irregularidades: puntos
                        using (var dot = new SolidBrush(MultiplyColor(Color.DarkGray, 0.9f)))
                        {
                            var rand = new Random(material.GetHashCode());
                            for (int i = 0; i < 14; i++)
                            {
                                int x = r.Left + rand.Next(2, r.Width - 4);
                                int y = r.Top + rand.Next(2, r.Height - 4);
                                g.FillEllipse(dot, x, y, 2, 2);
                            }
                        }
                    }
                    break;

                case MaterialType.Striped:
                    {
                        using (var brush = new SolidBrush(MultiplyColor(baseColor, 0.95f)))
                            g.FillRectangle(brush, r);

                        using (var hb = new HatchBrush(HatchStyle.DiagonalCross, MultiplyColor(Color.Black, 0.65f), Color.Transparent))
                        {
                            g.FillRectangle(hb, r);
                        }
                    }
                    break;

                case MaterialType.Smooth:
                    {
                        // Gradiente suave con brillo
                        var g2 = new LinearGradientBrush(r, MultiplyColor(baseColor, 1.15f), MultiplyColor(baseColor, 0.8f), 315f);
                        g.FillRectangle(g2, r);

                        // pequeño highlight
                        RectangleF h = new RectangleF(r.Left + r.Width * 0.55f - 8, r.Top + r.Height * 0.12f, r.Width * 0.35f, r.Height * 0.28f);
                        using (var hb = new LinearGradientBrush(h, Color.FromArgb(180, Color.White), Color.FromArgb(0, Color.White), LinearGradientMode.ForwardDiagonal))
                        {
                            g.FillEllipse(hb, h);
                        }
                    }
                    break;

                case MaterialType.VeryShiny:
                    {
                        // Gradiente marcado
                        var gbg = new LinearGradientBrush(r, MultiplyColor(baseColor, 1.35f), MultiplyColor(baseColor, 0.6f), 330f);
                        g.FillRectangle(gbg, r);

                        // Especular fuerte
                        RectangleF spec = new RectangleF(r.Left + r.Width * 0.45f, r.Top + r.Height * 0.05f, r.Width * 0.5f, r.Height * 0.5f);
                        using (var sb = new SolidBrush(Color.FromArgb(210, Color.White)))
                        {
                            g.FillEllipse(sb, spec);
                        }

                        // Sutileza de reflejo invertido
                        RectangleF spec2 = new RectangleF(r.Left + r.Width * 0.1f, r.Top + r.Height * 0.55f, r.Width * 0.25f, r.Height * 0.25f);
                        using (var sb2 = new SolidBrush(Color.FromArgb(40, Color.White)))
                        {
                            g.FillEllipse(sb2, spec2);
                        }
                    }
                    break;

                default:
                    {
                        // Solid
                        using (var brush = new SolidBrush(MultiplyColor(baseColor, 1.0f)))
                            g.FillRectangle(brush, r);

                        // leve gradiente
                        var lg = new LinearGradientBrush(r, Color.FromArgb(230, Color.White), Color.FromArgb(30, Color.Black), 315f);
                        g.FillRectangle(lg, r);
                    }
                    break;
            }

            // marco
            using (var pen = new Pen(Color.FromArgb(120, Color.Black)))
                g.DrawRectangle(pen, r.Left, r.Top, r.Width - 1, r.Height - 1);
        }

        // Helper: multiplicar color por factor (clamped)
        private static Color MultiplyColor(Color c, float k)
        {
            k = Math.Max(0f, Math.Min(2f, k));
            int r = (int)Math.Round(c.R * k);
            int g = (int)Math.Round(c.G * k);
            int b = (int)Math.Round(c.B * k);
            r = Math.Max(0, Math.Min(255, r));
            g = Math.Max(0, Math.Min(255, g));
            b = Math.Max(0, Math.Min(255, b));
            return Color.FromArgb(c.A, r, g, b);
        }

        private void MaterialPickerForm_Load(object sender, EventArgs e)
        {
        }
    }
}
