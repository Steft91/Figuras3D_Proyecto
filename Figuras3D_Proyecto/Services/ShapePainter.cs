using Figuras3D_Proyecto.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static Figuras3D_Proyecto.Models.Shape3D;

namespace Figuras3D_Proyecto.Services
{
    public static class ShapePainter
    {
        public static void DrawFace(
    Graphics g,
    List<PointF> projectedPoints,
    List<int> face,
    Shape3D shape)
        {
            var polygon = face.Select(i => projectedPoints[i]).ToArray();
            Brush brush;

            switch (shape.Material)
            {
                case MaterialType.Rough:
                    brush = new System.Drawing.Drawing2D.HatchBrush(
                        System.Drawing.Drawing2D.HatchStyle.DottedGrid,
                        shape.PaintColor,
                        Color.DarkGray);
                    break;

                case MaterialType.Striped:
                    brush = new System.Drawing.Drawing2D.HatchBrush(
                        System.Drawing.Drawing2D.HatchStyle.DiagonalCross,
                        shape.PaintColor,
                        Color.Black);
                    break;

                case MaterialType.Smooth:
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        polygon[0],
                        polygon[2],
                        Color.White,
                        shape.PaintColor);
                    break;

                default:
                    brush = new SolidBrush(
                        shape.IsPainted ? shape.PaintColor : Color.Gray);
                    break;
            }

            g.FillPolygon(brush, polygon);
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
    }
}
