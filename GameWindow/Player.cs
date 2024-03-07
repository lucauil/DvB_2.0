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
	class Player : SpriteNode
	{
		// your private fields here (rotSpeed, thrustForce)
		

		public int health;
		public int MaxHealth;
		// constructor + call base constructor
		public Player() : base("resources/decidueye.png")
		{
			
			Position = new Vector2(600, 590);
			
			MaxHealth = 3;
			health = MaxHealth;
			Rotation = 11;
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
			Console.WriteLine("P1_HP: " + health);
		}

		public Arrow Shoot()
		{
			Arrow a = new Arrow();
			a.Position.X = this.Position.X + (float)Math.Cos(Rotation);
			a.Position.Y = this.Position.Y + (float)Math.Sin(Rotation);
			a.Rotation = Rotation;
			a.Velocity = new Vector2(1000 * (float)Math.Cos(Rotation), 1000 * (float)Math.Sin(Rotation));

			return a;
		}

		public Arrow Zmove()
		{
			Arrow z = new Arrow();
			z.Position.X = this.Position.X + (float)Math.Cos(Rotation);
			z.Position.Y = this.Position.Y + (float)Math.Sin(Rotation);
			z.Rotation = Rotation;
			z.Velocity = new Vector2(1000 * (float)Math.Cos(Rotation), 1000 * (float)Math.Sin(Rotation));
			return z;
		}

	}
}
