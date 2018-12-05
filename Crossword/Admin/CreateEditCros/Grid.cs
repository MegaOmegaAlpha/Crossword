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

        public Grid(int width, int height)
        {
            this.height = height;
            this.width = width;
            gridMatr = new bool[width, height];
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

        public bool GetGridItem(int i, int j)
        {
            return gridMatr[i, j];
        }

        public void SetGridItem(int i, int j, bool val)
        {
            gridMatr[i, j] = val;
        }
    }
}
