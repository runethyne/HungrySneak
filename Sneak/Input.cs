using System.Collections.Generic;
using System;

namespace Sneak
{
    internal class Input
    {
        public GameLogic gameLogic;
        public void Update()
        {
            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        gameLogic.tryMove(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        gameLogic.tryMove(0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        gameLogic.tryMove(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        gameLogic.tryMove(1, 0);
                        break;
                }
            }

        }
    }
}
