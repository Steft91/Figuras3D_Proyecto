using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figuras3D_Proyecto.Models
{
    public class Sphere : Shape3D
    {
        public int LatitudeSegments { get; set; } = 12;
        public int LongitudeSegments { get; set; } = 12;
        public float Radius { get; set; } = 50f;

        public override void GenerateShape()
        {
            Points.Clear();
            Edges.Clear();
            Faces.Clear();

            // Generar puntos
            for (int lat = 0; lat <= LatitudeSegments; lat++)
            {
                float theta = (float)Math.PI * lat / LatitudeSegments;
                float sinTheta = (float)Math.Sin(theta);
                float cosTheta = (float)Math.Cos(theta);

                for (int lon = 0; lon <= LongitudeSegments; lon++)
                {
                    float phi = 2f * (float)Math.PI * lon / LongitudeSegments;
                    float sinPhi = (float)Math.Sin(phi);
                    float cosPhi = (float)Math.Cos(phi);

                    float x = Radius * sinTheta * cosPhi;
                    float y = Radius * cosTheta;
                    float z = Radius * sinTheta * sinPhi;

                    Points.Add(new Point3D(x, y, z));
                }
            }

            // Generar aristas
            int cols = LongitudeSegments + 1;

            for (int lat = 0; lat < LatitudeSegments; lat++)
            {
                for (int lon = 0; lon < LongitudeSegments; lon++)
                {
                    int current = lat * cols + lon;
                    int next = current + cols;

                    Edges.Add((current, current + 1));
                    Edges.Add((current, next));
                }
            }

            // Generar caras (opcional pero compatible)
            for (int lat = 0; lat < LatitudeSegments; lat++)
            {
                for (int lon = 0; lon < LongitudeSegments; lon++)
                {
                    int current = lat * cols + lon;

                    Faces.Add(new List<int>
                    {
                        current,
                        current + 1,
                        current + cols + 1,
                        current + cols
                    });
                }
            }

            // Guardar puntos originales
            OriginalPoints = Points
                .Select(p => new Point3D(p.X, p.Y, p.Z))
                .ToList();
        }

        public override Shape3D Clone()
        {
            var clone = new Sphere
            {
                Radius = this.Radius,
                LatitudeSegments = this.LatitudeSegments,
                LongitudeSegments = this.LongitudeSegments
            };

            clone.OriginalPoints = this.OriginalPoints
                .Select(p => new Point3D(p.X, p.Y, p.Z))
                .ToList();

            clone.Points = clone.OriginalPoints
                .Select(p => new Point3D(p.X, p.Y, p.Z))
                .ToList();

            clone.Edges = new List<(int, int)>(this.Edges);
            clone.Faces = this.Faces.Select(f => new List<int>(f)).ToList();
            clone.IsPainted = this.IsPainted;

            this.CopyTransformationsTo(clone);
            return clone;
        }
    }
}