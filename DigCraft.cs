using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Drawing;

using DigCraft.IO;
using DigCraft.Graphics;
using DigCraft.Graphics.UI;
using DigCraft.Entities;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;
using DigCraft.VoxelEngine;


namespace DigCraft
{
    public class Digcraft:GameWindow
    {

        Chunk chunk = new Chunk();
        World world = new World();

        ShaderProgram ChunkShader;
        Matrix4d proj = Matrix4d.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 16 / 9, 0.01, 100.0);
        Mesh LoadingMesh;
        //dont change to loading until gui drawing is fixed
        GameState gamestate = GameState.Game;
        Texture2D tex;
        Entity entity;
        //ui textures
        Texture2D CrosshairTexture;

        Camera cam = new Camera();
        UIElement Crosshair;

        public Digcraft() : base(Config.Width, Config.Height, GraphicsMode.Default, "Digcraft", Config.Fullscreen ? GameWindowFlags.Fullscreen : GameWindowFlags.FixedWindow) { }
        protected override void OnLoad(EventArgs e)
        {
            Debug.Log("DigCraft v:0.1");
            tex = new Texture2D("Assets/Textures/Voxels/grass_top.png");
            CrosshairTexture = new Texture2D("Assets/Textures/Crosshair.png");

            ChunkShader = new ShaderProgram("Textured", true);

            Crosshair = new UIElement(CrosshairTexture, new Vector2(0.5f, 0.5f), 0.0f, new Vector2(0.05f, 0.05f));

            initGL();
            //cam.Position = new Vector3(55, 33, 59);
            LoadingMesh = Meshes.loadingmesh;

            chunk.Init();
            GL.Viewport(0, 0, Width, Height);
            Game.LoadingScene.init();
            UIRenderer.Init();
            entity = new Entity(Meshes.quad, ChunkShader, tex);

            world.GenerateWorld();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Focused)
            {
                time += (float)e.Time;

                if(gamestate == GameState.Game)
                {
                    Title = "Digcraft FPS:" + 1 / e.Time;
                    InputManager.Update1();

                    cam.Rotate(MathHelper.DegreesToRadians(-InputManager.GetMouseDelta().Y), MathHelper.DegreesToRadians(InputManager.GetMouseDelta().X));

                    if (InputManager.KeyDown(Key.W))
                    {
                        cam.Move(new Vector3(0, 0, -0.1f));
                    }
                    if (InputManager.KeyDown(Key.S))
                    {
                        cam.Move(new Vector3(0, 0, 0.1f));
                    }
                    if (InputManager.KeyDown(Key.A))
                    {
                        cam.Move(new Vector3(-0.1f, 0, 0));
                    }
                    if (InputManager.KeyDown(Key.D))
                    {
                        cam.Move(new Vector3(0.1f, 0, 0));
                    }
                    if (InputManager.KeyPressed(Key.E))
                    {
                        world.SetVoxel((int)Math.Floor(cam.Position.X), (int)Math.Floor(cam.Position.Y), (int)Math.Floor(cam.Position.Z), 1);
                    }
                    if (InputManager.KeyPressed(Key.Q))
                    {
                        world.SetVoxel((int)Math.Floor(cam.Position.X), (int)Math.Floor(cam.Position.Y), (int)Math.Floor(cam.Position.Z), 0);
                    }
                    Mouse.SetPosition(Width / 2 - X, Height / 2 - Y);
                    cam.Update();
                    chunk.Update();
                    world.Update();

                    InputManager.Update2();
                }
            }
        }
        float time;
        protected override void OnRenderFrame(FrameEventArgs e)
        {

            if(gamestate == GameState.Loading)
            {
                Game.LoadingScene.draw((float)e.Time);

                SwapBuffers();
            }
            else if(gamestate == GameState.Game)
            {


                GLHelper.Clear();
                GL.Enable(EnableCap.DepthTest);

                var mat = Matrix4.CreateTranslation(cam.Position);
                ChunkShader.Use();

                ChunkShader.SetMatrix("projection", GLHelper.proj);
                ChunkShader.SetMatrix("view", cam.View);
                ChunkShader.SetMatrix("transform", Matrix4.Identity);

                tex.bind();
                world.Draw();

                UIRenderer.DrawElement(Crosshair);


                SwapBuffers();

            }
            
        }
        void initGL()
        {

            //GL.Disable(EnableCap.CullFace);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }
    }
}
