using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Admin.CreateEditCros
{
    [Serializable]
    class Solution
    {
        private Dictionary<string, string> dictionary;
        private Grid grid;
        private string[,] solutionGrid;
        private int helpCount;

        public Solution(Dictionary<string, string> _dictionary, Grid _grid, string[,] _solutionGrid, int _helpCount)
        {
            dictionary = _dictionary;
            grid = _grid;
            solutionGrid = _solutionGrid;
            helpCount = _helpCount;
        }

        public Dictionary<string, string> GetDictionary()
        {
            return dictionary;
        }

        public Grid GetGrid()
        {
            return grid;
        }

        public string[,] GetSolutionGrid()
        {
            return solutionGrid;
        }

        public int GetHelpCount()
        {
            return helpCount;
        }
    }
}
