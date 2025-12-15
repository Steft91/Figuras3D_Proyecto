namespace Figuras3D_Proyecto
{
    partial class ShapeExplorerForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShapeExplorerForm));
            this.gunacmbFigures = new Guna.UI2.WinForms.Guna2ComboBox();
            this.gunacmbMode = new Guna.UI2.WinForms.Guna2ComboBox();
            this.gunarbtnVertexes = new Guna.UI2.WinForms.Guna2RadioButton();
            this.gunarbtnPaintFigures = new Guna.UI2.WinForms.Guna2RadioButton();
            this.gunbtnSelectColor = new Guna.UI2.WinForms.Guna2Button();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.lblControls = new System.Windows.Forms.Label();
            this.gunarbtnChangeMaterial = new Guna.UI2.WinForms.Guna2RadioButton();
            this.gunarbtnChangeLighting = new Guna.UI2.WinForms.Guna2RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gunbtnSelectMaterial = new Guna.UI2.WinForms.Guna2Button();
            this.gunbtnSelectLight = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // gunacmbFigures
            // 
            this.gunacmbFigures.BackColor = System.Drawing.Color.DarkKhaki;
            this.gunacmbFigures.BorderColor = System.Drawing.Color.DarkKhaki;
            this.gunacmbFigures.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.gunacmbFigures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gunacmbFigures.FillColor = System.Drawing.Color.DarkKhaki;
            this.gunacmbFigures.FocusedColor = System.Drawing.Color.White;
            this.gunacmbFigures.FocusedState.BorderColor = System.Drawing.Color.White;
            this.gunacmbFigures.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gunacmbFigures.ForeColor = System.Drawing.Color.Black;
            this.gunacmbFigures.ItemHeight = 30;
            this.gunacmbFigures.ItemsAppearance.BackColor = System.Drawing.Color.Black;
            this.gunacmbFigures.ItemsAppearance.ForeColor = System.Drawing.Color.White;
            this.gunacmbFigures.ItemsAppearance.SelectedBackColor = System.Drawing.Color.DimGray;
            this.gunacmbFigures.Location = new System.Drawing.Point(17, 12);
            this.gunacmbFigures.Name = "gunacmbFigures";
            this.gunacmbFigures.Size = new System.Drawing.Size(372, 36);
            this.gunacmbFigures.TabIndex = 12;
            // 
            // gunacmbMode
            // 
            this.gunacmbMode.BackColor = System.Drawing.Color.DarkKhaki;
            this.gunacmbMode.BorderColor = System.Drawing.Color.DarkKhaki;
            this.gunacmbMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.gunacmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gunacmbMode.FillColor = System.Drawing.Color.DarkKhaki;
            this.gunacmbMode.FocusedColor = System.Drawing.Color.White;
            this.gunacmbMode.FocusedState.BorderColor = System.Drawing.Color.White;
            this.gunacmbMode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gunacmbMode.ForeColor = System.Drawing.Color.Black;
            this.gunacmbMode.ItemHeight = 30;
            this.gunacmbMode.ItemsAppearance.BackColor = System.Drawing.Color.Black;
            this.gunacmbMode.ItemsAppearance.ForeColor = System.Drawing.Color.White;
            this.gunacmbMode.ItemsAppearance.SelectedBackColor = System.Drawing.Color.DimGray;
            this.gunacmbMode.Location = new System.Drawing.Point(17, 54);
            this.gunacmbMode.Name = "gunacmbMode";
            this.gunacmbMode.Size = new System.Drawing.Size(372, 36);
            this.gunacmbMode.TabIndex = 13;
            // 
            // gunarbtnVertexes
            // 
            this.gunarbtnVertexes.AutoSize = true;
            this.gunarbtnVertexes.BackColor = System.Drawing.Color.DarkKhaki;
            this.gunarbtnVertexes.CheckedState.BorderColor = System.Drawing.Color.White;
            this.gunarbtnVertexes.CheckedState.BorderThickness = 0;
            this.gunarbtnVertexes.CheckedState.FillColor = System.Drawing.Color.DimGray;
            this.gunarbtnVertexes.CheckedState.InnerColor = System.Drawing.Color.White;
            this.gunarbtnVertexes.CheckedState.InnerOffset = -4;
            this.gunarbtnVertexes.Font = new System.Drawing.Font("Stork Delivery", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunarbtnVertexes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gunarbtnVertexes.Location = new System.Drawing.Point(17, 117);
            this.gunarbtnVertexes.Name = "gunarbtnVertexes";
            this.gunarbtnVertexes.Size = new System.Drawing.Size(130, 30);
            this.gunarbtnVertexes.TabIndex = 14;
            this.gunarbtnVertexes.Text = "🟢 Vertexes";
            this.gunarbtnVertexes.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gunarbtnVertexes.UncheckedState.BorderThickness = 2;
            this.gunarbtnVertexes.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.gunarbtnVertexes.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.gunarbtnVertexes.UseVisualStyleBackColor = false;
            // 
            // gunarbtnPaintFigures
            // 
            this.gunarbtnPaintFigures.AutoSize = true;
            this.gunarbtnPaintFigures.BackColor = System.Drawing.Color.DarkKhaki;
            this.gunarbtnPaintFigures.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gunarbtnPaintFigures.CheckedState.BorderThickness = 0;
            this.gunarbtnPaintFigures.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gunarbtnPaintFigures.CheckedState.InnerColor = System.Drawing.Color.White;
            this.gunarbtnPaintFigures.CheckedState.InnerOffset = -4;
            this.gunarbtnPaintFigures.Font = new System.Drawing.Font("Stork Delivery", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunarbtnPaintFigures.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gunarbtnPaintFigures.Location = new System.Drawing.Point(17, 162);
            this.gunarbtnPaintFigures.Name = "gunarbtnPaintFigures";
            this.gunarbtnPaintFigures.Size = new System.Drawing.Size(153, 30);
            this.gunarbtnPaintFigures.TabIndex = 17;
            this.gunarbtnPaintFigures.Text = "🎨Paint Figures";
            this.gunarbtnPaintFigures.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gunarbtnPaintFigures.UncheckedState.BorderThickness = 2;
            this.gunarbtnPaintFigures.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.gunarbtnPaintFigures.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.gunarbtnPaintFigures.UseVisualStyleBackColor = false;
            this.gunarbtnPaintFigures.CheckedChanged += new System.EventHandler(this.gunarbtnPaintFigures_CheckedChanged);
            // 
            // gunbtnSelectColor
            // 
            this.gunbtnSelectColor.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.gunbtnSelectColor.BorderColor = System.Drawing.Color.Transparent;
            this.gunbtnSelectColor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.gunbtnSelectColor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.gunbtnSelectColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.gunbtnSelectColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.gunbtnSelectColor.FillColor = System.Drawing.Color.Transparent;
            this.gunbtnSelectColor.FocusedColor = System.Drawing.Color.White;
            this.gunbtnSelectColor.Font = new System.Drawing.Font("Stork Delivery", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunbtnSelectColor.ForeColor = System.Drawing.Color.Black;
            this.gunbtnSelectColor.HoverState.BorderColor = System.Drawing.Color.White;
            this.gunbtnSelectColor.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.gunbtnSelectColor.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.gunbtnSelectColor.IndicateFocus = true;
            this.gunbtnSelectColor.Location = new System.Drawing.Point(30, 523);
            this.gunbtnSelectColor.Name = "gunbtnSelectColor";
            this.gunbtnSelectColor.Size = new System.Drawing.Size(298, 32);
            this.gunbtnSelectColor.TabIndex = 18;
            this.gunbtnSelectColor.Text = "🌈Seleccionar Color";
            this.gunbtnSelectColor.Click += new System.EventHandler(this.gunbtnSelectColor_Click);
            // 
            // picCanvas
            // 
            this.picCanvas.BackColor = System.Drawing.SystemColors.Info;
            this.picCanvas.Location = new System.Drawing.Point(413, 26);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(604, 605);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseClick);
            // 
            // lblControls
            // 
            this.lblControls.Location = new System.Drawing.Point(0, 0);
            this.lblControls.Name = "lblControls";
            this.lblControls.Size = new System.Drawing.Size(100, 23);
            this.lblControls.TabIndex = 0;
            // 
            // gunarbtnChangeMaterial
            // 
            this.gunarbtnChangeMaterial.AutoSize = true;
            this.gunarbtnChangeMaterial.BackColor = System.Drawing.Color.DarkKhaki;
            this.gunarbtnChangeMaterial.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gunarbtnChangeMaterial.CheckedState.BorderThickness = 0;
            this.gunarbtnChangeMaterial.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gunarbtnChangeMaterial.CheckedState.InnerColor = System.Drawing.Color.White;
            this.gunarbtnChangeMaterial.CheckedState.InnerOffset = -4;
            this.gunarbtnChangeMaterial.Font = new System.Drawing.Font("Stork Delivery", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunarbtnChangeMaterial.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gunarbtnChangeMaterial.Location = new System.Drawing.Point(207, 114);
            this.gunarbtnChangeMaterial.Name = "gunarbtnChangeMaterial";
            this.gunarbtnChangeMaterial.Size = new System.Drawing.Size(171, 30);
            this.gunarbtnChangeMaterial.TabIndex = 19;
            this.gunarbtnChangeMaterial.Text = "📏Change Material";
            this.gunarbtnChangeMaterial.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gunarbtnChangeMaterial.UncheckedState.BorderThickness = 2;
            this.gunarbtnChangeMaterial.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.gunarbtnChangeMaterial.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.gunarbtnChangeMaterial.UseVisualStyleBackColor = false;
            // 
            // gunarbtnChangeLighting
            // 
            this.gunarbtnChangeLighting.AutoSize = true;
            this.gunarbtnChangeLighting.BackColor = System.Drawing.Color.DarkKhaki;
            this.gunarbtnChangeLighting.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gunarbtnChangeLighting.CheckedState.BorderThickness = 0;
            this.gunarbtnChangeLighting.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gunarbtnChangeLighting.CheckedState.InnerColor = System.Drawing.Color.White;
            this.gunarbtnChangeLighting.CheckedState.InnerOffset = -4;
            this.gunarbtnChangeLighting.Font = new System.Drawing.Font("Stork Delivery", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunarbtnChangeLighting.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gunarbtnChangeLighting.Location = new System.Drawing.Point(207, 162);
            this.gunarbtnChangeLighting.Name = "gunarbtnChangeLighting";
            this.gunarbtnChangeLighting.Size = new System.Drawing.Size(162, 30);
            this.gunarbtnChangeLighting.TabIndex = 20;
            this.gunarbtnChangeLighting.Text = "🧊ChangeLighting";
            this.gunarbtnChangeLighting.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gunarbtnChangeLighting.UncheckedState.BorderThickness = 2;
            this.gunarbtnChangeLighting.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.gunarbtnChangeLighting.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.gunarbtnChangeLighting.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Stork Delivery", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 285);
            this.label1.TabIndex = 21;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // gunbtnSelectMaterial
            // 
            this.gunbtnSelectMaterial.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.gunbtnSelectMaterial.BorderColor = System.Drawing.Color.Transparent;
            this.gunbtnSelectMaterial.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.gunbtnSelectMaterial.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.gunbtnSelectMaterial.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.gunbtnSelectMaterial.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.gunbtnSelectMaterial.FillColor = System.Drawing.Color.Transparent;
            this.gunbtnSelectMaterial.FocusedColor = System.Drawing.Color.White;
            this.gunbtnSelectMaterial.Font = new System.Drawing.Font("Stork Delivery", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunbtnSelectMaterial.ForeColor = System.Drawing.Color.Black;
            this.gunbtnSelectMaterial.HoverState.BorderColor = System.Drawing.Color.White;
            this.gunbtnSelectMaterial.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.gunbtnSelectMaterial.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.gunbtnSelectMaterial.IndicateFocus = true;
            this.gunbtnSelectMaterial.Location = new System.Drawing.Point(30, 561);
            this.gunbtnSelectMaterial.Name = "gunbtnSelectMaterial";
            this.gunbtnSelectMaterial.Size = new System.Drawing.Size(298, 32);
            this.gunbtnSelectMaterial.TabIndex = 22;
            this.gunbtnSelectMaterial.Text = "🌈Seleccionar Material";
            // 
            // gunbtnSelectLight
            // 
            this.gunbtnSelectLight.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.gunbtnSelectLight.BorderColor = System.Drawing.Color.Transparent;
            this.gunbtnSelectLight.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.gunbtnSelectLight.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.gunbtnSelectLight.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.gunbtnSelectLight.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.gunbtnSelectLight.FillColor = System.Drawing.Color.Transparent;
            this.gunbtnSelectLight.FocusedColor = System.Drawing.Color.White;
            this.gunbtnSelectLight.Font = new System.Drawing.Font("Stork Delivery", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunbtnSelectLight.ForeColor = System.Drawing.Color.Black;
            this.gunbtnSelectLight.HoverState.BorderColor = System.Drawing.Color.White;
            this.gunbtnSelectLight.HoverState.CustomBorderColor = System.Drawing.Color.White;
            this.gunbtnSelectLight.HoverState.FillColor = System.Drawing.Color.Transparent;
            this.gunbtnSelectLight.IndicateFocus = true;
            this.gunbtnSelectLight.Location = new System.Drawing.Point(30, 599);
            this.gunbtnSelectLight.Name = "gunbtnSelectLight";
            this.gunbtnSelectLight.Size = new System.Drawing.Size(298, 32);
            this.gunbtnSelectLight.TabIndex = 23;
            this.gunbtnSelectLight.Text = "🌈Seleccionar Iluminacion";
            // 
            // ShapeExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(1041, 655);
            this.Controls.Add(this.gunbtnSelectLight);
            this.Controls.Add(this.gunbtnSelectMaterial);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gunarbtnChangeLighting);
            this.Controls.Add(this.gunarbtnChangeMaterial);
            this.Controls.Add(this.gunbtnSelectColor);
            this.Controls.Add(this.gunarbtnPaintFigures);
            this.Controls.Add(this.gunarbtnVertexes);
            this.Controls.Add(this.gunacmbMode);
            this.Controls.Add(this.gunacmbFigures);
            this.Controls.Add(this.picCanvas);
            this.Name = "ShapeExplorerForm";
            this.Text = "Figuras 3D ";
            this.Load += new System.EventHandler(this.ShapeExplorerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ComboBox gunacmbFigures;
        private Guna.UI2.WinForms.Guna2ComboBox gunacmbMode;
        private Guna.UI2.WinForms.Guna2RadioButton gunarbtnVertexes;
        private Guna.UI2.WinForms.Guna2RadioButton gunarbtnPaintFigures;
        private Guna.UI2.WinForms.Guna2Button gunbtnSelectColor;
        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Label lblControls;
        private Guna.UI2.WinForms.Guna2RadioButton gunarbtnChangeMaterial;
        private Guna.UI2.WinForms.Guna2RadioButton gunarbtnChangeLighting;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button gunbtnSelectMaterial;
        private Guna.UI2.WinForms.Guna2Button gunbtnSelectLight;
    }
}

