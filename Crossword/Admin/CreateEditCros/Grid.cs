using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Admin
{
    class Grid
    {
        private int height;
        private int width;

        private bool[,] gridMatr;
        private string[,] gridChar;

        public Grid(int width, int height)
        {
            this.height = height;
            this.width = width;
            gridMatr = new bool[width, height];
            gridChar = new string[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    gridChar[i, j] = "";
                }
            }
        }

        public int Height
        {
            get { return height;  }
            set { height = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public string GetGridChar(int i, int j)
        {
            return gridChar[i, j];
        }

        public void SetGridChar(int i, int j, string val)
        {
            gridChar[i, j] = val;
        }

        public bool GetGridItem(int i, int j)
        {
            return gridMatr[i, j];
        }

        public void SetGridItem(int i, int j, bool val)
        {
            gridMatr[i, j] = val;
        }

        public int GetWordLength()
        {
            int len = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (gridMatr[i, j])
                    {
                        len++;
                    }
                }
            }
            return len;
        }

        
    }
}
