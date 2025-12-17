using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Figuras3D_Proyecto.Models;
using Figuras3D_Proyecto.Utils;

namespace Figuras3D_Proyecto.Services
{
    public class ShapeRenderer
    {
        public void Draw(Graphics g, List<Shape3D> shapes, Size panelSize, SceneManager sceneManager, bool isInEditMode, Color paintColor)
        {
            foreach (var shape in shapes)
            {
                shape.ApplyTransformations();

                // ✅ CAMBIO: proyectar usando cámara
                var cam = (sceneManager != null) ? sceneManager.Camera : null;
                List<PointF> projectedPoints;

                if (cam != null)
                    projectedPoints = shape.Points.Select(p => Projection3D.Project(p, panelSize, cam)).ToList();
                else
                    projectedPoints = shape.Points.Select(p => Projection3D.Project(p, panelSize)).ToList();

                DrawCenter(g, projectedPoints);
                DrawFaces(g, shape, projectedPoints, sceneManager);
                DrawEdges(g, shape, projectedPoints);
                DrawSelection(g, shape, sceneManager, projectedPoints);
            }
        }

        private void DrawCenter(Graphics g, List<PointF> points)
        {
            float avgX = points.Average(p => p.X);
            float avgY = points.Average(p => p.Y);
            g.FillEllipse(Brushes.Red, avgX - 3, avgY - 3, 6, 6);
        }

        private void DrawFaces(Graphics g, Shape3D shape, List<PointF> points, SceneManager sceneManager)
        {
            for (int i = 0; i < shape.Faces.Count; i++)
            {
                var face = shape.Faces[i];
                ShapePainter.DrawFace(g, points, face, shape, sceneManager);
            }
        }

        private void DrawEdges(Graphics g, Shape3D shape, List<PointF> points)
        {
            Pen edgePen = shape.IsSelected ? new Pen(Color.Orange, 2) : new Pen(Color.Black, 1);

            foreach (var face in shape.Faces)
            {
                for (int i = 0; i < face.Count; i++)
                {
                    int idx1 = face[i];
                    int idx2 = face[(i + 1) % face.Count];
                    g.DrawLine(edgePen, points[idx1], points[idx2]);
                }
            }

            edgePen.Dispose();
        }

        private void DrawSelection(Graphics g, Shape3D shape, SceneManager sceneManager, List<PointF> points)
        {
            if (sceneManager.SelectedShapeIndex.HasValue &&
                sceneManager.SelectedVertexIndex.HasValue &&
                sceneManager.Shapes.IndexOf(shape) == sceneManager.SelectedShapeIndex.Value)
            {
                int index = sceneManager.SelectedVertexIndex.Value;
                if (index >= 0 && index < points.Count)
                {
                    PointF pt = points[index];
                    g.FillEllipse(Brushes.Blue, pt.X - 5, pt.Y - 5, 10, 10);
                }
            }

            if (sceneManager.SelectedShapeIndex.HasValue &&
                sceneManager.SelectedEdge != null &&
                sceneManager.Shapes.IndexOf(shape) == sceneManager.SelectedShapeIndex.Value)
            {
                var a = sceneManager.SelectedEdge.Item1;
                var b = sceneManager.SelectedEdge.Item2;

                if (a < points.Count && b < points.Count)
                {
                    Pen redPen = new Pen(Color.Red, 2);
                    g.DrawLine(redPen, points[a], points[b]);
                    redPen.Dispose();
                }
            }

            if (sceneManager.SelectedShapeIndex.HasValue &&
                 sceneManager.SelectedFace != null &&
                 sceneManager.Shapes.IndexOf(shape) == sceneManager.SelectedShapeIndex.Value &&
                 IsValidFace(sceneManager.SelectedFace, points.Count))
            {
                var facePoints = sceneManager.SelectedFace.Select(i => points[i]).ToArray();
                Pen purplePen = new Pen(Color.Purple, 2);
                g.DrawPolygon(purplePen, facePoints);
                purplePen.Dispose();
            }
        }

        private bool IsValidFace(List<int> face, int pointCount)
        {
            return face.Count >= 3 && face.All(i => i >= 0 && i < pointCount);
        }
    }
}
