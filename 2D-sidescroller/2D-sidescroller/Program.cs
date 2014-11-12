using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace _2D_sidescroller
{
    class Program
    {
		class Game : GameWindow
		{

			uint PlayerTexture;

			public Game()
				: base(800, 600, GraphicsMode.Default, "John och Simons superspel")
			{
				VSync = VSyncMode.On;
			//	Keyboard.KeyDown += new KeyDownEvent(OnKeyDown);
			}

			protected override void OnLoad(EventArgs e)
			{
				base.OnLoad(e);

				GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
				GL.Enable(EnableCap.DepthTest);
				GL.Enable(EnableCap.Texture2D);

				PlayerTexture = (uint)LoadTexture(getFilepathInDebugDir("player.png"));
			}

			

			protected override void OnResize(EventArgs e)
			{
				base.OnResize(e);

				GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

				Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
				GL.MatrixMode(MatrixMode.Projection);
				GL.LoadMatrix(ref projection);
			}

			protected override void OnUpdateFrame(FrameEventArgs e)
			{
				base.OnUpdateFrame(e);

                if (Keyboard[Key.Escape])
                {
                    Exit();
				}
				else if (Keyboard[Key.Right])
				{

				}
				else if (Keyboard[Key.Left])
				{

				}
				else if (Keyboard[Key.Up])
				{

				}
				else if (Keyboard[Key.Down])
				{

				}
			}

			protected override void OnRenderFrame(FrameEventArgs e)
			{
				base.OnRenderFrame(e);

				GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

				Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
				GL.MatrixMode(MatrixMode.Modelview);
				GL.LoadMatrix(ref modelview);

				GL.Begin(BeginMode.Triangles);

				GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 4.0f);
				GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 4.0f);
				GL.Color3(0.2f, 0.9f, 1.0f); GL.Vertex3(0.0f, 1.0f, 4.0f);

				GL.End();

				SwapBuffers();
			}

			private string getFilepathInDebugDir(string path)
			{
				return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), path).Replace("file:\\", "");
			}


			static int LoadTexture(string filename)
			{
				if (String.IsNullOrEmpty(filename))
					throw new ArgumentException(filename);

				int id = GL.GenTexture();
				GL.BindTexture(TextureTarget.Texture2D, id);

				Bitmap bmp = new Bitmap(filename);
				BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
					OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

				bmp.UnlockBits(bmp_data);

				// We haven't uploaded mipmaps, so disable mipmapping (otherwise the texture will not appear).
				// On newer video cards, we can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
				// mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

				return id;
			}


			[STAThread]
			static void Main()
			{
				// The 'using' idiom guarantees proper resource cleanup.
				// We request 60 UpdateFrame events per second, and unlimited
				// RenderFrame events (as fast as the computer can handle).
				using (Game game = new Game())
				{
					game.Run(60.0);
				}
			}
		}
    }
}
