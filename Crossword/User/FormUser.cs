﻿using Crossword.Admin;
using Crossword.Admin.CreateEditCros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossword.User
{
    public partial class FormUser : Form
    {

        private FormMain formMain;
        private string fileName;
        private CrosswordCont mainCross;
        private Dictionary<string, string> dictionary;
        private int width, height;
        private Grid grid;
        private RichTextBox[,] textBoxes;
        private int helpCount;
        List<Word> words;
        bool isSolution;
        Solution solution;

        public FormUser(FormMain formMain, string fileName)
        {
            InitializeComponent();
            this.formMain = formMain;
            this.fileName = fileName;
            Stream stream = new FileStream(fileName, FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();
            textBoxСurrentCrwd.Text = fileName;
            if (fileName.EndsWith("slt"))
            {
                isSolution = true;
                solution = (Solution)deserializer.Deserialize(stream);
            }
            else
            {
                deserializer.Deserialize(stream);
                mainCross = (CrosswordCont)deserializer.Deserialize(stream);
            }
            stream.Close();
        }

        /*public FormUser(string fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
            Stream stream = new FileStream(fileName, FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();
            textBoxСurrentCrwd.Text = fileName;
            if (fileName.EndsWith("slt"))
            {
                isSolution = true;
                solution = (Solution)deserializer.Deserialize(stream);
            }
            else
            {
                mainCross = (CrosswordCont)deserializer.Deserialize(stream);
            }
            stream.Close();
        }*/

        private void InitializeCrwd()
        {
            tableContainer.Controls.Clear();
            listBoxHor.Items.Clear();
            listBoxVert.Items.Clear();
            grid = mainCross.GetGrid();
            dictionary = mainCross.GetDictionary();
            words = grid.GetWords();
            width = grid.Width;
            height = grid.Height;
            textBoxes = new RichTextBox[width, height];
            var tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Visible = true;
            tableLayoutPanel.ColumnCount = width;
            tableLayoutPanel.RowCount = height;
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
                    textBoxes[col, row] = new RichTextBox();
                    textBoxes[col, row].Dock = DockStyle.Fill;
                    textBoxes[col, row].MaxLength = 1;
                    textBoxes[col, row].Enabled = false;
                    textBoxes[col, row].KeyPress += textBox_KeyPress;
                    textBoxes[col, row].ScrollBars = RichTextBoxScrollBars.None;
                    tableLayoutPanel.Controls.Add(textBoxes[col, row], col, row);
                }
            }
            //таблицу в контейнер
            tableContainer.Controls.Add(tableLayoutPanel);
            //заполнение сетки
            foreach (Word word in words)
            {
                if (word.GetDirection().Equals(Direction.Horizontal))
                {
                    for (int i = word.GetJ(), j = 0; i < word.GetJ() + word.GetNotion().Length; i++, j++)
                    {
                        textBoxes[i, word.GetI()].BackColor = Color.LightBlue;
                        textBoxes[i, word.GetI()].Tag = word.GetNotion().ElementAt(j).ToString();
                        textBoxes[i, word.GetI()].Enabled = true;
                    }
                    listBoxHor.Items.Add(dictionary[word.GetNotion()]);
                }
                else
                {
                    for (int i = word.GetI(), j = 0; i < word.GetI() + word.GetNotion().Length; i++, j++)
                    {
                        textBoxes[word.GetJ(), i].BackColor = Color.LightBlue;
                        textBoxes[word.GetJ(), i].Tag = word.GetNotion().ElementAt(j).ToString();
                        textBoxes[word.GetJ(), i].Enabled = true;
                    }
                    listBoxVert.Items.Add(dictionary[word.GetNotion()]);
                }
            }
            helpCount = dictionary.Count / 10 > 0 ? dictionary.Count / 10 : 1;

            labelHelpCount.Text = helpCount.ToString();
            tableContainer.Update();
        }

        private void InitializeSolution()
        {
            tableContainer.Controls.Clear();
            grid = solution.GetGrid();
            dictionary = solution.GetDictionary();
            words = grid.GetWords();
            width = grid.Width;
            height = grid.Height;
            textBoxes = new RichTextBox[width, height];
            var tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Visible = true;
            tableLayoutPanel.ColumnCount = width;
            tableLayoutPanel.RowCount = height;
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
                    textBoxes[col, row] = new RichTextBox();
                    textBoxes[col, row].Dock = DockStyle.Fill;
                    textBoxes[col, row].MaxLength = 1;
                    textBoxes[col, row].Enabled = false;
                    textBoxes[col, row].KeyPress += textBox_KeyPress;
                    textBoxes[col, row].ScrollBars = RichTextBoxScrollBars.None;
                    tableLayoutPanel.Controls.Add(textBoxes[col, row], col, row);
                }
            }
            //таблицу в контейнер
            tableContainer.Controls.Add(tableLayoutPanel);
            //заполнение сетки
            string[,] solutionGrid = solution.GetSolutionGrid();
            foreach (Word word in words)
            {
                if (word.GetDirection().Equals(Direction.Horizontal))
                {
                    for (int i = word.GetJ(), j = 0; i < word.GetJ() + word.GetNotion().Length; i++, j++)
                    {
                        textBoxes[i, word.GetI()].BackColor = Color.LightBlue;
                        textBoxes[i, word.GetI()].Tag = word.GetNotion().ElementAt(j).ToString();
                        textBoxes[i, word.GetI()].Enabled = true;
                        if (!solutionGrid[i, word.GetI()].Equals(""))
                        {
                            textBoxes[i, word.GetI()].Text = solutionGrid[i, word.GetI()];
                        }
                    }
                    listBoxHor.Items.Add(dictionary[word.GetNotion()]);
                }
                else
                {
                    for (int i = word.GetI(), j = 0; i < word.GetI() + word.GetNotion().Length; i++, j++)
                    {
                        textBoxes[word.GetJ(), i].BackColor = Color.LightBlue;
                        textBoxes[word.GetJ(), i].Tag = word.GetNotion().ElementAt(j).ToString();
                        textBoxes[word.GetJ(), i].Enabled = true;
                        if (!solutionGrid[word.GetJ(), i].Equals(""))
                        {
                            textBoxes[word.GetJ(), i].Text = solutionGrid[word.GetJ(), i];
                        }
                    }
                    listBoxVert.Items.Add(dictionary[word.GetNotion()]);
                }
            }
            helpCount = solution.GetHelpCount();
            labelHelpCount.Text = helpCount > 1 ? helpCount.ToString() : "1";
            tableContainer.Update();
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            if (!isSolution)
            {
                InitializeCrwd();
            }
            else
            {
                InitializeSolution();
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private int indS1 = -1, indF1 = -1;
        //выбрано пояснение из списка "по горизонтали"
        private void listBoxHor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxHor.SelectedIndex != -1)
            {
                string dif = listBoxHor.SelectedItem.ToString();
                string notion = FindKey(dif);
                Word word = grid.GetWord(notion);
                if (indS1 != -1 && indF1 != -1)
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (textBoxes[i, j].Enabled && textBoxes[i, j].BackColor.Equals(Color.Yellow))
                            {
                                textBoxes[i, j].BackColor = Color.LightBlue;
                            }
                        }
                    }
                }
                for (int i = word.GetJ(); i < word.GetJ() + word.GetNotion().Length; i++)
                {
                    textBoxes[i, word.GetI()].BackColor = Color.Yellow;
                }
                indS1 = word.GetJ();
                indF1 = word.GetJ() + word.GetNotion().Length;
            }

        }

        //private int indS2 = -1, indF2 = -1;
        //выбрано пояснение из списка "по вертикали"
        private void listBoxVert_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxVert.SelectedIndex != -1)
            {
                string dif = listBoxVert.SelectedItem.ToString();
                string notion = FindKey(dif);
                Word word = grid.GetWord(notion);
                if (indS1 != -1 && indF1 != -1)
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (textBoxes[i, j].Enabled && textBoxes[i, j].BackColor.Equals(Color.Yellow))
                            {
                                textBoxes[i, j].BackColor = Color.LightBlue;
                            }
                        }
                    }
                }
                for (int i = word.GetI(); i < word.GetI() + word.GetNotion().Length; i++)
                {
                    textBoxes[word.GetJ(), i].BackColor = Color.Yellow;
                }
                indS1 = word.GetJ();
                indF1 = word.GetJ() + word.GetNotion().Length;
            }
        }

        private void FormUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            formMain.Visible = true;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //formMain.Visible = true;
            Close();
        }

        private void buttonSaveSolution_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Maximum = width * height;
            string[,] charGrid = new string[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    charGrid[j, i] = textBoxes[j, i].Text;
                    progressBar1.PerformStep();
                }
            }
            Solution solution = new Solution(dictionary, grid, charGrid, helpCount);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CrosswordSolution |*.slt";
            saveFileDialog.Title = "Сохранить кроссворд";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                Stream s = new FileStream(saveFileDialog.FileName, FileMode.Create);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(s, solution);
                MessageBox.Show("Решение сохранено", "Сохранение", MessageBoxButtons.OK,
                                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                s.Close();
            }

        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            if (helpCount > 0)
            {
                helpCount--;
                labelHelpCount.Text = helpCount.ToString();
                Random rnd = new Random();
                Word word = words.ElementAt(rnd.Next(words.Count));
                string notion = word.GetNotion();
                if (word.GetDirection().Equals(Direction.Horizontal))
                {
                    for (int i = word.GetJ(), j = 0; i < word.GetJ() + notion.Length; i++, j++)
                    {
                        textBoxes[i, word.GetI()].Text = notion[j].ToString();
                    }
                }
                else
                {
                    for (int i = word.GetI(), j = 0; i < word.GetI() + notion.Length; i++, j++)
                    {
                        textBoxes[word.GetJ(), i].Text = notion[j].ToString();
                    }
                }


            }
        }

        private void listBoxHor_Click(object sender, EventArgs e)
        {
            listBoxVert.SelectedIndex = -1;
        }

        private void listBoxVert_Click(object sender, EventArgs e)
        {
            listBoxHor.SelectedIndex = -1;
        }

        private void buttonChangeCrwd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Crossword |*.crwd; *.slt";
            openFileDialog.Title = "Открыть кроссворд";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                Stream stream = new FileStream(openFileDialog.FileName, FileMode.Open);
                BinaryFormatter deserializer = new BinaryFormatter();
                textBoxСurrentCrwd.Text = openFileDialog.FileName;
                if (openFileDialog.FileName.EndsWith("slt"))
                {
                    isSolution = true;
                    solution = (Solution)deserializer.Deserialize(stream);
                    InitializeSolution();
                } 
                else
                {
                    isSolution = false;
                    deserializer.Deserialize(stream);
                    mainCross = (CrosswordCont)deserializer.Deserialize(stream);
                    InitializeCrwd();
                }
                stream.Close();
            }
        }

        //поиск ключа словаря по значению
        private string FindKey(string dif)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                if (dictionary.ElementAt(i).Value.Equals(dif))
                {
                    return dictionary.ElementAt(i).Key;
                }
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\nikit\Documents\GitHub\Crossword\index.html");
        }

        private void buttonCheckSolution_Click(object sender, EventArgs e)
        {
            if (CheckGrid())
            {
                pictureBoxUp.Visible = true;
                pictureBoxDown.Visible = false;
                MessageBox.Show("Красава!!!");
            }
            else
            {
                pictureBoxUp.Visible = false;
                pictureBoxDown.Visible = true;
            }
        }

        //проверка правильности разгадывания
        private bool CheckGrid()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    RichTextBox textBox = textBoxes[i, j];
                    string letter = textBox.Text;
                    if (textBox.Enabled.Equals(true))
                    {
                        if (!letter.Equals(textBox.Tag.ToString()))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
