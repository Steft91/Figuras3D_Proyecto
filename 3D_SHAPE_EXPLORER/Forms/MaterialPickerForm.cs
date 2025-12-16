using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Figuras3D_Proyecto.Models.Shape3D;

namespace Figuras3D_Proyecto.Forms
{
    public partial class MaterialPickerForm : Form
    {
        public MaterialType SelectedMaterial { get; private set; } = MaterialType.Solid;
        public MaterialPickerForm(MaterialType current)
        {
            SelectedMaterial = current;

            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Size = new Size(400, 150);
            StartPosition = FormStartPosition.Manual;

            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Padding = new Padding(20),
            };

            panel.ColumnStyles.Clear();
            for (int i = 0; i < 4; i++)
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            AddButton(panel, "Solid", MaterialType.Solid);
            AddButton(panel, "Rough", MaterialType.Rough);
            AddButton(panel, "Striped", MaterialType.Striped);
            AddButton(panel, "Smooth", MaterialType.Smooth);

            Controls.Add(panel);
        }

        private void AddButton(TableLayoutPanel panel, string text, MaterialType material)
        {
            var btn = new Guna2Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(4),
                BorderRadius = 20
            };

            btn.Click += (_, __) =>
            {
                SelectedMaterial = material;
                DialogResult = DialogResult.OK;
                Close();
            };

            panel.Controls.Add(btn);
        }



        private void MaterialPickerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
