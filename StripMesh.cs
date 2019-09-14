using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DigCraft.Graphics
{
    public class StripMesh
    {
        public List<Vector3> Vertices = new List<Vector3>();
        public Vector2[] UV;
        public Color4[] Colors;
        public uint[] indices;
        public int vertexCount => indices.Length;

        int VAO, VBO, IBO, CBO, TBO;

        public StripMesh()
        {
            UV = new Vector2[1];
            Colors = new Color4[1];
            indices = new uint[1];

            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            IBO = GL.GenBuffer();
            CBO = GL.GenBuffer();
            TBO = GL.GenBuffer();
        }
        public virtual void UploadToGL()
        {



            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            GL.NamedBufferStorage(VBO, Vector3.SizeInBytes * Vertices.Count, Vertices.ToArray(), BufferStorageFlags.MapWriteBit);

            GL.VertexArrayAttribBinding(VAO, 0, 0);
            GL.EnableVertexArrayAttrib(VAO, 0);

            GL.VertexArrayAttribFormat(VAO, 0, 3, VertexAttribType.Float, false, 0);

            GL.VertexArrayVertexBuffer(VAO, 0, VBO, IntPtr.Zero, Vector3.SizeInBytes);


            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);


            GL.BindBuffer(BufferTarget.ArrayBuffer, CBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Colors.Length * Vector4.SizeInBytes, Colors, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
            GL.BufferData(BufferTarget.ArrayBuffer, UV.Length * Vector2.SizeInBytes, UV, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);
        }
        public void Add(Vector3 vert)
        {
            Vertices.Add(vert);
        }
        public virtual void Draw()
        {

            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            GL.BindVertexArray(VAO);
            GL.DrawArrays(BeginMode.TriangleStrip, 0, 6);
            GL.BindVertexArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
        }
    }
}
