using System;
using System.Collections.Generic;
using Raylib_cs;

namespace Movement
{
	class Game
	{
		// private fields
		private Core core;
		private MenuScene menuscene;
		private GameScene gamescene;

		// constructor
		public Game()
		{
			core = new Core("Decidueye vs Bibarel - 2.0");
			menuscene = new MenuScene("Decidueye vs Bibarel -  2.0");
			gamescene = new GameScene("Decidueye vs Bibarel -  2.0");
			
		}

		// public methods
		public void Play()
		{
			//int scene_id = 0;
			bool running = true;
			while (running)
			{
				// handle scene_id
				//if (Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT_BRACKET)) { scene_id--; }
				//if (Raylib.IsKeyReleased(KeyboardKey.KEY_RIGHT_BRACKET)) { scene_id++; }
				//if (scene_id <= 0) { scene_id = 0; }
				//if (scene_id >= scenes.Count) { scene_id = scenes.Count-1; }

				// run current scene
				SceneNode current = gamescene;
				running = core.Run(current);
			}

			Console.WriteLine("Thanks for playing!");
		}
	}
}
