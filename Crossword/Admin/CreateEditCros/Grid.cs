using Crossword.Admin.CreateEditCros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Admin
{
    [Serializable]
    class Grid
    {
        private int height;
        private int width;

        private List<Word> words;

        private bool[,] gridMatr;
        private bool[,] isInters;

        public Grid(int width, int height)
        {
            words = new List<Word>();
            this.height = height;
            this.width = width;
            gridMatr = new bool[width, height];
            isInters = new bool[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    isInters[i, j] = false;
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

        public List<Word> GetWords()
        {
            return words;
        }

        public void AddWord(Word word)
        {
            words.Add(word);
        }

        public void DeleteWord(Word word)
        {
            for (int i = 0; i < words.Count; i++)
            {
                if (words.ElementAt(i).GetNotion().Equals(word.GetNotion()))
                {
                    words.Remove(word);
                }
            }
        }

        public Word GetWord(string word)
        {
            foreach (Word w in words) {
                if (w.GetNotion().Equals(word))
                {
                    return w;
                }
            }
            return null;
        }

        public bool GetInters(int i, int j)
        {
            return isInters[i, j];
        }

        public void SetInters(int i, int j, bool val)
        {
            isInters[i, j] = val;
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
