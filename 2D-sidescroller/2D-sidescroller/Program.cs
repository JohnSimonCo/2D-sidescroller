using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
<<<<<<< HEAD
using System.Diagnostics;
=======
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
>>>>>>> origin/master

namespace _2D_sidescroller
{
    class Program
    {
		class Game : GameWindow
		{
<<<<<<< HEAD
			private uint[] Buffers = new uint[2];

			private int BUFFER_POSITIONS = 0;
			private int BUFFER_TEXCOORDS = 1;

			int VertexShader, FragmentShader, ShaderProgram;

			int PositionAttribute, TexCoordAttribute;
=======

			uint PlayerTexture;
>>>>>>> origin/master

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
<<<<<<< HEAD
				GL.DepthFunc(DepthFunction.Lequal);

				InitShaders();

				InitAttributes();

				InitBuffers();
			}

			private void InitShaders()
			{
				string vertexShaderSrc = @"
					#version 150

					attribute vec3 aPosition;
					attribute float aTexCoord;

					varying lowp float vTexCoord;

					void main() {
						gl_Position = vec4(aPosition);
						vTexCoord = aTexCoord;
					}

				";

				string fragmentShaderSrc = @"
					#version 150

					varying lowp float vTexCoord;

					void main() {
						gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);
					}
				";

				VertexShader = GL.CreateShader(ShaderType.FragmentShader);
				GL.ShaderSource(VertexShader, vertexShaderSrc);
				GL.CompileShader(VertexShader);
				Debug.WriteLine(GL.GetShaderInfoLog(VertexShader));

				FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
				GL.ShaderSource(FragmentShader, fragmentShaderSrc);
				GL.CompileShader(FragmentShader);
				Debug.WriteLine(GL.GetShaderInfoLog(FragmentShader));

				// Create program
				ShaderProgram = GL.CreateProgram();
				GL.AttachShader(ShaderProgram, VertexShader);
				GL.AttachShader(ShaderProgram, FragmentShader);
				GL.LinkProgram(ShaderProgram);
				Debug.WriteLine(GL.GetProgramInfoLog(ShaderProgram));

				GL.UseProgram(ShaderProgram);
			}

			private void InitAttributes()
			{
				PositionAttribute = GL.GetAttribLocation(ShaderProgram, "aPosition");
				TexCoordAttribute = GL.GetAttribLocation(ShaderProgram, "aTexCoord");
			}

			private void InitBuffers()
			{
				GL.GenBuffers(2, Buffers);

				GL.BindBuffer(BufferTarget.ArrayBuffer, Buffers[BUFFER_POSITIONS]);

				float[] positions = new float[] {
				//  x	  y
					0.0f, 1.0f,
					1.0f, 1.0f,
					1.0f, 0.0f,
					0.0f, 0.0f

				};
				//Amount of data per vertex
				int positionAmount = 2;

				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(positions.Length * positionAmount * sizeof(float)), positions, BufferUsageHint.StaticDraw);


				GL.BindBuffer(BufferTarget.ArrayBuffer, Buffers[BUFFER_POSITIONS]);

				float[] texCoords = new float[] {
				//  x	  y
					0.0f, 1.0f,
					1.0f, 1.0f,
					1.0f, 0.0f,
					0.0f, 0.0f

				};
				//Amount of data per vertex
				int texcoordAmount = 2;

				GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(texCoords.Length * texcoordAmount * sizeof(float)), texCoords, BufferUsageHint.StaticDraw);
=======
				GL.Enable(EnableCap.Texture2D);

				PlayerTexture = (uint)LoadTexture(getFilepathInDebugDir("player.png"));
>>>>>>> origin/master
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
				/*
				Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
				GL.MatrixMode(MatrixMode.Modelview);
				GL.LoadMatrix(ref modelview);
				*/

				GL.BindBuffer(BufferTarget.ArrayBuffer, Buffers[BUFFER_POSITIONS]);
				GL.VertexAttribPointer(PositionAttribute, 2, VertexAttribPointerType.Float, false, 0, 0);

				GL.BindBuffer(BufferTarget.ArrayBuffer, Buffers[BUFFER_TEXCOORDS]);
				GL.VertexAttribPointer(PositionAttribute, 2, VertexAttribPointerType.Float, false, 0, 0);
				
				GL.DrawArrays(PrimitiveType.Triangles, 0, 4);
			}

			protected override void OnClosed(EventArgs e)
			{
				base.OnClosed(e);
			
				
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
