﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossword.Admin
{
    public partial class FormHandMadeCros : Form
    {
        private FormBeforeCreate formBefore;
        private int width, height;
        private Button[,] buttons;
        private string fileDict;
        private Dictionary<string, string> dictionary;
        private Grid grid;
        public FormHandMadeCros(FormBeforeCreate formBefore, int width, int height, string fileName)
        {
            this.formBefore = formBefore;
            this.width = width;
            this.height = height;
            fileDict = fileName;
            InitializeComponent();
        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private class Node
        {
            private int i;
            private int j;
            public Node (int i, int j)
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

        private void FormHandMadeCros_Load(object sender, EventArgs e)
        {
            grid = new Grid(width, height);
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

            //dictionary
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
                        dictionary.Add(notion.ToUpper(), def.ToLower());
                        listBoxDict.Items.Add(notion);
                    }
                }
            }
            catch(ArgumentException)
            {
                MessageBox.Show("В словаре повторяется понятие!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                formBefore.Visible = true;
                Close();
            }
        }

        private bool isSortedLet;
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
            formBefore.Visible = true;
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
                        }
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("В словаре повторяется понятие!", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                    formBefore.Visible = true;
                    Close();
                }
            }
        }

        private void GridButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            Node node = (Node) button.Tag;
            grid.SetGridItem(node.I, node.J, !grid.GetGridItem(node.I, node.J));
            button.BackColor = grid.GetGridItem(node.I, node.J) ? Color.White : Color.Black;
        }
    }
}