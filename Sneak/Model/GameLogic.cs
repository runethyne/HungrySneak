using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Sneak
{
    internal class GameLogic
    {
       
        public int[,] MapData;
        public HungrySnake hongrySneak {  get; set; }
        public IntVector2 dirr = new IntVector2(1, 0);
        public IntVector2 lastdir = new IntVector2(1, 0);
        public IntVector2 nutPosition ;

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

        internal void update()
        {
            
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
                newX = Settings.windowWidth-2;
            }
            if (newX == Settings.windowWidth-1)
            {
                newX = 1;
            }
            if (newy == 0)
            {
                newy = Settings.windowHeigth - 2;
            }
            if (newy == Settings.windowHeigth - 1)
            {
                newy = 1;
            }

            if (nutPosition.x == newX
                && nutPosition.y == newy)
            {
                hongrySneak.addSegment(new IntVector2(newX, newy));
                nutPosition.x = 0;
                nutPosition.y = 0;
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