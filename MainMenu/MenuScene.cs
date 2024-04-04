using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

namespace Movement
{
	class MenuScene : SceneNode
	{
		// private fields
		// private Core core;
		private List<MenuButton> menubuttons;
		// constructor + call base constructors
		public MenuScene(String t) : base(t)
		{
			MenuButtons();
		}

		// Update is called every frame
		public override void Update(float deltaTime)
		{
			Vector2 mousepos = Raylib.GetMousePosition();

			for (int i = 0; i < menubuttons.Count; i++)
			{
				if (!(mousepos.X >= menubuttons[i].Position.X - menubuttons[i].TextureSize.X / 2 && mousepos.X <= menubuttons[i].Position.X + menubuttons[i].TextureSize.X / 2 && mousepos.Y >= menubuttons[i].Position.Y - menubuttons[i].TextureSize.Y / 2 && mousepos.Y <= menubuttons[i].Position.Y + menubuttons[i].TextureSize.Y / 2))
				{
					continue;
				}
				if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
				{
					MenuButtonWork(i);
					Console.WriteLine("Works");
					return;
				}
			}
		}

		public void MenuButtons()
		{
			menubuttons = new List<MenuButton>();

			MenuButton start = new MenuButton();
			menubuttons.Add(start);
			AddChild(start);
			start.Position.X = 640;
			start.Position.Y = 300;
			start.Color = Raylib_cs.Color.ORANGE;
			// specify which scene you want to move to

			MenuButton quit = new MenuButton();
			menubuttons.Add(quit);
			AddChild(quit);
			quit.Position.X = 640;
			quit.Position.Y = 400;
			quit.Color = Raylib_cs.Color.BLUE;
		}

		private void MenuButtonWork(int i)
		{
			switch (i)
			{
				case 0:
					//SceneNode current = gamescene; try to somehow make this function
					break;

				case 1:

					break;
			}
		}
	}
} // namespace