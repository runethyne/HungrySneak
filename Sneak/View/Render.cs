using System;

namespace Sneak
{
    internal class Render
    {
        internal GameLogic gameLogic;
        private char symbol_empty = ' ';
        private char symbol_body = '■';
        private char symbol_head = '¤';
        private char symbol_wall = '*';
        private char symbol_nut = 'O';

        public void rend()
        {

            for (var w = 0; w < Settings.windowWidth; w++)
                for (var h = 0; h < Settings.windowHeigth; h++)
                {
                    if (gameLogic.MapData[w, h] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(w*2, h);
                        Console.Write(symbol_empty);
                    }
                    if (gameLogic.MapData[w, h] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(w*2, h);
                        if (gameLogic.hongrySneak.HeadCoord.x == w && gameLogic.hongrySneak.HeadCoord.y == h)
                            Console.Write(symbol_head);
                        else
                            Console.Write(symbol_body);
                    }
                    if (gameLogic.MapData[w, h] == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(w*2, h);
                        Console.Write(symbol_nut);
                        
                    }

                    if (w == 0 || w == Settings.windowWidth-1 || h == 0 || h == Settings.windowHeigth-1)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(w*2, h);
                        Console.Write(symbol_wall);
                    }
                }
            Console.SetCursorPosition(0, Settings.windowHeigth);
            Console.WriteLine($"Текущие координаты: X {gameLogic.hongrySneak.HeadCoord.x} Y {gameLogic.hongrySneak.HeadCoord.y} -------");
            Console.CursorVisible = false;

        }

    }
}