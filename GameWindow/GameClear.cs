using System;
using System.Drawing;
using Raylib_cs;
using System.Numerics;
using System.Configuration;

namespace Movement
{
	class GameClear : MoverNode
	{
		public GameClear() : base("resources/gameclear.png")
		{
			Position = new Vector2(Settings.ScreenSize.X / 2, Settings.ScreenSize.Y / 2);
		}

		public override void Update(float deltaTime)
		{
			
		}
	}
}