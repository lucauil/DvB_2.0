using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Raylib_cs;

namespace Movement
{
	class GameScene : SceneNode
	{
		// private fields
		private Player player;
		private River river;
		private Enemy enemy;
		private GameOver gameover;
		private GameClear gameclear;
		
		
		 List<Arrow> arrows;
		 List<Log> logs;
		 List<Enemy> enemies;
		List<int> xpostions;
		
		private Column column = new Column();
 
		int speed = 200;
		bool GameIsOver;
		
		Random rand = new Random();

		private float arrowTimer = 0.0f;
		private float zTimer = 0.0f;
		private float logTimer = 0.0f;
        private List<int> xpositions;

        // constructor + call base constructors
        public GameScene(String t) : base(t)
		{
			
			Start();
		}

		// Update is called every frame
		public override void Update(float deltaTime)
		{
			

			

			
			Gamestate();
			base.Update(deltaTime);
			HandleInput(deltaTime);
			Zmove(deltaTime);
			column.DrawGrid();
			Collum();
			Logplacer(deltaTime);
			Collision();
		
		}

		public void Drawcollum() //later verder
		{
			int gridSize = 10;
			int cellSize = 140;
			int gridWidth = gridSize * cellSize;
			int gridHeight = gridSize * cellSize;

			int centerX = (Raylib.GetScreenWidth() - gridWidth) / 2;
			int centerY = (Raylib.GetScreenHeight() - gridHeight) / 2;

			for (int row = 0; row < gridSize; row++)
			{
				for (int col = 0; col < gridSize; col++)
				{
					int cellX = centerX + col * cellSize;
					int cellY = centerY + row * cellSize;

					Raylib_cs.Rectangle cellRect = new Raylib_cs.Rectangle(cellX, cellY, cellSize, cellSize);
					Raylib.DrawLine(cellX + cellSize, cellY, cellX + cellSize, cellY + cellSize, Raylib_cs.Color.BLACK);
					Raylib.DrawLine(cellX, cellY + cellSize, cellX + cellSize, cellY + cellSize, Raylib_cs.Color.BLACK);
				}
			}
		}

		public void Collision() //navragen bij rik of maik
		{
			List<Log> logsToRemove = new List<Log>();
			List<Arrow> arrowsToRemove = new List<Arrow>();
			
			
			// loop door lijst met lazers
			// check distance met player
			for (int l = logs.Count-1; l >= 0; l--)
			{
				if (CalculateDistance(logs[l].Position, player.Position) < 90) //navragen hoe ik onderscheid kan maken tussen de x en y positie
				{
						RemoveChild(logs[l]);
						logs.RemoveAt(l);
						player.Damage(1);
				}
			}

			// Example for collision with logs
				for (int a = arrows.Count - 1; a >= 0; a--)
				{
					for (int l = logs.Count - 1; l >= 0; l--)
					{
						if (CalculateDistance(arrows[a].Position, logs[l].Position) < 60)
						{
							// Mark for removal
							logsToRemove.Add(logs[l]);
							arrowsToRemove.Add(arrows[a]);
						}
					}
				}


				// Remove marked items after iteration
				foreach (var log in logsToRemove)
				{
					logs.Remove(log);
					RemoveChild(log); // Assuming RemoveChild is a method to remove from the game scene
				}

				foreach (var arrow in arrowsToRemove)
				{
					arrows.Remove(arrow);
					RemoveChild(arrow); // Assuming RemoveChild is a method to remove from the game scene
				}

				

			for (int a = arrows.Count - 1; a >= 0; a--)
			{
				for (int e = enemies.Count-1; e >= 0; e--) 
					{
						if (CalculateDistance(enemies[e].Position, arrows[a].Position) < 40)
						{
							RemoveChild(enemies[e]);
							enemies.RemoveAt(e);
						}
					}
			}

			// bit of A cleanup hack.
			for (int i = 0; i < arrows.Count; i++)
			{
				if (arrows[i].Position.Y < -100)
				{
					RemoveChild(arrows[i]);
					arrows.RemoveAt(i);
				}
			}

			
		}

		private void HandleInput(float deltaTime) 
		{
			if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
			{		
				Restart();
				Start();
			}
			
			if (GameIsOver) return;
			if (Raylib.IsKeyDown(KeyboardKey.KEY_A) ||Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
			{
				player.Position.X -= speed*deltaTime;
			}
			if (Raylib.IsKeyDown(KeyboardKey.KEY_D) ||Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
			{

			player.Position.X += speed*deltaTime;
			}

			if (player.Position.X > Settings.ScreenSize.X-60)
			{
			player.Position.X = Settings.ScreenSize.X-60;
			}

			if (player.Position.X < 60)
			{
			player.Position.X = 60;
			}


			arrowTimer += deltaTime;
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
			{
				if (arrowTimer > 0.2f)
				{
					Arrow a = player.Shoot();
					if (a != null)
					{
						AddChild(a);
						arrows.Add(a);
					}
					arrowTimer = 0.0f;
				}
				
			}

			
			
		}

		private void Zmove(float deltaTime) // klaar
		{
			// if (gameIsOver) return;
			zTimer += deltaTime;
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_Z)) 
			{
				if (zTimer > 5.0f)
				{
					for (int i = -12; i < 13; i++)
					{
						Arrow z = player.Zmove();
				
						AddChild(z);
						arrows.Add(z);
						Vector2 pp = player.Position;
		
						z.Position = new Vector2(pp.X - (i * 20), pp.Y);
					}
					zTimer = 0.0f;	
				}
			}			
		}

		public void Logplacer(float deltaTime) //na vragen hoe 1 stam per door 1 bever gegooid wordt 
		{



			if (GameIsOver) return;
			logTimer += deltaTime;
			if (logTimer > 2.0f)
			{
			
				
			

				
				int randomBeaverIndex = rand.Next(0, enemies.Count);

				for (int i = 0; i < 10; i++)
				{	
					
					if (rand.Next(0, 10) == 0)
					{
						Log l = new Log();
						float xpos = enemies[randomBeaverIndex].Position.X;
						// float xpos = i * 128 + 64;
						l.Position.X = xpos;
						l.Position.Y = 80;
						
						if (i < logs.Count)
						{
							// AddChild(l);
							// logs.Add(l);
							
						}
						else
						{
							
						}
						AddChild(l);
						logs.Add(l);
					}
				}
				logTimer = 0.0f;
			}
		}




		public void Gamestate()
		{
			// Check if there are 10 logs in a row at a specified position
			if (CheckLogsInRow())
			{
				GameIsOver = true;
				AddChild(gameover);
				return; // Exit the method early to avoid further checks
			}

			// Existing game state checks
			// if (logs.Count == 10)
			// {
			// 	GameIsOver = true;
			// 	AddChild(gameover);
			// 	return; // Exit the method early to avoid further checks
			// }

			if (!player.IsAlive()) 
			{
				AddChild(gameover);
				GameIsOver = true;
			}

			if (enemies.Count == 0) //klaar
			{
				AddChild(gameclear);
				GameIsOver = true;
			}
		}

		private bool CheckLogsInRow()
		{
			// Define the position where the logs should be in a row
			float targetX = 700; // Replace with the actual X-coordinate
			float tolerance = 10; // Tolerance for the logs' positions

			// Count the number of logs within the tolerance of the target X-coordinate
			int logsInRow = 0;
			foreach (var log in logs)
			{
				if (Math.Abs(log.Position.X - targetX) <= tolerance)
				{
					logsInRow++;
				}
			}

			// Check if there are 10 logs in a row
			return logsInRow >= 10;
		}

		private float CalculateDistance(Vector2 a, Vector2 b)
		{
			return Vector2.Distance(a, b);
		}

		void Start()
		{
			//start the game
			GameIsOver = false;
			river = new River();
			AddChild(river);
			player = new Player();
			AddChild(player);
			arrows = new List<Arrow>();
			logs = new List<Log>();
			enemies = new List<Enemy>();
			gameover = new GameOver();
			gameclear = new GameClear();
			
			
			for (int i = 0; i < 10; i++) 
			{
				Enemy e = new Enemy();
				float xpos = i * 128 + 64;
				// e.Position.X = rand.Next(0, 1200);
				e.Position.X = xpos;
				enemies.Add(e);
				AddChild(e);
			}

			xpositions = new List<int>(10);
			for (int i = 0; i < 10; i++)
			{
				int xpos = i * 128 + 64;
				
			}
		}

		void Restart()
		{
			RemoveChild(player);
			RemoveChild(gameclear);
			RemoveChild(gameover);
		}

		private void Collum() //navragen bij rik
		{
			int gridSize = 10;
			int cellSize = 128;
			int gridWidth = gridSize * cellSize;
			int gridHeight = gridSize * cellSize;

			int centerX = (Raylib.GetScreenWidth() - gridWidth) / 2;
			int centerY = (Raylib.GetScreenHeight() - gridHeight) / 2;

			// Vector2 CalculatePosition(int row, int col)
			// {
			// 	int x = centerX + col * cellSize / 2;
			// 	int y = centerX + col * cellSize / 2;
			// 	return new Vector2(x, y);
			// }

			// AddChild
			//Ypos = height


		}

		
	
	}
} // namespace
