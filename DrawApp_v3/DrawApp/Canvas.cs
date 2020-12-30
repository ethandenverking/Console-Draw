using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    /// <summary>
    /// A class that displays the current grid or canvas the user is drawing on
    /// </summary>
    class Canvas
    {
        public int MarginLeft { get; }
        public int MarginTop { get; }
        public int Width { get; }
        public int Height { get; }
        public Pixel[,] pixelData;
        Stack<Stack<Pixel>> undoStack;

        public Canvas()
        {
            MarginLeft = 18;
            MarginTop = 9;
            
            Width = 38;
            Height = 16;

            pixelData = new Pixel[Height, Width];
            undoStack = new Stack<Stack<Pixel>>();

            for (int i = 0; i < pixelData.GetLength(0); i++)
                for (int e = 0; e < pixelData.GetLength(1); e++)
                    pixelData[i, e] = new Pixel(e, i, 0, 0, ConsoleColor.Black);

            Draw();
        }

        public void Draw()
        {
            // Draw border
            Console.CursorVisible = false;
            Console.SetCursorPosition(MarginLeft - 2, MarginTop);
            Console.BackgroundColor = ConsoleColor.White;

            for (int i = 0; i < Height; i += 2)
            {
                Console.Write("  ");
                Console.CursorLeft -= 2;
                Console.CursorTop += 2;
            }

            Console.Write("  ");
            Console.CursorLeft += 2;

            for (int i = 0; i < Width; i += 2)
            {
                Console.Write("  ");
                Console.CursorLeft += 2;
            }

            // Draw Pixels
            Console.SetCursorPosition(MarginLeft, MarginTop);
            for (int h = 0; h < Height; h++)
            {
                Console.SetCursorPosition(MarginLeft, MarginTop + h);
                for (int w = 0; w < Width; w++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = pixelData[h,w].Color;
                    Console.Write("  ");
                }
            }

            Console.CursorVisible = true;
            Console.ResetColor();
        }

        /// <summary>
        /// Changes the ConsoleColor value in the pixelData array
        /// at position x, y
        /// </summary>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Update(ConsoleColor color, int x, int y)
        {
            pixelData[y, x].Color = color;
            Draw();
        }

        public void UpdateWithoutDraw(ConsoleColor color, int x, int y)
        {
            pixelData[y, x].Color = color;
        }

        /// <summary>
        /// Creates a blank pixelData array and redraws the canvas
        /// </summary>
        public void Reset()
        {
            //pixelData = new Pixel[Height, Width];
            foreach (Pixel p in pixelData)
                p.Color = ConsoleColor.Black;
            Draw();
        }

        /// <summary>
        /// Saves the canvas and pixelData array as a file of name filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Save(string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter($"{filename}.txt"))
                {
                    for (int i = 0; i < Height; i++)
                    {
                        string line = "";
                        for (int j = 0; j < Width; j++)
                        {
                            line += (int)pixelData[i, j].Color;
                            line += ",";
                        }

                        // remove extra trailing character in line
                        line = line.Remove(line.Length - 1);
                        writer.WriteLine(line);
                    }
                }
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// With the given filename, will load the pixelData and redraw the canvas
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Load(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader($"{filename}.txt"))
                {
                    int index = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] input = line.Split(',');

                        for (int i = 0; i < input.Length; i++)
                        {
                            pixelData[index, i].Color = (ConsoleColor)Int32.Parse(input[i]);
                        }
                        index++;
                    }
                }
                Draw();
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Fills in all blocks of the same color accessible from the input x and y
        /// with the ConsoleColor newColor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="newColor"></param>
        public void FloodFill(int x, int y, ConsoleColor newColor)
        {
            int curX = Console.CursorLeft;
            int curY = Console.CursorTop;
            Stack<Pixel> currentUndo = new Stack<Pixel>();

            FloodFill(x, y, newColor, pixelData[y, x].Color, currentUndo);
            Draw();
            undoStack.Push(currentUndo);
            Console.SetCursorPosition(curX, curY);
        }

        private void FloodFill(int x, int y, ConsoleColor newColor, ConsoleColor colorToReplace, Stack<Pixel> currentUndo)
        {
            if (pixelData[y, x].Color != colorToReplace)
                return;

            currentUndo.Push(new Pixel(x, y, Console.CursorLeft, Console.CursorTop, pixelData[y, x].Color));
            pixelData[y, x].Color = newColor;

            if (x - 1 >= 0 && x - 1 < pixelData.GetLength(1))
                FloodFill(x - 1, y, newColor, colorToReplace, currentUndo);

            if (x + 1 >= 0 && x + 1 < pixelData.GetLength(1))
                FloodFill(x + 1, y, newColor, colorToReplace, currentUndo);

            if (y - 1 >= 0 && y - 1 < pixelData.GetLength(0))
                FloodFill(x, y - 1, newColor, colorToReplace, currentUndo);

            if (y + 1 >= 0 && y + 1 < pixelData.GetLength(0))
                FloodFill(x, y + 1, newColor, colorToReplace, currentUndo);
        }

        /// <summary>
        /// Adds a Stack of Pixels to the undoStack as a save state to revert to 
        /// if the user calls the Undo method
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddUndo(int x, int y)
        {
            int x2 = Console.CursorLeft;
            int y2 = Console.CursorTop;
            Stack<Pixel> currentUndo = new Stack<Pixel>();

            currentUndo.Push(new Pixel(x, y, Console.CursorLeft, Console.CursorTop, pixelData[y, x].Color));
            undoStack.Push(currentUndo);
        }

        /// <summary>
        /// Pops the pixels on top of the undoStack and alters the pixelData to match them
        /// </summary>
        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                int cursX = 0;
                int cursY = 0;

                Stack<Pixel> pixel = undoStack.Pop();
                foreach (Pixel p in pixel)
                {
                    UpdateWithoutDraw(p.Color, p.PosX, p.PosY);
                    cursX = p.AbsoluteX;
                    cursY = p.AbsoluteY;
                }
                Draw();
                Console.SetCursorPosition(cursX, cursY);
            }
        }

        /// <summary>
        /// Replaces all of the selected color on the canvas with ConsoleColor newColor
        /// </summary>
        /// <param name="newColor"></param>
        public void replaceColor(ConsoleColor newColor)
        {
            int x = (Console.CursorLeft - MarginLeft) / 2;
            int y = Console.CursorTop - MarginTop;
            int x2 = Console.CursorLeft;
            int y2 = Console.CursorTop;
            Stack<Pixel> currentUndo = new Stack<Pixel>();

            ConsoleColor oldColor = pixelData[y,x].Color;

            var pixelsToReplace =
                from Pixel p in pixelData
                where p.Color == oldColor
                select p;

            foreach (Pixel p in pixelsToReplace)
            {
                currentUndo.Push(new Pixel(p.PosX, p.PosY, x2, y2, p.Color));
                p.Color = newColor;
            }

            Draw();
            undoStack.Push(currentUndo);
            Console.SetCursorPosition(x2, y2);
        }
    }
}
