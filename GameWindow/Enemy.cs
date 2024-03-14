using System; // Console
using System.Drawing;
using System.Numerics; // Vector2
using Raylib_cs; // Color

/*
In this class, we have the properties:

- Vector2  Position
- float    Rotation
- Vector2  Scale

- Vector2 TextureSize
- Vector2 Pivot
- Color Color

Methods:

- AddChild(Node child)
- RemoveChild(Node child, bool keepAlive = false)
*/

namespace Movement
{
	class Enemy : MoverNode
	{
		// your private fields here (rotSpeed, thrustForce)
	

		public int health;
		public int MaxHealth;

		// constructor + call base constructor
		public Enemy() : base("resources/bibarel.png")
		{
			
			Position = new Vector2(50, 50);
			MaxHealth = 1;
			health = MaxHealth;
			Rotation = 1.55;
		}

		// Update is called every frame
		public override void Update(float deltaTime)
		{
			//Move(deltaTime);
			//WrapEdges();
			//BounceEdges();
		}

		// your own private methods

		

	

		

		public void Damage(int amount)
		{
			health -= amount;
		}

		public bool IsAlive()
		{
			if (health <= 0)
			{
				return false;
			}
			return true;
		}

		public void ShowHealth()
		{
			Console.WriteLine("EN_HP: " + health);
		}

		public Log Shoot()
		{
			Log l = new Log();
			l.Position.X = this.Position.X + (float)Math.Cos(Rotation);
			l.Position.Y = this.Position.Y + (float)Math.Sin(Rotation);
			l.Rotation = Rotation;
			//l.Velocity = new Vector2(1000 * (float)Math.Cos(Rotation), 1500 * (float)Math.Sin(Rotation));

			return l;
		}
		//public Circle();

	}
}