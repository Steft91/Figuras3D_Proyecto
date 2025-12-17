using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Figuras3D_Proyecto.Models;

namespace Figuras3D_Proyecto.Services
{
    public class SceneManager
    {
        public List<Shape3D> Shapes { get; private set; } = new List<Shape3D>();
        public Camera3D Camera { get; private set; } = new Camera3D();

        public int? SelectedVertexIndex = null;
        public Tuple<int, int> SelectedEdge = null;
        public List<int> SelectedFace = null;
        private int totalShapesInserted = 1;
        public int? SelectedShapeIndex = null;  // <- para saber de qué figura es el vértice seleccionado

        // ------------------ NUEVO: Sombrado simple (estable) ------------------
        public bool LightingEnabled { get; set; } = true;

        // Qué tan oscuras quedan las caras "en sombra" (0.35..0.70 recomendado)
        public float ShadowFactor { get; set; } = 0.55f;

        // Qué tan claras quedan las caras "iluminadas" (1.05..1.40 recomendado)
        public float LightFactor { get; set; } = 1.25f;
        // --------------------------------------------------------------------

        public void Initialize()
        {
            var cube = new Cube();
            cube.GenerateShape();
            Shapes.Add(cube);
            Camera.SetOrbitalDefault();
        }

        public void AddShape(Shape3D shape)
        {
            float spacing = 100f;
            shape.GenerateShape();

            float centerX = shape.Points.Average(p => p.X);
            float centerY = shape.Points.Average(p => p.Y);
            float centerZ = shape.Points.Average(p => p.Z);

            shape.Traslate(-centerX, -centerY, -centerZ);

            shape.OriginalPoints = shape.Points.Select(p => new Point3D(p.X, p.Y, p.Z)).ToList();

            shape.TraslateX = spacing * totalShapesInserted;

            shape.ApplyTransformations();

            Shapes.Add(shape);
            totalShapesInserted++;
        }

        public void ResetSelectedShape()
        {
            var selected = Shapes.FirstOrDefault(s => s.IsSelected);
            if (selected != null)
            {
                selected.RotationX = 0;
                selected.RotationY = 0;
                selected.RotationZ = 0;

                selected.ScaleFactor = 1f;

                selected.TraslateX = 0;
                selected.TraslateY = 0;
                selected.TraslateZ = 0;
            }
        }
    }
}
