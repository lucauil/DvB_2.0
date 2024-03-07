using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

namespace Movement
{
	class GameScene : SceneNode
	{
		// private fields
		private Player player;
		private Log log;
		private River river;
		private Enemy enemy
		;
		
		 List<Arrow> arrows;
		 List<Log> logs;
		private GameOver gameover;
		private GameClear gameclear;
		private PlayerHP playerhp;
		private EnemyHP enemyhp;
		private Column column = new Column();
 
		int speed = 200;
	

		// constructor + call base constructors
		public GameScene(String t) : base(t)
		{
			
			Restart();
		}

		// Update is called every frame
		public override void Update(float deltaTime)
		{
			if (!player.IsAlive()) 
			{
				AddChild(gameover);
				if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
				{
					RemoveChild(player);
					RemoveChild(enemy);
					RemoveChild(playerhp);
					RemoveChild(enemyhp);
					RemoveChild(gameover);
					PlanetRemover();
					Restart();
				}
				return;
			}

			if (!enemy.IsAlive()) 
			{
				AddChild(gameclear);
				if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
				{
					RemoveChild(player);
					RemoveChild(enemy);
					RemoveChild(playerhp);
					RemoveChild(enemyhp);
					RemoveChild(gameclear);
					PlanetRemover();
					Restart();
				}
				return;
			}

			base.Update(deltaTime);
			HandleInput(deltaTime);
			Zmove(deltaTime);
			column.DrawGrid();
			InitBeavers();

			// loop door lijst met lazers
			// check distance met player
			for (int i = logs.Count-1; i >= 0; i--)
			{
				if (CalculateDistance(logs[i].Position, player.Position) < 40)
				{
					
					RemoveChild(logs[i]);
					logs.RemoveAt(i);
					player.Damage(10);
				}
			}

			for (int i = arrows.Count-1; i >= 0; i--)
			{
				if (CalculateDistance(arrows[i].Position, enemy.Position) < 40)
				{
					Console.WriteLine("boom");
					RemoveChild(arrows[i]);
					arrows.RemoveAt(i);
					enemy.Damage(10);
				}
			}

			// for (int i = arrows.Count-1; i >= 0; i--)
			// {
			// 	if (CalculateDistance(arrows[i].Position, log.Position) < 40)
			// 	{
			// 		Console.WriteLine("boom");
			// 		RemoveChild(arrows[i]);
			// 		arrows.RemoveAt(i);
			// 		log.Damage(10); 
			// 	}
			// }
			playerhp.Scale.X = player.health / 10;
			
		}

		public void Drawcollum()
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
		private void HandleInput(float deltaTime)
		{
			
			
			if (Raylib.IsKeyDown(KeyboardKey.KEY_A) ||Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
			{
				player.Position.X -= speed*deltaTime;
			}
			if (Raylib.IsKeyDown(KeyboardKey.KEY_D) ||Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
			{

			player.Position.X += speed*deltaTime;
			}

			if (player.Position.X > Settings.ScreenSize.X)
			{
			player.Position.X = Settings.ScreenSize.X;
			}

			if (player.Position.X > Settings.ScreenSize.X)
			{
			player.Position.X = Settings.ScreenSize.X;
			}

			if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
			{
				Arrow a = player.Shoot();
				if (a != null)
				{
					AddChild(a);
					arrows.Add(a);
				}
			}

			if (Raylib.IsKeyPressed(KeyboardKey.KEY_L))
			{
			 	Log l = enemy.Shoot();
			 	if (l != null)
			 	{
			 		AddChild(l);
			 		logs.Add(l);
			 	}
			 }
		
		}

		private void Zmove(float deltaTime)
		{
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_Z)) 
			{
				for (int i = -12; i < 13; i++)
				{
					Arrow z = player.Zmove();
			
					AddChild(z);
					arrows.Add(z);
					Vector2 pp = player.Position;
	
					z.Position = new Vector2(pp.X - (i * 20), pp.Y);
				}	
			
			}			
		}

		public void logplacer() //komt later
		{
			// planets = new List<Planet>();
			// for (int i = 0; i < 15; i++)
			// {
			// 	Planet p = new Planet();
			// 	planets.Add(p);
			// 	AddChild(p);

			// 	p.Position.X = rand.Next(0, 1200);
			// 	p.Position.Y = rand.Next(0, 650);
			// }
			// if (Raylib.IsKeyPressed(KeyboardKey.KEY_KP_ENTER))
			// {
			//  	Log l = enemy.Shoot();
			//  	if (l != null)
			//  	{
			//  		AddChild(l);
			//  		logs.Add(l);
			//  	}
			//  }
		}

		public void PlanetRemover() //kan nog van pas komen
		{
			// for (int i = 0; i < planets.Count; i++)
			// {
			// 	RemoveChild(planets[i]);
			// }
			// planets.Clear();
		}


		private float CalculateDistance(Vector2 a, Vector2 b)
		{
			return Vector2.Distance(a, b);
		}

		void Restart()
		{
			//restart the game
			logplacer();
			river = new River();
			AddChild(river);
			player = new Player();
			AddChild(player);
			enemy = new Enemy();
			AddChild(enemy);
			arrows = new List<Arrow>();
			logs = new List<Log>();
			gameover = new GameOver();
			gameclear = new GameClear();
			playerhp = new PlayerHP();
			AddChild(playerhp);
			enemyhp = new EnemyHP();
			AddChild(enemyhp);
		}
		private void InitBeavers()
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
		}
	}
} // namespace