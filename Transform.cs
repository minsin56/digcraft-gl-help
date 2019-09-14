using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;

namespace DigCraft.Graphics
{
    public class Transform
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        public Transform()
        {
            scale = new Vector3(1, 1, 1);
            position = new Vector3(0, 0, 0);
            rotation = new Vector3(0, 0, 0);
        }
        public void SetPosition(Vector3 position)
        {
            this.position = position;
        }
        public void SetPositionAndRotation(Vector3 position,Vector3 rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
        public void Translate(Vector3 to,float t)
        {
            position = Vector3.Lerp(position, to, t);
        }
        public Matrix4 GetTransformMatrix => Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) * Matrix4.CreateTranslation(position) * Matrix4.CreateScale(scale);
    }
}
