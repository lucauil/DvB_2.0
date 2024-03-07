using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Movement
{
	internal class Column
	{
		public void DrawGrid()
		{
			int gridSize = 10;
			int cellSize = 128;
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

					if (row == gridSize / 2 && col == gridSize / 2)
					{
						Raylib.DrawRectangleRec(cellRect, Raylib_cs.Color.BLUE);
					}
					else
					{
						Raylib.DrawRectangleRec(cellRect, Raylib_cs.Color.BLUE);
					}

					// Draw lines to separate cells
					Raylib.DrawLine(cellX + cellSize, cellY, cellX + cellSize, cellY + cellSize, Raylib_cs.Color.BLACK);
					//Raylib.DrawLine(cellX, cellY + cellSize, cellX + cellSize, cellY + cellSize, Raylib_cs.Color.BLACK);
				}
			}
		}
	}
}
