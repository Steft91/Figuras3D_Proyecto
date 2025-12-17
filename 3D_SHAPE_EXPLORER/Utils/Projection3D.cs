using System;
using System.Drawing;
using Figuras3D_Proyecto.Models;

namespace Figuras3D_Proyecto.Utils
{
    public static class Projection3D
    {
        // Compatibilidad (si alguien la usa todavía)
        public static PointF Project(Point3D p, Size panelSize)
        {
            // cámara fija simple (frontal)
            var cam = new Camera3D();
            cam.SetFixedFront();
            return Project(p, panelSize, cam);
        }

        public static PointF Project(Point3D p, Size panelSize, Camera3D cam)
        {
            // 1) Transformación de "vista" (world -> view)
            Point3D v = WorldToView(p, cam);

            // 2) Proyección perspectiva
            // Evitar dividir por 0 o valores negativos
            float z = (v.Z < cam.Near) ? cam.Near : v.Z;

            float cx = panelSize.Width / 2f;
            float cy = panelSize.Height / 2f;

            float scale = cam.Fov / z;

            float x2 = cx + v.X * scale;
            float y2 = cy - v.Y * scale; // Y invertida pantalla

            return new PointF(x2, y2);
        }

        private static Point3D WorldToView(Point3D p, Camera3D cam)
        {
            // Determinar posición real de cámara:
            Point3D camPos;
            Point3D target;

            if (cam.Mode == CameraMode.Orbital)
            {
                target = cam.Target;

                // Orbital: posición se deriva de yaw/pitch/distance alrededor del target
                float cy = (float)Math.Cos(cam.Yaw);
                float sy = (float)Math.Sin(cam.Yaw);
                float cp = (float)Math.Cos(cam.Pitch);
                float sp = (float)Math.Sin(cam.Pitch);

                // Vector desde target hacia cámara
                float x = cam.Distance * cp * sy;
                float y = cam.Distance * sp;
                float z = cam.Distance * cp * cy;

                camPos = new Point3D(target.X + x, target.Y + y, target.Z + z);
            }
            else if (cam.Mode == CameraMode.Fixed)
            {
                camPos = cam.Position;
                target = cam.Target;
            }
            else // Free
            {
                camPos = cam.Position;
                target = cam.Target;   // <-- IMPORTANTE: mirar al target real
            }

            // Construir ejes de cámara (lookAt):
            Point3D zaxis = Normalize(Sub(target, camPos));      // forward
            Point3D xaxis = Normalize(Cross(zaxis, new Point3D(0, 1, 0))); // right
            Point3D yaxis = Cross(xaxis, zaxis);                 // up

            // Trasladar al origen cámara
            Point3D t = Sub(p, camPos);

            // Proyectar sobre ejes (dot)
            float vx = Dot(t, xaxis);
            float vy = Dot(t, yaxis);
            float vz = Dot(t, zaxis);

            // Roll (opcional)
            if (Math.Abs(cam.Roll) > 1e-6)
            {
                float cr = (float)Math.Cos(cam.Roll);
                float sr = (float)Math.Sin(cam.Roll);

                float rx = vx * cr - vy * sr;
                float ry = vx * sr + vy * cr;
                vx = rx;
                vy = ry;
            }

            return new Point3D(vx, vy, vz);
        }

        // --------- math helpers ---------
        private static float Dot(Point3D a, Point3D b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        private static Point3D Sub(Point3D a, Point3D b) => new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        private static Point3D Cross(Point3D a, Point3D b)
        {
            return new Point3D(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        private static Point3D Normalize(Point3D v)
        {
            float len = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            if (len < 1e-6f) return new Point3D(0, 0, 0);
            return new Point3D(v.X / len, v.Y / len, v.Z / len);
        }
    }
}
