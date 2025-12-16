using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Figuras3D_Proyecto.Models;

namespace Figuras3D_Proyecto.Utils
{
    public class ShapeFactory
    {
        public static Shape3D Create(string name)
        {
            Shape3D shape = null;

            switch (name)
            {
                case "Cube": shape = new Cube(); break;
                case "Pyramid": shape = new Pyramid(); break;
                case "Octahedron": shape = new Octahedron(); break;
                case "Cylinder": shape = new Cylinder(); break;
                case "DodecagonalPrism": shape = new DodecagonalPrism(); break;
                case "Sphere": shape = new Sphere(); break;
                default: return null;
            }

            shape.GenerateShape();
            shape.OriginalPoints = shape.Points.Select(p => new Point3D(p.X, p.Y, p.Z)).ToList();

            return shape;
        }

        public static void CentrarYGuardarOriginales(Shape3D shape)
        {
            float centerX = shape.Points.Average(p => p.X);
            float centerY = shape.Points.Average(p => p.Y);
            float centerZ = shape.Points.Average(p => p.Z);

            shape.Traslate(-centerX, -centerY, -centerZ);
            shape.OriginalPoints = shape.Points.Select(p => new Point3D(p.X, p.Y, p.Z)).ToList(); 
        }

    }
}
