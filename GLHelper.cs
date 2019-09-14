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
    public class GLHelper
    {
        public static Matrix4 proj => Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2,IO.Config.Width / IO.Config.Height, 0.01f, float.MaxValue);

        public static void Clear(GameTime gametime = GameTime.Day)
        {
            switch (gametime)
            {
                case GameTime.Day:
                    GL.ClearColor(System.Drawing.Color.SkyBlue);
                    break;
                case GameTime.Night:
                    GL.ClearColor(System.Drawing.Color.FromArgb(7, 11, 52));
                    break;
            }
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
        }
        public static void DrawQuad(double width,double height)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.TexCoord2(0, 0); GL.Vertex2(0, height);
            GL.TexCoord2(width, height); GL.Vertex2(width, 0);
            GL.TexCoord2(0, height); GL.Vertex2(0, 0);

            GL.TexCoord2(0, 0);GL.Vertex2(0, height);
            GL.TexCoord2(width, 0);GL.Vertex2(width, height);
            GL.TexCoord2(width, height);GL.Vertex2(width, 0);

            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }
    }
}
