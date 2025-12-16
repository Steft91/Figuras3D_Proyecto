using Figuras3D_Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static Figuras3D_Proyecto.Models.Shape3D;

namespace Figuras3D_Proyecto.Services
{
    public static class ShapePainter
    {
        // Compatibilidad: si alguien llama la versión antigua, no se rompe.
        public static void DrawFace(
            Graphics g,
            List<PointF> projectedPoints,
            List<int> face,
            Shape3D shape)
        {
            DrawFace(g, projectedPoints, face, shape, null);
        }

        // NUEVO: versión con SceneManager para sombreado
        public static void DrawFace(
            Graphics g,
            List<PointF> projectedPoints,
            List<int> face,
            Shape3D shape,
            SceneManager scene)
        {
            var polygon = face.Select(i => projectedPoints[i]).ToArray();

            // Color base (si no está pintado, gris)
            Color baseColor = shape.IsPainted ? shape.PaintColor : Color.Gray;

            // ------------------ SOMBREADO NUEVO (NO usa normales) ------------------
            float k = 1f;
            if (scene != null && scene.LightingEnabled)
            {
                // Centro de la cara (en pantalla)
                PointF faceCenter = Centroid(polygon);

                // Centro de la figura (en pantalla)
                float sx = 0f, sy = 0f;
                for (int i = 0; i < projectedPoints.Count; i++)
                {
                    sx += projectedPoints[i].X;
                    sy += projectedPoints[i].Y;
                }
                PointF shapeCenter = new PointF(sx / projectedPoints.Count, sy / projectedPoints.Count);

                // Luz fija arriba-derecha:
                // "arriba" = Y menor (en pantalla), "derecha" = X mayor
                bool lit = (faceCenter.X > shapeCenter.X) && (faceCenter.Y < shapeCenter.Y);

                k = lit ? scene.LightFactor : scene.ShadowFactor;
            }

            Color shaded = MultiplyColor(baseColor, k);
            // ---------------------------------------------------------------------

            Brush brush;

            switch (shape.Material)
            {
                case MaterialType.Rough:
                    brush = new System.Drawing.Drawing2D.HatchBrush(
                        System.Drawing.Drawing2D.HatchStyle.DottedGrid,
                        shaded,
                        MultiplyColor(Color.DarkGray, Clamp(k, 0.40f, 1.10f)));
                    break;

                case MaterialType.Striped:
                    brush = new System.Drawing.Drawing2D.HatchBrush(
                        System.Drawing.Drawing2D.HatchStyle.DiagonalCross,
                        shaded,
                        MultiplyColor(Color.Black, Clamp(k, 0.40f, 1.10f)));
                    break;

                case MaterialType.Smooth:
                    // gradiente suave para que se note más el “brillo”
                    {
                        GetBounds(polygon, out float minX, out float maxX, out float minY, out float maxY);
                        PointF p1 = new PointF(maxX, minY); // arriba-derecha
                        PointF p2 = new PointF(minX, maxY); // abajo-izquierda

                        Color light = MultiplyColor(shaded, 1.12f);
                        Color dark = MultiplyColor(shaded, 0.92f);

                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(p1, p2, light, dark);
                    }
                    break;

                default:
                    // sólido también con gradiente leve (se ve más 3D)
                    {
                        GetBounds(polygon, out float minX, out float maxX, out float minY, out float maxY);
                        PointF p1 = new PointF(maxX, minY); // arriba-derecha
                        PointF p2 = new PointF(minX, maxY); // abajo-izquierda

                        Color light = MultiplyColor(shaded, 1.08f);
                        Color dark = MultiplyColor(shaded, 0.95f);

                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(p1, p2, light, dark);
                    }
                    break;
            }

            g.FillPolygon(brush, polygon);
            brush.Dispose();
        }

        public static void DrawEdges(Graphics g, List<PointF> projectedPoints, List<int> face, Pen pen)
        {
            for (int i = 0; i < face.Count; i++)
            {
                int idx1 = face[i];
                int idx2 = face[(i + 1) % face.Count];
                g.DrawLine(pen, projectedPoints[idx1], projectedPoints[idx2]);
            }
        }

        public static void HighlightVertex(Graphics g, PointF pt)
        {
            g.FillEllipse(Brushes.Blue, pt.X - 5, pt.Y - 5, 10, 10);
        }

        public static void HighlightEdge(Graphics g, PointF a, PointF b)
        {
            using (Pen redPen = new Pen(Color.Red, 2))
                g.DrawLine(redPen, a, b);
        }

        public static void HighlightFace(Graphics g, List<PointF> facePoints)
        {
            using (Pen purplePen = new Pen(Color.Purple, 2))
                g.DrawPolygon(purplePen, facePoints.ToArray());
        }

        // ---------------- Helpers ----------------

        private static float Clamp(float v, float min, float max)
        {
            return Math.Max(min, Math.Min(max, v));
        }

        private static Color MultiplyColor(Color c, float k)
        {
            k = Clamp(k, 0f, 2f);

            int r = (int)Math.Round(c.R * k);
            int g = (int)Math.Round(c.G * k);
            int b = (int)Math.Round(c.B * k);

            r = (int)Clamp(r, 0, 255);
            g = (int)Clamp(g, 0, 255);
            b = (int)Clamp(b, 0, 255);

            return Color.FromArgb(c.A, r, g, b);
        }

        private static PointF Centroid(PointF[] pts)
        {
            float x = 0f, y = 0f;
            for (int i = 0; i < pts.Length; i++)
            {
                x += pts[i].X;
                y += pts[i].Y;
            }
            return new PointF(x / pts.Length, y / pts.Length);
        }

        private static void GetBounds(PointF[] pts, out float minX, out float maxX, out float minY, out float maxY)
        {
            minX = pts.Min(p => p.X);
            maxX = pts.Max(p => p.X);
            minY = pts.Min(p => p.Y);
            maxY = pts.Max(p => p.Y);
        }
    }
}
