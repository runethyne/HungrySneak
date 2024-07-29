using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace Sneak
{

    internal class HungrySnake
    {
        public Queue<IntVector2> segments = new Queue<IntVector2>();
        public IntVector2 HeadCoord;

        public void addSegment(IntVector2 coord)
        {
            segments.Enqueue(coord);
            HeadCoord = coord;
        }
        public void moveHead(IntVector2 coord)
        {
            segments.Enqueue(coord);
            segments.Dequeue();
            HeadCoord = coord;
        }
    }
}