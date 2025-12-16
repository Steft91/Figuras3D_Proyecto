using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Figuras3D_Proyecto.Models;

namespace Figuras3D_Proyecto.Services
{
    public class KeyboardController
    {
        private readonly Timer timer = new Timer();
        private readonly HashSet<Keys> keys = new HashSet<Keys>();
        private readonly Control targetCanvas;
        private readonly SceneManager sceneManager;

        private const float RotationStep = 2f;
        private const float ScaleStep = 0.05f;
        private const float TranslationStep = 2f;


        public KeyboardController(Form form, Control canvas, SceneManager manager)
        {
            sceneManager = manager;
            targetCanvas = canvas;

            form.KeyPreview = true;
            form.KeyDown += (s, e) => keys.Add(e.KeyCode);
            form.KeyDown += HandleDeleteKey;
            form.KeyUp += (s, e) => keys.Remove(e.KeyCode);


            timer.Interval = 20;
            timer.Tick += (s, e) => UpdateTransformations();
            timer.Start();
        }

        private void UpdateTransformations()
        {
            Shape3D selectedShape = sceneManager.Shapes.Find(s => s.IsSelected);
            if (selectedShape == null)
                return;

            bool somethingTransformed = false;

            if (sceneManager.SelectedVertexIndex.HasValue)
            {
                int index = sceneManager.SelectedVertexIndex.Value;
                ApplyTransformationToPoint(selectedShape.OriginalPoints[index]);
                somethingTransformed = true;
            }

            else if (sceneManager.SelectedEdge != null)
            {
                var (i1, i2) = sceneManager.SelectedEdge;
                ApplyTransformationToPoint(selectedShape.OriginalPoints[i1]);
                ApplyTransformationToPoint(selectedShape.OriginalPoints[i2]);
                somethingTransformed = true;
            }

            else if (sceneManager.SelectedFace != null)
            {
                foreach (var index in sceneManager.SelectedFace)
                {
                    ApplyTransformationToPoint(selectedShape.OriginalPoints[index]);

                }

                somethingTransformed = true;
            }

            if (!somethingTransformed)
            {
                // Rotación
                if (keys.Contains(Keys.Q)) selectedShape.RotationX -= RotationStep;
                if (keys.Contains(Keys.E)) selectedShape.RotationX += RotationStep;

                if (keys.Contains(Keys.A)) selectedShape.RotationY -= RotationStep;
                if (keys.Contains(Keys.D)) selectedShape.RotationY += RotationStep;

                if (keys.Contains(Keys.R)) selectedShape.RotationZ -= RotationStep;
                if (keys.Contains(Keys.F)) selectedShape.RotationZ += RotationStep;


                // Scale
                if (keys.Contains(Keys.W)) selectedShape.ScaleFactor += ScaleStep;
                if (keys.Contains(Keys.S)) selectedShape.ScaleFactor = Math.Max(0.1f, selectedShape.ScaleFactor - ScaleStep);

                // Traslación
                if (keys.Contains(Keys.Left)) selectedShape.TraslateX -= TranslationStep;
                if (keys.Contains(Keys.Right)) selectedShape.TraslateX += TranslationStep;
                if (keys.Contains(Keys.Up)) selectedShape.TraslateY += TranslationStep;
                if (keys.Contains(Keys.Down)) selectedShape.TraslateY -= TranslationStep;
                if (keys.Contains(Keys.Z)) selectedShape.TraslateZ += TranslationStep;
                if (keys.Contains(Keys.X)) selectedShape.TraslateZ -= TranslationStep;

            }

            targetCanvas.Invalidate();
        }

        private void ApplyTransformationToPoint(Point3D point)
        {
            // Traslación
            if (keys.Contains(Keys.Left)) point.X -= TranslationStep;
            if (keys.Contains(Keys.Right)) point.X += TranslationStep;
            if (keys.Contains(Keys.Up)) point.Y += TranslationStep;
            if (keys.Contains(Keys.Down)) point.Y -= TranslationStep;
            if (keys.Contains(Keys.Z)) point.Z += TranslationStep;
            if (keys.Contains(Keys.X)) point.Z -= TranslationStep;

            // Escalado
            if (keys.Contains(Keys.W))
            {
                point.X *= 1 + ScaleStep;
                point.Y *= 1 + ScaleStep;
                point.Z *= 1 + ScaleStep;
            }
            if (keys.Contains(Keys.S))
            {
                point.X *= 1 - ScaleStep;
                point.Y *= 1 - ScaleStep;
                point.Z *= 1 - ScaleStep;
            }
        }



        private void HandleDeleteKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                Shape3D selectedShape = sceneManager.Shapes.Find(s => s.IsSelected);
                if (selectedShape != null)
                {
                    var result = MessageBox.Show(
                        "Do you want to delete the selected figure?",
                        "Confirm deletion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        sceneManager.Shapes.Remove(selectedShape);
                        targetCanvas.Invalidate(); 
                    }
                }
            }
        }

        


    }
}
