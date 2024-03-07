using System; // Console
using System.Drawing;
using System.Numerics; // Vector2
using Raylib_cs; // Color

namespace Movement
{
	class EnemyHP : SpriteNode
	{
		public EnemyHP() : base("")
		{
			Position = new Vector2(100, 70);
			//Pivot = new Vector2(0, 0);
		}

		void Update()
		{

		}
	} 

}