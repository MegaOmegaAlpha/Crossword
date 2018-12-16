using Crossword.Admin.CreateEditCros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossword.Admin
{
    public partial class FormHandMadeCros : Form
    {
        private FormBeforeCreate formBefore;
        private FormAdmin formAdmin;
        private int width, height;
        private Button[,] buttons;
        private string fileDict;
        private Dictionary<string, string> dictionary;
        private Grid grid;
        private readonly List<string> _words = new List<string>();
        private List<string> _order;
        Crossik _board;
        Random _rand = new Random();
        private string[] notions;
        CrosswordCont mainCros;
        bool editing;
        public FormHandMadeCros(FormBeforeCreate formBefore, int width, int height, string fileName)
        {
            this.formBefore = formBefore;
            this.width = width;
            this.height = height;
            fileDict = fileName;
            InitializeComponent();
        }

        public FormHandMadeCros(string fileName)
        {
            InitializeComponent();
            height = 7;
            width = 7;
            this.fileDict = fileName;
        }

        public FormHandMadeCros(FormAdmin formBefore, string fileName)
        {
            InitializeComponent();
            Stream stream = new FileStream(fileName, FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();
            mainCros = (CrosswordCont)deserializer.Deserialize(stream);
            formAdmin = formBefore;
            formAdmin.Visible = false;
            editing = true;
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        #region class Node definition
        //содержит координаты кнопки
        private class Node
        {
            private int i;
            private int j;
            public Node(int i, int j)
            {
                this.i = i;
                this.j = j;
            }
            public int I
            {
                get { return i; }
            }
            public int J
            {
                get { return j; }
            }
        }
        #endregion 

        private void FormHandMadeCros_Load(object sender, EventArgs e)
        {
            if (!editing)
            {
                grid = new Grid(width, height);
                _board = new Crossik(width, height, ref grid);

                listButton = new List<Button>();                
                buttons = new Button[width, height];
                var tableLayoutPanel = new TableLayoutPanel();
                tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                tableLayoutPanel.Visible = true;
                tableLayoutPanel.ColumnCount = width;
                tableLayoutPanel.RowCount = height;
                //размер колонки и строки в процентах
                int widthT = 100 / tableLayoutPanel.ColumnCount;
                int heightT = 100 / tableLayoutPanel.RowCount;
                // добавляем колонки и строки
                for (int col = 0; col < tableLayoutPanel.ColumnCount; col++)
                {
                    //добавляем колонку
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, widthT));
                    for (int row = 0; row < tableLayoutPanel.RowCount; row++)
                    {
                        //строка
                        if (col == 0)
                        {
                            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, heightT));
                        }
                        //инициализация экземпляра
                        buttons[col, row] = new Button();
                        buttons[col, row].Dock = DockStyle.Fill;
                        buttons[col, row].BackColor = Color.Black;
                        buttons[col, row].Click += GridButtonClicked;
                        buttons[col, row].Tag = new Node(col, row);
                        grid.SetGridItem(col, row, false);
                        tableLayoutPanel.Controls.Add(buttons[col, row], col, row);
                    }
                }
                //таблицу в контейнер
                TableContainer.Controls.Add(tableLayoutPanel);

                textBoxCurrentDict.Text = fileDict;

                //заполнение словаря
                StreamReader reader = new StreamReader(fileDict, Encoding.GetEncoding("Windows-1251"));
                string dataFromFile = "";
                dictionary = new Dictionary<string, string>();
                try
                {
                    while (dataFromFile != null)
                    {
                        dataFromFile = reader.ReadLine();
                        if (dataFromFile != null)
                        {
                            string[] stringArr = dataFromFile.Split(' ');
                            string notion = stringArr[0];
                            string def = "";
                            for (int i = 1; i < stringArr.Length; i++)
                            {
                                def += " " + stringArr[i];
                            }
                            dictionary.Add(notion, def);
                            listBoxDict.Items.Add(notion);
                        }
                    }
                    notions = new string[dictionary.Count];
                    notions = dictionary.Keys.ToArray();
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("В словаре повторяется понятие!", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    formBefore.Visible = true;
                    Close();
                }
            }
            else
            {
                dictionary = new Dictionary<string, string>();
                listButton = new List<Button>();
                grid = mainCros.GetGrid();
                List<Word> words = grid.GetWords();
                buttons = new Button[grid.Height, grid.Width];
                var tableLayoutPanel = new TableLayoutPanel();
                tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                tableLayoutPanel.Visible = true;
                tableLayoutPanel.ColumnCount = grid.Width;
                tableLayoutPanel.RowCount = grid.Height;
                width = grid.Width;
                height = grid.Height;
                //размер колонки и строки в процентах
                int widthT = 100 / tableLayoutPanel.ColumnCount;
                int heightT = 100 / tableLayoutPanel.RowCount;
                // добавляем колонки и строки
                for (int col = 0; col < tableLayoutPanel.ColumnCount; col++)
                {
                    //добавляем колонку
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, widthT));
                    for (int row = 0; row < tableLayoutPanel.RowCount; row++)
                    {
                        //строка
                        if (col == 0)
                        {
                            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, heightT));
                        }
                        buttons[col, row] = new Button();
                        buttons[col, row].Dock = DockStyle.Fill;
                        buttons[col, row].BackColor = Color.Black;
                        buttons[col, row].Click += GridButtonClicked;
                        buttons[col, row].Tag = new Node(col, row);
                        tableLayoutPanel.Controls.Add(buttons[col, row], col, row);

                    }
                }
                //таблицу в контейнер
                TableContainer.Controls.Add(tableLayoutPanel);

                textBoxCurrentDict.Text = fileDict;
                //размещение букв на сетке
                foreach (Word word in words)
                {
                    if (word.GetDirection().Equals(Direction.Horizontal))
                    {
                        for (int i = word.GetJ(), j = 0; i < word.GetJ() + word.GetNotion().Length; i++, j++)
                        {
                            buttons[i, word.GetI()].BackColor = Color.LightBlue;
                            buttons[i, word.GetI()].Text = word.GetNotion().ElementAt(j).ToString();
                        }
                        listBoxHor.Items.Add(word.GetNotion());
                        dictionary.Add(word.GetNotion(), mainCros.GetDictionary()[word.GetNotion()]);
                    }
                    else
                    {
                        for (int i = word.GetI(), j = 0; i < word.GetI() + word.GetNotion().Length; i++, j++)
                        {
                            buttons[word.GetJ(), i].BackColor = Color.LightBlue;
                            buttons[word.GetJ(), i].Text = word.GetNotion().ElementAt(j).ToString();
                        }
                        listBoxVert.Items.Add(word.GetNotion());
                        dictionary.Add(word.GetNotion(), mainCros.GetDictionary()[word.GetNotion()]);
                    }
                }

            }
        }

        private bool isSortedLet;
        //сортировка по алфавиту
        private void sortLetter()
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < listBoxDict.Items.Count; i++)
            {
                strings.Add(listBoxDict.Items[i].ToString());
            }
            if (!isSortedLet)
            {
                strings.Sort();
                isSortedLet = true;
            }
            else
            {
                strings.Sort(delegate (string s1, string s2) { return -s1.CompareTo(s2); });
                isSortedLet = false;
            }
            listBoxDict.Items.Clear();
            foreach (string str in strings)
            {
                listBoxDict.Items.Add(str);
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            sortLetter();
        }

        private bool isSortLen;
        //сортировка по длине
        private void sortLen()
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < listBoxDict.Items.Count; i++)
            {
                strings.Add(listBoxDict.Items[i].ToString());
            }
            if (!isSortLen)
            {
                strings.Sort(delegate (string s1, string s2) { return s1.Length.CompareTo(s2.Length); });
                isSortLen = true;
            }
            else
            {
                strings.Sort(delegate (string s1, string s2) { return -s1.Length.CompareTo(s2.Length); });
                isSortLen = false;
            }
            listBoxDict.Items.Clear();
            foreach (string str in strings)
            {
                listBoxDict.Items.Add(str);
            }
        }

        private void buttonSortLen_Click(object sender, EventArgs e)
        {
            sortLen();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonDir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text |*.txt";
            openFileDialog.Title = "Открыть словарь";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != null)
            {
                textBoxCurrentDict.Text = openFileDialog.FileName;
                fileDict = openFileDialog.FileName;
                StreamReader reader = new StreamReader(fileDict, Encoding.GetEncoding("Windows-1251"));
                string dataFromFile = "";
                dictionary.Clear();
                listBoxDict.Items.Clear();
                try
                {
                    while (dataFromFile != null)
                    {
                        dataFromFile = reader.ReadLine();
                        if (dataFromFile != null)
                        {
                            string[] stringArr = dataFromFile.Split(' ');
                            string notion = stringArr[0];
                            string def = "";
                            for (int i = 1; i < stringArr.Length; i++)
                            {
                                def += " " + stringArr[i];
                            }
                            dictionary.Add(notion.ToUpper(), def.ToLower());
                            listBoxDict.Items.Add(notion);
                            notions = new string[dictionary.Count];
                            notions = dictionary.Keys.ToArray();
                        }
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("В словаре повторяется понятие!", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    formBefore.Visible = true;
                    Close();
                }
            }
        }

        private int length;
        private void buttonAddNotion_Click(object sender, EventArgs e)
        {
            if (listBoxDict.SelectedItem != null && listButton.Count > 0)
            {
                bool check = true;
                Node node1, node2, nodeL;
                //проверка корректности выделения области
                for (int i = 0; i < listButton.Count - 1; i++)
                {
                    node1 = (Node)listButton.ElementAt(i).Tag;
                    node2 = (Node)listButton.ElementAt(i + 1).Tag;
                    if ((node2.I - node1.I != 1 && node2.J - node1.J == 0) ||
                        (node2.I - node1.I == 0 && node2.J - node1.J != 1) ||
                        (node2.I - node1.I != 0 && node2.J - node1.J != 0))
                    //между кнопками не должно быть разрывов, направление дожно соблюдаться
                    {
                        check = false;
                    }

                }
                node1 = (Node)listButton.First().Tag;
                nodeL = (Node)listButton.Last().Tag;
                //если первая и последняя кнопка не в одной плоскости, то ошибка
                if (nodeL.I - node1.I != 0 && nodeL.J - node1.J != 0)
                {
                    check = false;
                }

                if (check)
                {
                    string not = listBoxDict.SelectedItem.ToString();
                    char[] charNot = not.ToCharArray();
                    node1 = (Node)listButton.ElementAt(0).Tag;
                    node2 = (Node)listButton.ElementAt(1).Tag;
                    nodeL = (Node)listButton.Last().Tag;
                    Direction dir;
                    Word word;
                    //определение направления
                    if (node1.I == node2.I - 1)
                    {
                        dir = Direction.Horizontal;
                        listBoxHor.Items.Add(not);
                        #region block outside buttons
                        //блокировка крайних положений, чтобы на них нельзя было кликать
                        if (0 < node1.I && nodeL.I < width - 1)
                        {
                            buttons[node1.I - 1, node1.J].Enabled = false;
                            buttons[nodeL.I + 1, node1.J].Enabled = false;
                        }
                        else
                        {
                            if (0 < node1.I && nodeL.I == width - 1)
                            {
                                buttons[node1.I - 1, node1.J].Enabled = false;
                            }
                            else
                            {
                                if (0 == node1.I && nodeL.I < width - 1)
                                {
                                    buttons[nodeL.I + 1, node1.J].Enabled = false;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        dir = Direction.Vertical;
                        listBoxVert.Items.Add(not);
                        #region block outside buttons
                        //блокировка крайних положений, чтобы на них нельзя было кликать
                        if (0 < node1.J && nodeL.J < height - 1)
                        {
                            buttons[node1.I, node1.J - 1].Enabled = false;
                            buttons[node1.I, nodeL.J + 1].Enabled = false;
                        }
                        else
                        {
                            if (0 < node1.J && nodeL.J == width - 1)
                            {
                                buttons[node1.I, node1.J - 1].Enabled = false;
                            }
                            else
                            {
                                if (0 == node1.J && nodeL.J < width - 1)
                                {
                                    buttons[node1.I, nodeL.J + 1].Enabled = false;
                                }
                            }
                        }
                        #endregion
                    }
                    int intI = node1.I;
                    int intJ = node1.J;
                    word = new Word(intJ, intI, not, dir);
                    for (int i = 0; i < charNot.Length; i++)
                    {
                        listButton.ElementAt(i).BackColor = Color.LightBlue;
                        listButton.ElementAt(i).Text = charNot[i].ToString();
                    }
                    grid.AddWord(word);
                    length = 0;
                    listButton.Clear();                    
                }
                else
                {
                    MessageBox.Show("Область выделена неверно!", "Ошибка", MessageBoxButtons.OK,
                                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private List<Button> listButton;
        
        private void GridButtonClicked(object sender, EventArgs e)
        {
            int countOfNeighbours = 0;
            Button button = (Button)sender;           
            //если на эту кнопку ничего не добавлено
            if (!button.BackColor.Equals(Color.LightBlue))
            {
                Node node = (Node)button.Tag;
                try
                {
                    //установка флага на сетке
                    grid.SetGridItem(node.I, node.J, !grid.GetGridItem(node.I, node.J));
                    button.BackColor = grid.GetGridItem(node.I, node.J) ? Color.White : Color.Black;

                    if (button.BackColor.Equals(Color.White))
                    {
                        listButton.Add(button);
                    }
                    else
                    {
                        listButton.Remove(button);
                    }
                    #region looking for intersections
                    //поиск соседних клеток
                    for (int i = node.J - 1; i <= node.J + 1; i++)
                    {
                        for (int j = node.I - 1; j <= node.I + 1; j++)
                        {
                            if (i >= 0 && j >= 0 && i < width && j < height && !(j == width && i == height))
                            {
                                if (buttons[j, i].BackColor.Equals(Color.LightBlue) || buttons[j, i].BackColor.Equals(Color.White))
                                {
                                    countOfNeighbours++;
                                }
                                //игнорирование диагональных соседей
                                if ((!(i == node.J - 1 && j == node.I - 1) &&
                                     !(i == node.J + 1 && j == node.I + 1) &&
                                     !(i == node.J - 1 && j == node.I + 1) &&
                                     !(i == node.J + 1 && j == node.I - 1)))
                                {
                                    //если сосед содержит букву
                                    if (buttons[j, i].BackColor.Equals(Color.LightBlue))
                                    {
                                        Button buttonNeigh = buttons[j, i];
                                        //если такая кнопка не встречалась
                                        if (!listButton.Contains(buttonNeigh))
                                        {
                                            if (!button.BackColor.Equals(Color.Black))
                                            {
                                                //установка пересечения
                                                grid.SetInters(j, i, true);
                                                Button added = listButton.First();
                                                Node addedNode = (Node)added.Tag;
                                                //если сосед идет перед новой буквой, то добавить перед ней
                                                if ((i == addedNode.J - 1 && j == addedNode.I) ||
                                                    (i == addedNode.J && j == addedNode.I - 1))
                                                {
                                                    listButton.Insert(0, buttonNeigh);
                                                }
                                                //иначе в конец
                                                else
                                                {
                                                    listButton.Add(buttonNeigh);
                                                }
                                            }
                                        }
                                        #region check neighbours around intersection
                                        //иначе проверка, есть ли вокруг пересечения нажатые кнопки
                                        else
                                        {
                                            bool haveClicked = false;
                                            Node nodeNeighbour = (Node)buttonNeigh.Tag;
                                            for (int ii = nodeNeighbour.J - 1; ii <= nodeNeighbour.J + 1; ii++)
                                            {
                                                for (int jj = nodeNeighbour.I - 1; jj <= nodeNeighbour.I + 1; jj++)
                                                {
                                                    if (ii >= 0 && jj >= 0 && ii < width && jj < height && !(jj == width && ii == height))
                                                    {
                                                        //игнорирование диагональных соседей
                                                        if ((!(ii == nodeNeighbour.J - 1 && jj == nodeNeighbour.I - 1) &&
                                                             !(ii == nodeNeighbour.J + 1 && jj == nodeNeighbour.I + 1) &&
                                                             !(ii == nodeNeighbour.J - 1 && jj == nodeNeighbour.I + 1) &&
                                                             !(ii == nodeNeighbour.J + 1 && jj == nodeNeighbour.I - 1)))
                                                        {
                                                            if (buttons[jj, ii].BackColor.Equals(Color.White))
                                                            {
                                                                haveClicked = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            //если вокруг пересечения нет нажатых кнопок, то оно снимается
                                            //и удаляется
                                            if (!haveClicked)
                                            {
                                                listButton.Remove(buttonNeigh);
                                                grid.SetInters(j, i, false);
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                   /* if (countOfNeighbours >= 4)
                    {
                        throw new ArgumentException();
                    }*/
                    #region searching notion by mask
                    string mask = GetMask();
                    length = listButton.Count;
                    if (length > 0)
                    {
                        listBoxDict.Items.Clear();
                        foreach (string notion in notions)
                        {
                            if (Regex.IsMatch(notion, mask, RegexOptions.IgnoreCase))
                            {
                                listBoxDict.Items.Add(notion);
                            }
                        }
                    }
                    else
                    {
                        listBoxDict.Items.AddRange(notions);
                    }
                    #endregion
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Для редактирования кроссворда нужен словарь!", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    button.BackColor = Color.White;
                    grid.SetGridItem(node.I, node.J, !grid.GetGridItem(node.I, node.J));
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Попытка выделить недоступное место", "Ошибка", MessageBoxButtons.OK,
                           MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    listButton.Remove(button);
                    grid.SetGridItem(node.I, node.J, !grid.GetGridItem(node.I, node.J));
                    button.BackColor = grid.GetGridItem(node.I, node.J) ? Color.White : Color.Black;
                }
            }
        }

        private string GetMask()
        {
            string mask = @"";
            foreach (Button button in listButton)
            {
                if (!button.Text.Equals(""))
                {
                    mask += button.Text;
                }
                else
                {
                    mask += ".";
                }
            }
            mask += "$";
            mask = "^" + mask;
            return mask;
        }

        private void buttonDelNotion_Click(object sender, EventArgs e)
        {
            if (!(listBoxHor.SelectedItem != null && listBoxVert.SelectedItem != null))
            {
                //удаляется "горизонтальное" понятие
                if (listBoxHor.SelectedItem != null)
                {
                    string word = listBoxHor.SelectedItem.ToString();
                    Word w = grid.GetWord(word);
                    for (int i = w.GetJ(); i < w.GetJ() + w.GetNotion().Length; i++)
                    {
                        //если буква не является пересечением, то ее можно удалить
                        if (!grid.GetInters(i, w.GetI()))
                        {
                            buttons[i, w.GetI()].BackColor = Color.Black;
                            buttons[i, w.GetI()].Text = "";
                            grid.SetGridItem(i, w.GetI(), false);
                        }
                        //иначе снимаем пересечение
                        else
                        {
                            grid.SetInters(i, w.GetI(), false);
                        }
                    }
                    grid.DeleteWord(w);
                    listBoxHor.Items.Remove(listBoxHor.SelectedItem);
                    #region set ouside buttons enabled=true
                    //разблокировка крайних кнопок для дальнийшего использования
                    if (0 < w.GetJ() && w.GetJ() + w.GetNotion().Length - 1 < width - 1)
                    {
                        buttons[w.GetJ() - 1, w.GetI()].Enabled = true;
                        buttons[w.GetJ() + w.GetNotion().Length, w.GetI()].Enabled = true;
                    }
                    else
                    {
                        if (0 < w.GetJ() && w.GetJ() + w.GetNotion().Length - 1 == width - 1)
                        {
                            buttons[w.GetJ() - 1, w.GetI()].Enabled = true;
                        }
                        else
                        {
                            if (0 == w.GetJ() && w.GetJ() + w.GetNotion().Length - 1 < width - 1)
                            {
                                buttons[w.GetJ() + w.GetNotion().Length, w.GetI()].Enabled = true;
                            }
                        }
                    }
                    #endregion
                }
                //удаляется "вертикальное" понятие
                else
                {
                    if (listBoxVert.SelectedItem != null)
                    {
                        string word = listBoxVert.SelectedItem.ToString();
                        Word w = grid.GetWord(word);
                        for (int i = w.GetI(); i < w.GetI() + w.GetNotion().Length; i++)
                        {
                            if (!grid.GetInters(w.GetJ(), i))
                            {
                                buttons[w.GetJ(), i].BackColor = Color.Black;
                                buttons[w.GetJ(), i].Text = "";
                                grid.SetGridItem(i, w.GetI(), false);
                            }
                            else
                            {
                                grid.SetInters(w.GetJ(), i, false);
                            }
                        }
                        grid.DeleteWord(w);
                        listBoxVert.Items.Remove(listBoxVert.SelectedItem);
                        #region set outside buttons enabled=true
                        //восстановление недоступных конопок
                        if (0 < w.GetI() && w.GetI() + w.GetNotion().Length - 1 < height - 1)
                        {
                            buttons[w.GetJ(), w.GetI() - 1].Enabled = true;
                            buttons[w.GetJ(), w.GetI() + w.GetNotion().Length].Enabled = true;
                        }
                        else
                        {
                            if (0 < w.GetI() && w.GetI() + w.GetNotion().Length - 1 < height - 1)
                            {
                                buttons[w.GetJ(), w.GetI() - 1].Enabled = true;
                            }
                            else
                            {
                                if (0 == w.GetI() && w.GetI() + w.GetNotion().Length - 1 < height - 1)
                                {
                                    buttons[w.GetJ(), w.GetI() + w.GetNotion().Length].Enabled = true;
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
        }

        private void buttonSaveCros_Click(object sender, EventArgs e)
        {
            try
            {

                Dictionary<string, string> dictCross = new Dictionary<string, string>();
                foreach (string item in listBoxVert.Items)
                {
                    dictCross.Add(item, dictionary[item]);
                }
                foreach (string item in listBoxHor.Items)
                {
                    dictCross.Add(item, dictionary[item]);
                }
               
                CrosswordCont crossword = new CrosswordCont(grid, dictCross);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CrosswordFile |*.crwd";
                saveFileDialog.Title = "Сохранить кроссворд";
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName != "")
                {
                    Stream s = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(s, crossword);
                    MessageBox.Show("Кроссворд сохранен", "Сохранение", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    s.Close();
                }
            }
            catch(ArgumentException)
            {
                MessageBox.Show("На сетке повторяются слова!", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        void GenCrossword()
        {
            listBoxHor.Items.Clear();
            listBoxVert.Items.Clear();
            _board.Reset();
            grid.GetWords().Clear();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid.SetGridItem(i, j, false);
                }
            }
            foreach (var word in _order)
            {
                //var wordToInsert = ((bool)RTLRadioButton.IsChecked) ? word.Reverse().Aggregate("",(x,y) => x + y) : word; 
                switch (_board.AddWord(word))
                {
                    case 1:
                        listBoxHor.Items.Add(word);
                        break;
                    case 0:
                        listBoxVert.Items.Add(word);
                        break;
                }
            }
            ActualizeData();
        }
        void ActualizeData()
        {
            var count = _board.N * _board.M;
            var board = _board.GetBoard;
            var p = 0;
            for (var i = 0; i < _board.N; i++)
            {
                for (var j = 0; j < _board.M; j++)
                {
                    var letter = board[i, j] == '*' ? ' ' : board[i, j];
                    if (letter != ' ') count--;
                    //((Button)grid1.Children[p]).Content = letter.ToString(); 
                    //((Button)grid1.Children[p]).Background = letter != ' ' ? _buttons[4].Background : _buttons[0].Background; 
                    buttons[i, j].Text = letter.ToString().Equals(" ") ? "" : letter.ToString();
                    //buttons[i, j].BackColor = Color.Black;
                    /*if (!buttons[i, j].Text.Equals(" "))
                        buttons[i, j].BackColor = Color.LightBlue;*/
                    buttons[i, j].BackColor = grid.GetGridItem(i, j) ? Color.White : Color.Black;
                    if (!buttons[i, j].Text.Equals(""))
                        buttons[i, j].BackColor = Color.LightBlue;
                    p++;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<string> keyList = new List<string>(dictionary.Keys);
            for (int i = 0; i < 1000; i++)
            {
                string randomKey = keyList[_rand.Next(keyList.Count)];
                if (randomKey.Length <= height)
                {
                    _words.Add(randomKey);
                }
            }
            _words.Sort(Comparer);
            _words.Reverse();
            _order = _words;
            GenCrossword();
            //grid = _board.GetGrid();
        }

        private void listBoxVert_Click(object sender, EventArgs e)
        {
            listBoxHor.SelectedIndex = -1;
        }

        private void listBoxHor_Click(object sender, EventArgs e)
        {
            listBoxVert.SelectedIndex = -1;
        }

        private void FormHandMadeCros_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editing)
            {
                formAdmin.Visible = true;
            }
            else
            {
                formBefore.Visible = true;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            listBoxDict.Items.Clear();
            foreach (string notion in notions)
            {
                if (notion.StartsWith(textBoxSearch.Text))
                {
                    listBoxDict.Items.Add(notion);
                }
            }
        }

        static int Comparer(string a, string b)
        {
            var temp = a.Length.CompareTo(b.Length);
            return temp == 0 ? a.CompareTo(b) : temp;
        }

    }
}
