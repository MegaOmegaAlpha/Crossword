using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Admin.CreateEditCros
{
    [Serializable]
    enum Direction
    {
        Horizontal,
        Vertical
    }

    [Serializable]
    class Word
    {

        private int i;
        private int j;
        private string notion;
        private Direction direction;

        public Word(int indI, int indJ, string notion, Direction direction)
        {
            i = indI;
            j = indJ;
            this.notion = notion;
            this.direction = direction;
        }

        public int GetI()
        {
            return i;
        }

        public int GetJ()
        {
            return j;
        }

        public string GetNotion()
        {
            return notion;
        }

        public Direction GetDirection()
        {
            return direction;
        }
    }
}
