using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrawApp
{
    /// <summary>
    /// The class representing a ConsoleCursor with more indepth functionality
    /// with the keyboard and the Canvas class
    /// 
    /// Author: Ethan King
    /// </summary>
    class Cursor
    {
        private ConsoleColor selectedColor = ConsoleColor.DarkBlue;
        private Palette palette;
        private Canvas canvas;
        private Menu menu;

        public Cursor(Canvas canvas, Palette palette, Menu menu)
        {
            this.canvas = canvas;
            this.palette = palette;
            this.menu = menu;

            CursorInput();
        }


        /// <summary>
        /// Input Manager that controls the input and functionality of the cursor
        /// 
        /// Author: Ethan King
        /// </summary>
        public void CursorInput()
        {
            while (true)
            {
                while (Console.KeyAvailable == false)
                {
                    Thread.Sleep(50);
                }

                ConsoleKeyInfo cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    // Cursor Movement
                    case ConsoleKey.W:
                        if (Console.CursorTop - 1 >= menu.MarginTop)
                            Console.CursorTop--;
                        break;

                    case ConsoleKey.A:
                        if (Console.CursorLeft - 2 >= palette.MarginLeft)
                            Console.CursorLeft -= 2;
                        break;

                    case ConsoleKey.S:
                        if (Console.CursorTop + 1 < canvas.MarginTop + canvas.Height)
                            Console.CursorTop++;
                        break;

                    case ConsoleKey.D:
                        if (Console.CursorLeft + 2 < canvas.MarginLeft + (canvas.Width * 2))
                            Console.CursorLeft += 2;
                        break;

                    // Selection
                    case ConsoleKey.K:
                        int tempx = Console.CursorLeft;
                        int tempy = Console.CursorTop;
                        if (IsOverCanvas())
                        {
                            canvas.AddUndo((Console.CursorLeft - canvas.MarginLeft) / 2, Console.CursorTop - canvas.MarginTop);
                            canvas.Update(selectedColor, (Console.CursorLeft - canvas.MarginLeft) / 2, Console.CursorTop - canvas.MarginTop);
                        }
                        else if (IsOverPalette())
                        {
                            selectedColor = GetPaletteColor();
                        }
                        else if (IsOverSaveButton())
                        {
                            menu.DrawNotification("Please enter a filename to save as: ");
                            string filename = Console.ReadLine();
                            if (canvas.Save(filename))
                                menu.DrawNotification($"Drawing saved as {filename}.txt");
                            else
                                menu.DrawNotification($"ERROR: file {filename}.txt could not be created.");
                        }
                        else if (IsOverLoadButton())
                        {
                            menu.DrawNotification("Please enter a filename to load from: ");
                            string filename = Console.ReadLine();
                            if (canvas.Load(filename))
                                menu.DrawNotification($"Drawing loaded from {filename}.txt");
                            else
                                menu.DrawNotification($"ERROR: file {filename}.txt was not found.");
                        }
                        else if (IsOverNewButton())
                        {
                            canvas.Reset();
                            menu.DrawNotification("The Canvas has been reset");
                        }

                        // Reset cursor position to original location, as it gets moved around on redraws
                        Console.SetCursorPosition(tempx, tempy);
                        break;

                    case ConsoleKey.J:
                        if (IsOverCanvas())
                        {
                            canvas.FloodFill((Console.CursorLeft - canvas.MarginLeft) / 2,
                                Console.CursorTop - canvas.MarginTop,
                                selectedColor);
                        }
                        break;

                    case ConsoleKey.U:
                        canvas.Undo();
                        break;

                    case ConsoleKey.L:
                        canvas.replaceColor(selectedColor);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Checks if the mouse is within the DrawingGrid and returns a boolean
        /// 
        /// Author: Ethan King
        /// </summary>
        /// <returns></returns>
        private bool IsOverCanvas()
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;
             
            if (cursorX < canvas.MarginLeft || cursorX >= canvas.MarginLeft + (canvas.Width * 2))
                return false;

            if (cursorY < canvas.MarginTop || cursorY >= canvas.MarginTop + canvas.Height)
                return false;

            return true;
        }

        private bool IsOverSaveButton()
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            if (cursorX < menu.MarginLeft || cursorX > menu.MarginLeft + 10)
                return false;

            if (cursorY != menu.MarginTop)
                return false;

            return true;
        }

        private bool IsOverLoadButton()
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            if (cursorX < menu.MarginLeft + 14 || cursorX > menu.MarginLeft + 24)
                return false;

            if (cursorY != menu.MarginTop)
                return false;

            return true;
        }

        private bool IsOverNewButton()
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            if (cursorX < menu.MarginLeft + 28 || cursorX > menu.MarginLeft + 38)
                return false;

            if (cursorY != menu.MarginTop)
                return false;

            return true;
        }

        private bool IsOverPalette()
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            if (cursorX < palette.MarginLeft || cursorX > palette.MarginLeft + 6)
                return false;

            if (cursorY < palette.MarginTop || cursorY > palette.MarginTop + 64 )
                return false;

            return true;
        }
        
        private ConsoleColor GetPaletteColor()
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            // Left side of pallet
            if (cursorX >= palette.MarginLeft && cursorX < palette.MarginLeft + 4)
            {
                // Dark Blue
                if (cursorY >= palette.MarginTop && cursorY < palette.MarginTop + 2)
                {
                    menu.DrawNotification("Dark Blue Selected");
                    return ConsoleColor.DarkBlue;
                }
                // Dark Cyan
                if (cursorY >= palette.MarginTop + 2 && cursorY < palette.MarginTop + 4)
                {
                    menu.DrawNotification("Dark Cyan Selected");
                    return ConsoleColor.DarkCyan;
                }
                // Dark Green
                if (cursorY >= palette.MarginTop + 4 && cursorY < palette.MarginTop + 6)
                {
                    menu.DrawNotification("Dark Green Selected");
                    return ConsoleColor.DarkGreen;
                }
                // Dark Yellow
                if (cursorY >= palette.MarginTop + 6 && cursorY < palette.MarginTop + 8)
                {
                    menu.DrawNotification("Dark Yellow Selected");
                    return ConsoleColor.DarkYellow;
                }
                // Dark Red
                if (cursorY >= palette.MarginTop + 8 && cursorY < palette.MarginTop + 10)
                {
                    menu.DrawNotification("Dark Red Selected");
                    return ConsoleColor.DarkRed;
                }
                // Dark Magenta
                if (cursorY >= palette.MarginTop + 10 && cursorY < palette.MarginTop + 12)
                {
                    menu.DrawNotification("Dark Magenta Selected");
                    return ConsoleColor.DarkMagenta;
                }
                // White
                if (cursorY >= palette.MarginTop + 12 && cursorY < palette.MarginTop + 14)
                {
                    menu.DrawNotification("White Selected");
                    return ConsoleColor.White;
                }
                // Black
                if (cursorY >= palette.MarginTop + 14 && cursorY < palette.MarginTop + 16)
                {
                    menu.DrawNotification("Black Selected");
                    return ConsoleColor.Black;
                }
            }
            // Right side of pallet
            else
            {
                if (cursorY >= palette.MarginTop && cursorY < palette.MarginTop + 2)
                {
                    menu.DrawNotification("Blue Selected");
                    return ConsoleColor.Blue;
                }
                // Dark Cyan
                if (cursorY >= palette.MarginTop + 2 && cursorY < palette.MarginTop + 4)
                {
                    menu.DrawNotification("Cyan Selected");
                    return ConsoleColor.Cyan;
                }
                // Dark Green
                if (cursorY >= palette.MarginTop + 4 && cursorY < palette.MarginTop + 6)
                {
                    menu.DrawNotification("Green Selected");
                    return ConsoleColor.Green;
                }
                // Dark Yellow
                if (cursorY >= palette.MarginTop + 6 && cursorY < palette.MarginTop + 8)
                {
                    menu.DrawNotification("Yellow Selected");
                    return ConsoleColor.Yellow;
                }
                // Dark Red
                if (cursorY >= palette.MarginTop + 8 && cursorY < palette.MarginTop + 10)
                {
                    menu.DrawNotification("Red Selected");
                    return ConsoleColor.Red;
                }
                // Dark Magenta
                if (cursorY >= palette.MarginTop + 10 && cursorY < palette.MarginTop + 12)
                {
                    menu.DrawNotification("Magenta Selected");
                    return ConsoleColor.Magenta;
                }
                // White
                if (cursorY >= palette.MarginTop + 12 && cursorY < palette.MarginTop + 14)
                {
                    menu.DrawNotification("Gray Selected");
                    return ConsoleColor.Gray;
                }
                // Black
                if (cursorY >= palette.MarginTop + 14 && cursorY < palette.MarginTop + 16)
                {
                    menu.DrawNotification("Dark Gray Selected");
                    return ConsoleColor.DarkGray;
                }
            }

            menu.DrawNotification("Black Selected");
            return ConsoleColor.Black;
        }
    }
}
