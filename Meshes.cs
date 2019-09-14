using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;

using DigCraft.Graphics;

namespace DigCraft
{
    public class Meshes
    {
        public static readonly Mesh loadingmesh = new Mesh()
        {
            Vertices = 
                {
                    new Vector3(-0.25f, 0.25f, 1f),
                    new Vector3( 0.0f, -0.25f, 1f),
                    new Vector3( 0.25f, 0.25f, 1f),
                },
            Colors = new Color4[] { Color4.Red, Color4.Green, Color4.Blue },
            UV =  { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0.5f, 1.0f) },
            indices = { 0, 1, 2 }

        };
        public static readonly Mesh quad = new Mesh()
        {
            Vertices =
            {
                new Vector3(0,0.5f,0.0f),
                new Vector3(0.5f,0.5f,0.0f),
                new Vector3(0.5f,0.0f,0.0f),
                new Vector3(0.0f,0.0f,0.0f)
            },
            indices ={ 1,2,3,0,1,2 }
        };
        public static Mesh Cube()
        {
            Mesh ret = new Mesh();


            List<Vector3> vertices = new List<Vector3>();
            List<int> ind = new List<int>();
            List<Color4> colors = new List<Color4>();
            List<Vector2> uv = new List<Vector2>()
            {
                new Vector2(0, 0), new Vector2(1, 0),
                new Vector2(0, 1), new Vector2(1, 1),
                new Vector2(0, 0), new Vector2(1, 0),
                new Vector2(0, 1), new Vector2(1, 1),
                new Vector2(0, 0), new Vector2(1, 0),
                new Vector2(0, 1), new Vector2(1, 1),
                new Vector2(0, 0), new Vector2(1, 0),
                new Vector2(0, 1), new Vector2(1, 1),
                new Vector2(0, 0), new Vector2(1, 0),
                new Vector2(0, 1), new Vector2(1, 1),
                new Vector2(0, 0), new Vector2(1, 0),
                new Vector2(0, 1), new Vector2(1, 1),
            };



            vertices.Add(new Vector3(-0.5f, +0.5f, -0.5f)); vertices.Add(new Vector3(-0.5f, +0.5f, +0.5f));
            vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f)); vertices.Add(new Vector3(-0.5f, -0.5f, +0.5f));
            vertices.Add(new Vector3(+0.5f, +0.5f, +0.5f)); vertices.Add(new Vector3(+0.5f, +0.5f, -0.5f));
            vertices.Add(new Vector3(+0.5f, -0.5f, +0.5f)); vertices.Add(new Vector3(+0.5f, -0.5f, -0.5f));
            vertices.Add(new Vector3(-0.5f, -0.5f, +0.5f)); vertices.Add(new Vector3(+0.5f, -0.5f, +0.5f));
            vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f)); vertices.Add(new Vector3(+0.5f, -0.5f, -0.5f));
            vertices.Add(new Vector3(-0.5f, +0.5f, -0.5f)); vertices.Add(new Vector3(+0.5f, +0.5f, -0.5f));
            vertices.Add(new Vector3(-0.5f, +0.5f, +0.5f)); vertices.Add(new Vector3(+0.5f, +0.5f, +0.5f));
            vertices.Add(new Vector3(+0.5f, +0.5f, -0.5f)); vertices.Add(new Vector3(-0.5f, +0.5f, -0.5f));
            vertices.Add(new Vector3(+0.5f, -0.5f, -0.5f)); vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            vertices.Add(new Vector3(-0.5f, +0.5f, +0.5f)); vertices.Add(new Vector3(+0.5f, +0.5f, +0.5f));
            vertices.Add(new Vector3(-0.5f, -0.5f, +0.5f)); vertices.Add(new Vector3(+0.5f, -0.5f, +0.5f));
            var indices = new uint[6 * 6];
            var faceIndices = new[] { 2, 1, 0, 2, 3, 1 };

            for (var i = 0; i < 6; i++)
                for (var j = 0; j < 6; j++)
                    indices[i * 6 + j] = (uint)(faceIndices[j] + i * 4);

            foreach (var f in vertices)
            {
                colors.Add(Color4.Red);
            }

            ret.Colors = colors.ToArray();
            ret.Vertices = vertices;
            ret.indices = indices.ToList();
            ret.UV = uv;
            return ret;
        }
    }
}
