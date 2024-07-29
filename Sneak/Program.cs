using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sneak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Settings.windowWidth*2, Settings.windowHeigth+2);

            GameLogic gameLogic = new GameLogic();
            HungrySnake hongrySneak = new HungrySnake();
            Render render = new Render();
            Input input = new Input();

            hongrySneak.addSegment(Settings.StartPoint);
            hongrySneak.addSegment(new IntVector2(11, 10));
            hongrySneak.addSegment(new IntVector2(12, 10));
           
            gameLogic.hongrySneak = hongrySneak;
            render.gameLogic = gameLogic;
            input.gameLogic = gameLogic;
            
            while (true)
            {
                input.Update();
                gameLogic.update();
                render.rend();

                Thread.Sleep(Settings.delay);
                
            }

        }
    }
}
