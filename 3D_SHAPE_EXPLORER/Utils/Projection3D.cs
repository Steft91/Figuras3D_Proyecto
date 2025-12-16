using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Figuras3D_Proyecto.Models;

namespace Figuras3D_Proyecto.Utils
{
    public class Projection3D
    {
        public static PointF Project(Point3D point, Size panelSize)
        {
            float distance = 500;
            float factor = distance / (distance + point.Z);
            float x = point.X * factor + panelSize.Width / 2;
            float y = -point.Y * factor + panelSize.Height / 2;
            return new PointF(x, y);
        }
    }
}
