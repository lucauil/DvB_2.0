using System;
using System.Drawing;
using Raylib_cs;
using System.Numerics;

namespace Movement
{
	class GameOver : MoverNode
	{
		public GameOver() : base("resources/gameover.png")
		{
			Position = new Vector2(Settings.ScreenSize.X / 2, Settings.ScreenSize.Y / 2);
		}

		public override void Update(float deltaTime)
		{

		}
	}
}