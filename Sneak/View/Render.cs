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
            string screenText = gameLogic.getScreenText();
            if (screenText.Length > 0)
            {
                Console.SetCursorPosition((gameLogic.MapData.GetLength(0) / 2 - screenText.Length / 2 + 1) * 2, gameLogic.MapData.GetLength(1) / 2 - 1);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(screenText);


            }
            else
            {
                for (var w = 0; w < Settings.windowWidth; w++)
                    for (var h = 0; h < Settings.windowHeigth; h++)
                    {
                        if (gameLogic.MapData[w, h] == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(w * 2, h);
                            Console.Write(symbol_empty);
                        }
                        if (gameLogic.MapData[w, h] == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(w * 2, h);
                            if (gameLogic.hongrySneak.HeadCoord.x == w && gameLogic.hongrySneak.HeadCoord.y == h)
                                Console.Write(symbol_head);
                            else
                                Console.Write(symbol_body);
                        }
                        if (gameLogic.MapData[w, h] == 2)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(w * 2, h);
                            Console.Write(symbol_nut);

                        }

                        if (w == 0 || w == Settings.windowWidth - 1 || h == 0 || h == Settings.windowHeigth - 1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(w * 2, h);
                            Console.Write(symbol_wall);
                        }
                    }
            }
            if (gameLogic.hongrySneak != null)
            {
                Console.SetCursorPosition(0, Settings.windowHeigth);
                Console.WriteLine($" Уровень {gameLogic.level} - Сожрано {gameLogic.hongrySneak.segments.Count - 3} из {gameLogic.nutsMod + gameLogic.nutsMod * gameLogic.level}");
                Console.CursorVisible = false;
            }
            

            

        }

    }
}