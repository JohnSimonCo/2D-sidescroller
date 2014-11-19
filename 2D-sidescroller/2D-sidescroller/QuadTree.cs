using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_sidescroller
{
	class QuadTree<T>
	{
		private static const int QUAD = 4;
		private static const int UL = 0;
		private static const int UR = 1;
		private static const int LL = 2;
		private static const int LR = 3;

		private bool IsRoot;

		private QuadTree<T>[] Children = null;

		private BoundingBox BB;

		public QuadTree(float width, float height)
		{
			IsRoot = true;

			BB = new BoundingBox(0f, 0f, width, height);
		}

		private QuadTree(float x, float y, float width, float height)
		{
			IsRoot = false;

			BB = new BoundingBox(x, y, width, height);
		}

		private bool IsLeaf()
		{
			return Children == null;
		}

		public void Divide()
		{
			if (IsLeaf())
			{
				Children = new QuadTree<T>[QUAD];
				for (int i = 0; i < Children.Length; i++)
				{
					//Children[i] = new QuadTree<T>(true);
				}
			}
			else
			{
				for (int i = 0; i < Children.Length; i++)
				{
					Children[i].Divide();
				}
			}
		}

		public List<T> Query(BoundingBox query)
		{
			return null;
		}
	}
}
