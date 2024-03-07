using System;
using System.Drawing;
using Raylib_cs;
using System.Numerics;
using System.Dynamic;

namespace Movement
{
	class Arrow : MoverNode
	{
		public Arrow() : base("resources/arrow.png")
		{
			Position = new Vector2(0, 0);
		}

		public override void Update(float deltaTime)
		{
			Move(deltaTime);
			//WrapEdges();
			//BounceEdges();
		}
    }
}