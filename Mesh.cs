using System;
using System.Collections.Generic;
using System.Linq;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DigCraft.Graphics
{
    public class Mesh
    {
        public List<Vector3> Vertices = new List<Vector3>();
        public List<Vector3> Normals = new List<Vector3>();
        public List<Vector2> UV = new List<Vector2>();
        public Color4[] Colors;
        public List<uint> indices = new List<uint>();


        public int vertexCount => indices.Count;

        int VAO, VBO, IBO, CBO, TBO,NBO;

        public Mesh()
        {
            Colors = new Color4[1];

            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            IBO = GL.GenBuffer();
            CBO = GL.GenBuffer();
            TBO = GL.GenBuffer();
            NBO = GL.GenBuffer();
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
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(), BufferUsageHint.StaticDraw);


            GL.BindBuffer(BufferTarget.ArrayBuffer, CBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Colors.Length * Vector4.SizeInBytes, Colors, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TBO);
            GL.BufferData(BufferTarget.ArrayBuffer, UV.Count * Vector2.SizeInBytes, UV.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, NBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Normals.Count * Vector3.SizeInBytes, Normals.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, 0, 0);
        }
        public void Add(Vector3 vert,Vector2 uv)
        {
            Vertices.Add(vert);
            UV.Add(uv);
        }
        public virtual void Draw()
        {

            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            GL.BindVertexArray(VAO);
            GL.DrawElements(BeginMode.Triangles, vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);

            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);

        }
        public void Clear()
        {
            Vertices.Clear();
            UV.Clear();
            Colors = new Color4[1];
            indices.Clear();
        }
    }
}
