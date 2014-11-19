using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace _2D_sidescroller
{
	class Level
	{

		List<Entity> Entities;

		public Vector2 Size;

		public Level(Vector2 size)
		{
			this.Size = size;
		}

		public void Update(long time)
		{
			for (int i = 0; i < Entities.Count; i++)
			{
				Entities[i].Update(time);
			}
		}

	}
}
