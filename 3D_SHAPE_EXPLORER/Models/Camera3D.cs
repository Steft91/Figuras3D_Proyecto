using System;
using System.Drawing;

namespace Figuras3D_Proyecto.Models
{
    public enum CameraMode
    {
        Fixed = 0,
        Orbital = 1,
        Free = 2
    }

    public class Camera3D
    {
        public CameraMode Mode { get; set; } = CameraMode.Orbital;

        // Para Orbital
        public Point3D Target { get; set; } = new Point3D(0, 0, 0);
        public float Distance { get; set; } = 500f;

        // Rotaciones (radianes)
        public float Yaw { get; set; } = 0.7f;    // izquierda-derecha
        public float Pitch { get; set; } = -0.35f; // arriba-abajo
        public float Roll { get; set; } = 0f;

        // Para Free
        public Point3D Position { get; set; } = new Point3D(0, 0, 500);

        // Proyección
        public float Fov { get; set; } = 650f;      // “zoom” (más alto = menos perspectiva)
        public float Near { get; set; } = 1f;

        // Sensibilidades
        public float OrbitSensitivity { get; set; } = 0.008f;
        public float PanSensitivity { get; set; } = 0.8f;
        public float ZoomSensitivity { get; set; } = 40f;

        public void SetFixedFront()
        {
            Mode = CameraMode.Fixed;
            Position = new Point3D(0, 0, 500);
            Target = new Point3D(0, 0, 0);
            Yaw = 0f;
            Pitch = 0f;
            Roll = 0f;
            Distance = 500f;
        }

        public void SetOrbitalDefault()
        {
            Mode = CameraMode.Orbital;
            Target = new Point3D(0, 0, 0);
            Distance = 500f;
            Yaw = 0.7f;
            Pitch = -0.35f;
            Roll = 0f;
        }

        public void SetFreeDefault()
        {
            Mode = CameraMode.Free;

            // Coloca la cámara “frente” a la escena
            Position = new Point3D(0, 0, 800);

            // Que mire hacia el origen (0,0,0)
            Target = new Point3D(0, 0, 0);

            // Esto hace que el forward quede hacia -Z (si tu fórmula usa yaw/pitch)
            Yaw = (float)Math.PI;   // 180°
            Pitch = 0f;
            Roll = 0f;
        }

        public void Orbit(int dx, int dy)
        {
            Yaw += dx * OrbitSensitivity;
            Pitch += dy * OrbitSensitivity;

            // Limitar pitch para evitar volteos raros
            float limit = (float)(Math.PI / 2 - 0.01);
            if (Pitch > limit) Pitch = limit;
            if (Pitch < -limit) Pitch = -limit;
        }

        public void Zoom(int wheelDelta)
        {
            // wheelDelta suele venir en múltiplos de 120
            float step = (wheelDelta / 120f) * ZoomSensitivity;

            if (Mode == CameraMode.Orbital)
            {
                Distance -= step;
                if (Distance < 80f) Distance = 80f;
                if (Distance > 4000f) Distance = 4000f;
            }
            else
            {
                // En free, zoom ajusta Fov (estilo “acercar”)
                Fov += step * 1.5f;
                if (Fov < 200f) Fov = 200f;
                if (Fov > 2000f) Fov = 2000f;
            }
        }

        public void Pan(int dx, int dy)
        {
            // Paneo solo tiene sentido en Orbital/Fixed (mover el target)
            Target = new Point3D(
                Target.X - dx * PanSensitivity,
                Target.Y - dy * PanSensitivity,
                Target.Z
            );
        }
    }
}
