using System; // Console
using System.Drawing;
using System.Numerics; // Vector2
using Raylib_cs; // Color

namespace Movement
{
	class PlayerHP : SpriteNode
	{
		public PlayerHP() : base("")
		{
			Position = new Vector2(100, 50);
			//Pivot = new Vector2(0, 0);
		}

		void Update()
		{

		}
	} 

}