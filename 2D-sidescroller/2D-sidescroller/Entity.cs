using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace _2D_sidescroller
{
	public class Entity
	{

		public Vector2 Pos;
		public Vector2 Size;
		public uint Texture;

		public Entity(Vector2 pos, Vector2 size, uint texture)
		{
			this.Pos = pos;
			this.Size = size;
			this.Texture = texture;

		}

		public void Update(long time)
		{
			
		}

	}
}
