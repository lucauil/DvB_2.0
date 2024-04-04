using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Movement
{
	class GameScene : SceneNode
	{
		// private fields
		private Player player;
		private River river;
		// private Enemy enemy;
		
		
		 List<Arrow> arrows;
		 List<Log> logs;
		 List<Enemy> enemies;
		private GameOver gameover;
		private GameClear gameclear;
		
		
		private Column column = new Column();
 
		int speed = 200;
		bool GameIsOver;
		
		Random rand = new Random();

		private float arrowTimer = 0.0f;
		private float zTimer = 0.0f;
		private float logTimer = 0.0f;

		// constructor + call base constructors
		public GameScene(String t) : base(t)
		{
			arrows = new List<Arrow>();
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
			// loop door lijst met lazers
			// check distance met player
			for (int i = logs.Count-1; i >= 0; i--)
			{
				if (CalculateDistance(logs[i].Position, player.Position) < 90) //navragen hoe ik onderscheid kan maken tussen de x en y positie
				{
					
					// RemoveChild(logs[i]);
					// logs.RemoveAt(i);
					player.Damage(1);
				}

				// if (CalculateDistance(logs[i].Position, player.Position) < 200) //navragen hoe ik onderscheid kan maken tussen de x en y positie
				// {
					
					
				// 	if(player.Position.X < log.Position.X)
				// 	{
				// 		player.Position.X = log.Position.X + 20;
				// 	}
				// 	if(player.Position.X > log.Position.X)
				// 	{
				// 		player.Position.X = log.Position.X + 20;
				// 	}
				//}
			}

			for (int a = arrows.Count-1; a >= 0; a--) //samenvoegen met vijand en speler collision
			{
				for (int l = logs.Count-1; l >= 0; l--)
				{
					if (CalculateDistance(logs[l].Position, arrows[a].Position) < 40)
					{
						
						
						RemoveChild(arrows[a]);
						arrows.RemoveAt(a);
						RemoveChild(logs[l]);
						logs.RemoveAt(l);
					}
				}
			}

			for (int a = arrows.Count-1; a >= 0; a--) 
			{
				for (int e = enemies.Count-1; e >= 0; e--) 
				{
					if (CalculateDistance(enemies[e].Position, arrows[a].Position) < 40)
					{
						// Console.WriteLine($"enemy {e}");
						// Console.WriteLine($"arrow {a}");
						RemoveChild(enemies[e]);
						enemies.RemoveAt(e);
						// RemoveChild(arrows[a]);
						// if (arrows.Count > 0) 
						// {
						// 	arrows.RemoveAt(a);
						// }
					}
				}
			}

			// bit of A cleanup hack. Ew!
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

		public void Logplacer(float deltaTime) //zo snel als mogelijk doen
		{
			


			if (GameIsOver) return;
			logTimer += deltaTime;
			if (logTimer > 2.0f)
			{
				// Log l = enemy.Shoot();
				// if (l != null)
				// {
				// 	AddChild(l);
				// 	logs.Add(l);
				// }
				//logTimer = 0.0f;
				// size_t numerOfBeavers = enemies.size(); nog mee bezig gaan
				// size_t randomBeaverIndex = rand()%numerOfBeavers;

				// Treelog* log = new Treelog();
				// //float xpos = (rand()%10) * 128 + 64;
				// float xpos = enemies[randomBeaverIndex]->position.x;
				// log.position = Vector2(xpos, 50);
				// addChild(log);
				// logs.Add(l);
				// logTimer = 0.0f;
			}
		}

		public void PlanetRemover() //kan nog van pas komen
		{
			// for (int i = 0; i < planets.Count; i++)
			// {
			// 	RemoveChild(planets[i]);
			// }
			// planets.Clear();
		}

		public void Gamestate()//komt later verder
		{
            // int numberoflogsonthebottom = 0;
            // std::vector<int> logpos = std::vector<int>();
            // logpos.resize(10);
            // for (size_t i = 0; i < xpositions.size(); i++)
            // {
            // 	for (size_t tl = 0; tl < treelogs.size(); tl++)
            // 	{
            // 		if (treelogs[tl]->position.x == xpositions[i] && 
            // 			(treelogs[tl]->position.y >= SHEIGHT - (treelogs[tl]->sprite()->height() / 2))
            // 		)
            // 		{
            // 			// treelog on this position.
            // 			// possibly game over
            // 			// numberoflogsonthebottom++;
            // 			logpos[i] = 1;
            // 		}
            // 		else
            // 		{
            // 			// no treelog on this position
            // 			// definitely not game over
            // 		}
            // 	}


            // 	int sum = 0;
            // 	for (size_t i = 0; i < logpos.size(); i++)
            // 	{
            // 		sum += logpos[i];
            // 	}
            // 	if (logs.count == 10)
            // 	{
            // 		gameIsOver = true;
            // 		gameText->message("GAME OVER",RED);
            // 	}

            
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
			enemies = new List<Enemy>();
			
			for (int i = 0; i < 10; i++) 
			{
				Enemy e = new Enemy();
				float xpos = i * 128 + 64;
				// e.Position.X = rand.Next(0, 1200);
				e.Position.X = xpos;
				enemies.Add(e);
				AddChild(e);
			}
		}

		void Restart()
		{
			RemoveChild(player);
			RemoveChild(gameclear);
			RemoveChild(gameover);
			PlanetRemover();
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