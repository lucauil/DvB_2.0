using System; // Console
using System.Drawing;
using System.Numerics; // Vector2
using Raylib_cs; // Color

namespace Movement 
{
    class River : SpriteNode
    {
        public River() : base("resources/river.png")
        {
			
		    Position = new Vector2(Settings.ScreenSize.X / 2, Settings.ScreenSize.Y / 2);
            
		}
	}
}