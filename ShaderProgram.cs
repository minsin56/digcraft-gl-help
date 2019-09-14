using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace DigCraft.Graphics
{
    public class ShaderProgram
    {
        public int VertexShader, FragmentShader, Program;

        public string FragmentPath { get; private set; }
        public string VertexPath { get; private set; }
        string name;

        public ShaderProgram(string name,bool CompileNow = false)
        {
            FragmentPath = "Assets/Shaders/" + name + ".frag";
            VertexPath = "Assets/Shaders/" + name + ".vert";
            this.name = name;

            if (CompileNow)
            {
                Compile();
            }
        }
        public void Compile()
        {

            int status;

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, File.ReadAllText(VertexPath));
            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out status);
            if(status == 0)
            {
                IO.Debug.LogError("Vertex Shader Error in:" + name + " Error: " + GL.GetShaderInfoLog(VertexShader));
            }

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, File.ReadAllText(FragmentPath));
            GL.CompileShader(FragmentShader);

            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out status);
            if (status == 0)
            {
                IO.Debug.LogError("Fragment Shader Error in:" + name + " Error: " + GL.GetShaderInfoLog(FragmentShader));
            }

            GL.GetProgram(Program, GetProgramParameterName.LinkStatus, out status);

            if(status == 0)
            {
                IO.Debug.LogError("Shader Link Error in:" + name + " Error: " + GL.GetProgramInfoLog(Program));
            }

            Program = GL.CreateProgram();
            GL.AttachShader(Program, VertexShader);
            GL.AttachShader(Program, FragmentShader);

            GL.LinkProgram(Program);

            GL.DetachShader(Program, VertexShader);
            GL.DetachShader(Program, FragmentShader);
            GL.DeleteShader(VertexShader);
            GL.DeleteShader(FragmentShader);
        }
        public void Use()
        {
            GL.UseProgram(Program);
        }
        public void SetFloat(string name,float f)
        {
            int index = GL.GetUniformLocation(Program, name);
            GL.Uniform1(index, f);
        }
        public void SetMatrix(string name,Matrix4 mat)
        {
            int index = GL.GetUniformLocation(Program, name);
            GL.UniformMatrix4(index, false,ref mat);
        }
        public int GetAttribLoc(string name) => GL.GetUniformLocation(Program, name);
    }
}
