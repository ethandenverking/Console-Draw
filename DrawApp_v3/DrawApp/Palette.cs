using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    class Palette
    {
        public int MarginTop { get; }
        public int MarginLeft { get; }
        public int SizeWidth { get; }
        public int SizeHeight { get; }
        public ConsoleColor[,] Swatch { get; }

        public Palette()
        {
            MarginLeft = 6;
            MarginTop = 9;

            Swatch = new ConsoleColor[8,2]
            {
                {ConsoleColor.DarkBlue,     ConsoleColor.Blue },
                {ConsoleColor.DarkCyan,     ConsoleColor.Cyan },
                {ConsoleColor.DarkGreen,    ConsoleColor.Green },
                {ConsoleColor.DarkYellow,   ConsoleColor.Yellow },
                {ConsoleColor.DarkRed,      ConsoleColor.Red },
                {ConsoleColor.DarkMagenta,  ConsoleColor.Magenta },
                {ConsoleColor.White,        ConsoleColor.Gray },
                {ConsoleColor.Black,        ConsoleColor.DarkGray },
            };
            Draw();
        }

        public void Draw()
        {
            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition( MarginLeft, MarginTop + i);
                Console.BackgroundColor = Swatch[i / 2,0];
                Console.Write("    ");
                Console.BackgroundColor = Swatch[i / 2,1];
                Console.Write("    ");
            }

            Console.ResetColor();
        }
    }
}
