using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DigCraft.Graphics
{
    public class Texture2D
    {
        public int id;

        public int width, height;
        public Color[] pixels;

        public Texture2D()
        {
            this.id = 0;
            this.width = 0;
            this.height = 0;
        }
        public Texture2D(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path,System.IO.FileMode.Open);
            Bitmap bmp = new Bitmap(fs);

            id = GL.GenTexture();

            BitmapData bmpdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpdata.Scan0);

            bmp.UnlockBits(bmpdata);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            
            width = bmp.Width;
            height = bmp.Height;

            fs.Close();
            fs.Dispose();
        }
        public void bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, id);
        }
    }
}
