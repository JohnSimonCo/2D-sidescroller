using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace _2D_sidescroller
{
	class Player : Entity
	{
		Player(Vector2 pos, Vector2 size, uint texture)
			: base(pos, size, texture)
		{

		}
	}
}