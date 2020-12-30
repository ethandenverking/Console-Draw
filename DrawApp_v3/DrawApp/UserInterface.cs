using System;

namespace DrawApp
{
    /// <summary>
    /// A class for storing the dimensions of and drawing 
    /// the digital canvas on the Console
    /// 
    /// Author: Ethan King
    /// </summary>
    class UserInterface
    {
        public int SideMargin { get; }
        public int TopMargin { get; }
        public int Width { get; }
        public int Height { get; }
        ConsoleColor[,] colorData;

        public UserInterface(int width, int height)
        {
            Width = width;
            Height = height;
            SideMargin = 18;
            TopMargin = 9;
            colorData = new ConsoleColor[height, width];
        }

        /// <summary>
        /// Overloaded constructor to create from a file
        /// 
        /// Author: Ethan King
        /// </summary>
        /// <param name="colorData"></param>
        public UserInterface(ConsoleColor[,] colorData)
        {
            // TODO
        }



        /// <summary>
        /// Draws the drawing grid in the bottom right corner
        /// 
        /// Author: Ethan King
        /// </summary>
        private void DrawGrid()
        {

        }

        /// <summary>
        /// Used to store and change color data from the Drawing Grid
        /// 
        /// Author: Ethan King
        /// </summary>
        /// <param name="cursorX"></param>
        /// <param name="cursorY"></param>
        /// <param name="color"></param>
        public void UpdateColorData(int cursorX, int cursorY, ConsoleColor color)
        {
            colorData[cursorY, cursorX] = color;
        }

        public void ResetCanvas()
        {


        }
    }
}
