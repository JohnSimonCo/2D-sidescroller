using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_sidescroller
{
	class BoundingBox
	{

		public float X, Y, W, H;

		public BoundingBox(float x, float y, float w, float h) {
			this.X = x;
			this.Y = y;
			this.W = w;
			this.H = h;
		}

		/*
		 * Function to determin if other BoundingBox
		 * intersects with current BoundingBox.
		 * 
		 * @param o		Other bounding box
		 */
		public Boolean Intersects(BoundingBox o)
		{
			return (Math.Abs(X - o.X) * 2 < (W + o.W)) &&
			(Math.Abs(Y - o.Y) * 2 < (H + o.H));
		}

	}
}
