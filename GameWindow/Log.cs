using System;
using System.Drawing;
using Raylib_cs;
using System.Numerics;

namespace Movement
{
	class Log : MoverNode
	{
	
        

        public Log() : base("resources/log.png")
		{
			Position = new Vector2(0, 0);
		}
		

		public override void Update(float deltaTime)
		{
			//Move(deltaTime);
			//WrapEdges();
			//BounceEdges();
			
			if (Position.Y < 710) // een && of || gebruiken om meerdere checks in conditie
			{// bool voor stappelen
	 			Position.Y += deltaTime * 100;
		
			}
		}
    	/*TODO:
		Laneswitch function, checks if colliding and checks if lane is free.
			then switch lanes
		*/
		
	}
}