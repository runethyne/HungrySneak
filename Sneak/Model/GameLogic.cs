using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Sneak
{
    internal class GameLogic
    {
       
        public int[,] MapData = new int[Settings.windowWidth, Settings.windowHeigth];
        public HungrySnake hongrySneak {  get; set; }
        public IntVector2 dirr = new IntVector2(1, 0);
        public IntVector2 lastdir = new IntVector2(1, 0);
        public IntVector2 nutPosition ;
        public int level = 1;
        public int nutsMod = 1;
        
        private int gameplayDelay;
        private string screenText;
        internal int speedMod = 0;

        internal void tryMove(int x, int y)
        {
            //блок смены направления по горизонтали или по вертикали
            //не самое чистое решение, но у меня кончилось кофе
            
            if ((lastdir.x == 0 && x == 0)
                || (lastdir.y == 0 && y == 0))
            { 
                return; 
            }

            dirr = new IntVector2(x, y);
        }
        internal void placeNut()
        {
            Random rnd = new Random();

            List<IntVector2> freePlaces = new List<IntVector2>();
            for (var w = 1; w < Settings.windowWidth-1; w++)
                for (var h = 1; h < Settings.windowHeigth-1; h++)
                    if (MapData[w, h] == 0)
                        freePlaces.Add(new IntVector2(w, h));

            nutPosition = freePlaces[rnd.Next(freePlaces.Count)];
        }

        public GameLogic() 
        {
            gameplayDelay = 10;
            screenText = "LEVEL " + level;
            hongrySneak = new HungrySnake();

            hongrySneak.addSegment(Settings.StartPoint);
            hongrySneak.addSegment(new IntVector2(11, 10));
            hongrySneak.addSegment(new IntVector2(12, 10));
            dirr = new IntVector2(1, 0);

        }

        private void gameWin()
        {
            level++;
            gameplayDelay = 10;
            screenText = "LEVEL " + level;
            speedMod = Math.Min(speedMod+10, 100);

            hongrySneak = new HungrySnake();
            hongrySneak.addSegment(Settings.StartPoint);
            hongrySneak.addSegment(new IntVector2(11, 10));
            hongrySneak.addSegment(new IntVector2(12, 10));
            dirr = new IntVector2(1, 0);
        }
        private void gameLose()
        {
            gameplayDelay = 1000;
            screenText = "YOU DIE";
            hongrySneak = null;
            
        }

        public string getScreenText()
        {
            return gameplayDelay > 0 ? screenText : "";
        }

        internal void update()
        {
            
            if (gameplayDelay > 0 || hongrySneak == null)
            {
                gameplayDelay--;
                if (gameplayDelay == 0)
                {
                    dirr = new IntVector2(1, 0);
                    Console.Clear();
                }
                else
                {
                    return;
                }
                
            }

            MapData = new int[Settings.windowWidth, Settings.windowHeigth];
            lastdir = dirr;


            //реализуем механику съеденого хвоста
            foreach (IntVector2 coordsSneak in hongrySneak.segments)
            {
                

                if (hongrySneak.HeadCoord.x + dirr.x == coordsSneak.x && hongrySneak.HeadCoord.y + dirr.y == coordsSneak.y)
                {
                    //отрубаем хвостик
                    while (
                        hongrySneak.segments.Peek().x != coordsSneak.x
                        || hongrySneak.segments.Peek().y != coordsSneak.y
                        )

                        hongrySneak.segments.Dequeue();

                    break;
                }
            }

            //проверям вкусняху и двигаемся
            //если дошли до края - магически проходим через стену
            int newX = hongrySneak.HeadCoord.x + dirr.x;
            int newy = hongrySneak.HeadCoord.y + dirr.y;
            if (newX == 0)
            {
                gameLose();
                return;
                //newX = Settings.windowWidth-2;
            }
            if (newX == Settings.windowWidth-1)
            {
                gameLose();
                return;
                //newX = 1;
            }
            if (newy == 0)
            {
                gameLose();
                return;
                // newy = Settings.windowHeigth - 2;
            }
            if (newy == Settings.windowHeigth - 1)
            {
                gameLose();
                return;
                // newy = 1;
            }

            if (nutPosition.x == newX
                && nutPosition.y == newy)
            {
                hongrySneak.addSegment(new IntVector2(newX, newy));
                nutPosition.x = 0;
                nutPosition.y = 0;

                //проверяем победу
                if (hongrySneak.segments.Count - 3 >= nutsMod + level * nutsMod)
                {
                    gameWin();
                    return;
                }
                   
            }
            else
            {
                hongrySneak.moveHead(new IntVector2(newX, newy));
            }
                    


            //разместим змею
            foreach (IntVector2 coordsSneak in hongrySneak.segments)
            {
                MapData[coordsSneak.x, coordsSneak.y] = 1;
            }

            //разместим вкусняху (что едят змеи?)
            if (nutPosition.x == 0 && nutPosition.y == 0) //если координаты по нулям - знач вкусняхи нет
            {
                placeNut();
            }
            MapData[nutPosition.x, nutPosition.y] = 2;


        }


    }
}