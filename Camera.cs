using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DigCraft.Graphics
{
    public class Camera
    {
        public Vector3 Right;
        public Vector3 Up;
        public Vector3 Forwards;
        public Vector3 Position;
        public Matrix4 View;

        public float Pitch, Yaw;

        public Camera()
        {
            Right = Vector3.UnitX;
        }
        public void Move(Vector3 mov)
        {
            var Delta = Vector3.Zero;

            Delta += Right * mov.X;
            Delta += Up * mov.Y;
            Delta += Forwards * mov.Z;

            Position += Delta;
        }
        public void Rotate(float pitch,float yaw)
        {
            Pitch += pitch;
            Yaw += yaw;

            Pitch = MathHelper.Clamp(Pitch, -MathHelper.PiOver2 + 0.1f, MathHelper.PiOver2 - 0.1f);
            Yaw %= MathHelper.TwoPi;
        }
        public void Update()
        {
            Forwards = new Vector3((float)Math.Sin(Yaw) * (float)Math.Cos(Pitch), (float)Math.Sin(Pitch), (float)Math.Cos(Pitch) * (float)Math.Cos(Yaw));
            Up = Vector3.UnitY;
            Right = Vector3.Cross(Up, Forwards);
            Right.NormalizeFast();

            View = Matrix4.LookAt(Position, Position - Forwards, Up);
        }
    }
}
