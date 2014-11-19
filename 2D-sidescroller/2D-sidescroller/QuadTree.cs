using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_sidescroller
{
	class QuadTree
	{
		private static const int QUAD = 4;
		private static const int UL = 0;
		private static const int UR = 1;
		private static const int LL = 2;
		private static const int LR = 3;

		private QuadTree[] Children = null;

		private BoundingBox BB;

		public HashSet<IBoundingBoxHolder> Data = new HashSet<IBoundingBoxHolder>();

		public QuadTree(float width, float height)
		{
			BB = new BoundingBox(new Vector2(width / 2f, height / 2f), new Vector2(width, height));
		}

		private QuadTree(Vector2 pos, Vector2 size)
		{
			BB = new BoundingBox(pos, size);
		}

		private bool IsLeaf
		{
			get
			{
				return Children == null;
			}
		}

		public void Divide()
		{
			if (IsLeaf)
			{
				Children = new QuadTree[QUAD];
				
				Vector2 childSize = BB.Size / 2f;
				Vector2 step = childSize / 2f;
				Children[UL] = new QuadTree(BB.Pos + new Vector2(-step.X, step.Y), childSize);
				Children[UR] = new QuadTree(BB.Pos + new Vector2(step.X, step.Y), childSize);
				Children[LL] = new QuadTree(BB.Pos + new Vector2(-step.X, -step.Y), childSize);
				Children[LR] = new QuadTree(BB.Pos + new Vector2(step.X, -step.Y), childSize);
			}
			else
			{
				for (int i = 0; i < Children.Length; i++)
				{
					Children[i].Divide();
				}
			}
		}

		public void Add(IBoundingBoxHolder data)
		{
			if (IsLeaf)
			{
				return;
			}

			BoundingBox bb = data.BB;
			foreach (QuadTree child in Children)
			{
				if (child.Intersects(bb))
				{
					child.Data.Add(data);
					child.Add(data);
				}
			}
		}

		public HashSet<IBoundingBoxHolder> Query(BoundingBox query)
		{
			if (IsLeaf)
			{
				return Data;
			}

			HashSet<IBoundingBoxHolder> result = new HashSet<IBoundingBoxHolder>();
			foreach (QuadTree child in Children)
			{
				if (child.Intersects(query))
				{
					foreach(IBoundingBoxHolder holder in child.Query(query))
					{
						if (!result.Contains(holder) && query.Intersects(holder.BB))
						{
							result.Add(holder);
						}
					}
				}
			}
			return result;
		}

		protected bool Intersects(BoundingBox other)
		{
			return BB.Intersects(other);
		}
	}
}
