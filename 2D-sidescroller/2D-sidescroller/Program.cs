using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Diagnostics;
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
			private int PositionBuffer, TexCoordBuffer;

			int VertexShader, FragmentShader, ShaderProgram;

			int[] Indices = new int[] {
				0, 1, 2,
				0, 2, 3
			};

			int PositionAttribute, TexCoordAttribute;

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
				GL.DepthFunc(DepthFunction.Lequal);

				InitShaders();

				InitAttributes();

				InitBuffers();

				//uint PlayerTexture = (uint)LoadTexture(getFilepathInDebugDir("player.png"));
			}

			private void InitShaders()
			{
				string vertexShaderSrc = @"
					#version 150

					in vec2 aPosition;
					in vec2 aTexCoord;

					out vec2 vTexCoord;

					void main() {
						gl_Position = vec4(aPosition, 0.0, 1.0);
						vTexCoord = aTexCoord;
					}

				";

				string fragmentShaderSrc = @"
					#version 150

					in vec2 vTexCoord;

					out vec4 fragmentColor;
					
					void main() {
						fragmentColor = vec4(0.5, 0.0, 0.0, 1.0);
					}
				";

				VertexShader = GL.CreateShader(ShaderType.VertexShader);
				GL.ShaderSource(VertexShader, vertexShaderSrc);
				GL.CompileShader(VertexShader);
				//Debug.WriteLine(GL.GetShaderInfoLog(VertexShader));

				FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
				GL.ShaderSource(FragmentShader, fragmentShaderSrc);
				GL.CompileShader(FragmentShader);
				//Debug.WriteLine(GL.GetShaderInfoLog(FragmentShader));

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
				GL.GenBuffers(1, out PositionBuffer);

				GL.BindBuffer(BufferTarget.ArrayBuffer, PositionBuffer);

				Vector2[] PositionData = new Vector2[] {
					new Vector2(-1f, -1f),
					new Vector2(-1f,  1f),
					new Vector2( 1f,  1f),
					new Vector2( 1f, -1f)
				};

				GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(PositionData.Length * Vector2.SizeInBytes), PositionData, BufferUsageHint.StaticDraw);
				GL.VertexAttribPointer(PositionAttribute, 2, VertexAttribPointerType.Float, false, 0, 0);

				//-----------

				GL.GenBuffers(1, out TexCoordBuffer);

				GL.BindBuffer(BufferTarget.ArrayBuffer, TexCoordBuffer);

				Vector2[] TexCoordData = new Vector2[] {
					new Vector2(0f, 0f),
					new Vector2(0f, 1f),
					new Vector2(1f, 1f),
					new Vector2(1f, 0f)
				};

				GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(TexCoordData.Length * Vector2.SizeInBytes), TexCoordData, BufferUsageHint.StaticDraw);
				GL.VertexAttribPointer(TexCoordBuffer, 2, VertexAttribPointerType.Float, false, 0, 0);
			}

			

			protected override void OnResize(EventArgs e)
			{
				base.OnResize(e);

				GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
			}

			protected override void OnUpdateFrame(FrameEventArgs e)
			{
				base.OnUpdateFrame(e);

                if (Keyboard[Key.Escape])
                {
                    Exit();
				}
			}

			protected override void OnKeyDown(KeyboardKeyEventArgs e)
			{
				base.OnKeyDown(e);
			
				//TODO use this instead
			}

			protected override void OnRenderFrame(FrameEventArgs e)
			{
				base.OnRenderFrame(e);

				GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

				//GL.BindBuffer(BufferTarget.ArrayBuffer, PositionBuffer);
				GL.EnableVertexAttribArray(PositionAttribute);
				//GL.EnableVertexAttribArray(TexCoordAttribute);

				//GL.BindBuffer(BufferTarget.ArrayBuffer, TexCoordBuffer);

				GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
				GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, Indices);

				GL.DisableVertexAttribArray(PositionAttribute);
				//GL.DisableVertexAttribArray(TexCoordAttribute);
				GL.Flush();

				SwapBuffers();
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
