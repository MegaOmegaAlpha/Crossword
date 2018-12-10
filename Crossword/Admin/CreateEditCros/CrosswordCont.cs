using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword.Admin.CreateEditCros
{
    [Serializable]
    class CrosswordCont
    {

        private Grid grid;
        private Dictionary<string, string> dictionary;

        public CrosswordCont(Grid gr, Dictionary<string, string> dict)
        {
            grid = gr;
            dictionary = dict;
        }

        public Grid GetGrid()
        {
            return grid;
        }

        public Dictionary<string, string> GetDictionary()
        {
            return dictionary;
        }

    }
}
