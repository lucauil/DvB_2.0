using System.Numerics;

namespace Movement
{
	abstract class MoverNode : SpriteNode
	{
		// public fields (!!!)
		public Vector2 Velocity;
		public Vector2 Acceleration;
		protected float maxSpeed;
		private float mass; // OK, let's keep this one private

		// public Vector2 Velocity { 
		// 	get { return velocity; }
		// 	set { velocity = value; }
		// }
		// public Vector2 Acceleration { 
		// 	get { return acceleration; }
		// 	set { acceleration = value; }
		// }
		public float Mass { 
			get { return mass; }
			private set { mass = value; }
		}

		// constructor
		protected MoverNode(string title) : base(title)
		{
			Velocity = new Vector2(0, 0);
			Acceleration = new Vector2(0, 0);
			Mass = 1.0f;
			maxSpeed = 10.0f;
		}

		public override void Update(float deltaTime)
		{
			// override in your subclass
		}

		// Protected methods to be called from subclass
		protected void Move(float deltaTime)
		{
			// Motion 101. Apply the rules.
		
			Velocity += Acceleration * deltaTime;
			Position += Velocity * deltaTime;
			// Reset acceleration
			Acceleration = new Vector2(0,0);
			Velocity *= 0.9995f;
		}

		protected void AddForce(Vector2 force)
		{
			Acceleration += force / Mass;
		}

		
		protected void Limit()
		{
			if (Velocity.Length() > maxSpeed)
			{
				Acceleration = new Vector2();
			}
		}

		protected void BounceEdges()
		{
			float scr_width = Settings.ScreenSize.X;
			float scr_height = Settings.ScreenSize.Y;
			float spr_width = TextureSize.X;
			float spr_heigth = TextureSize.Y;

			// TODO implement...
			if (Position.X > scr_width - spr_width / 2)
			{
				Position.X = scr_width - spr_width / 2;
				Velocity.X *= -0.9f;
			}
			if (Position.X < 0 + spr_width / 2)
			{
				Position.X = 0 + spr_width / 2;
				Velocity.X *= -0.9f;
			}
			if (Position.Y > scr_height - spr_heigth / 2)
			{
				Position.Y = scr_height - spr_heigth / 2;
				Velocity.Y *= -0.9f;
			}
			if (Position.Y < 0 + spr_heigth / 2)
			{
				Position.Y = 0 + spr_heigth / 2;
				Velocity.Y *= -0.9f;
			}
		}

		protected void WrapEdges()
		{
			float scr_width = Settings.ScreenSize.X;
			float scr_height = Settings.ScreenSize.Y;
			float spr_width = TextureSize.X;
			float spr_heigth = TextureSize.Y;

			// TODO implement...
			if (Position.X > scr_width)
			{
				Position.X = 0;
			}
			if (Position.X < 0)
			{
				Position.X = scr_width;
			}
			if (Position.Y > scr_height)
			{
				Position.Y = 0;
			}
			if (Position.Y < 0)
			{
				Position.Y = scr_height;
			}
		}

	}
}
