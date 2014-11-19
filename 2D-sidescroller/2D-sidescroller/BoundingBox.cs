using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_sidescroller
{
	class BoundingBox
	{
		public Vector2 Pos, Size;

		public BoundingBox(Vector2 pos, Vector2 size)
		{
			Pos = pos;
			Size = size;
		}

		/*
		 * Function to determin if other BoundingBox
		 * intersects with current BoundingBox.
		 * 
		 * @param o		Other bounding box
		 */
		public Boolean Intersects(BoundingBox o)
		{
			return (Math.Abs(Pos.X - o.Pos.X) * 2 < (Size.X + o.Size.X)) &&
			(Math.Abs(Pos.Y - o.Pos.Y) * 2 < (Size.Y + o.Size.Y));
		}

	}
}