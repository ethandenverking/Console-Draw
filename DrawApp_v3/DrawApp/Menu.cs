using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    class Menu
    {
        public int MarginLeft { get; }
        public int MarginTop { get; }

        /// <summary>
        /// Constructs the menu object
        /// </summary>
        public Menu()
        {
            MarginLeft = 6;
            MarginTop = 7;
            
            DrawTitle();

            DrawButton("SAVE FILE ", MarginLeft, MarginTop);
            DrawButton("LOAD FILE ", MarginLeft + 14, MarginTop);
            DrawButton("NEW CANVAS", MarginLeft + 28, MarginTop);
        }

        /// <summary>
        /// Draws an ASCII art title
        /// 
        /// Author: Ethan King
        /// </summary>
        private void DrawTitle()
        {
            Console.SetCursorPosition(MarginLeft, 0);
            Console.WriteLine("                            _      _____                     ");
            Console.CursorLeft = MarginLeft;
            Console.WriteLine("                           | |    |  __ \\                    ");
            Console.CursorLeft = MarginLeft;
            Console.WriteLine("   ___ ___  _ __  ___  ___ | | ___| |  | |_ __ __ ___      __");
            Console.CursorLeft = MarginLeft;
            Console.WriteLine("  / __/ _ \\| '_ \\/ __|/ _ \\| |/ _ \\ |  | | '__/ _` \\ \\ /\\ / /");
            Console.CursorLeft = MarginLeft;
            Console.WriteLine(" | (_| (_) | | | \\__ \\ (_) | |  __/ |__| | | | (_| |\\ V  V / ");
            Console.CursorLeft = MarginLeft;
            Console.WriteLine("  \\___\\___/|_| |_|___/\\___/|_|\\___|_____/|_|  \\__,_| \\_/\\_/  ");
        }

        /// <summary>
        /// Draws a button with text at the designated location
        /// 
        /// Author: Ethan King
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawButton(string buttonText, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" ");
            Console.Write(buttonText);
            Console.Write(" ");

            Console.ResetColor();
        }

        /// <summary>
        /// Displays a notification in the bottom right with the input text
        /// </summary>
        /// <param name="notificationText"></param>
        public void DrawNotification(string notificationText)
        {
            Console.SetCursorPosition(MarginLeft, 27);
            Console.Write(new string (' ', Console.WindowWidth));
            Console.SetCursorPosition(MarginLeft, 27);
            Console.Write(notificationText);
            Console.ResetColor();
        }
    }
}
