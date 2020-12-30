using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    /// <summary>
    /// A class representing a pixel on the drawing grid of the canvas
    /// </summary>
    class Pixel
    {
        public int PosX { get; }
        public int PosY { get; }
        public int AbsoluteX { get; }
        public int AbsoluteY { get; }
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Constructs a Pixel object with a posX, posY, absX, absY, and a ConsoleColor
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="absX"></param>
        /// <param name="absY"></param>
        /// <param name="color"></param>
        public Pixel(int posX, int posY, int absX, int absY, ConsoleColor color)
        {
            PosX = posX;
            PosY = posY;
            AbsoluteX = absX;
            AbsoluteY = absY;
            Color = color;
        }

    }
}
