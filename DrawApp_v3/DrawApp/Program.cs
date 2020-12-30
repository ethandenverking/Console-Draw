using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int windowWidth = Console.WindowWidth;
            int windowHeight = 29;
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(windowWidth, windowHeight);

            Menu menu = new Menu();
            Palette palette = new Palette();
            Canvas canvas = new Canvas();

            Cursor cursor = new Cursor(canvas, palette, menu);
        }
    }
}
