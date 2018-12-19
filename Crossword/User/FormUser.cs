using Crossword.Admin;
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
        bool isLoaded;

        public FormUser(FormMain formMain, string fileName, ref bool check)
        {
            InitializeComponent();
            this.formMain = formMain;
            this.fileName = fileName;
            Stream stream = new FileStream(fileName, FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();
            textBoxСurrentCrwd.Text = fileName;
            try
            {               
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
                isLoaded = true;
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                MessageBox.Show("Структура файла нарушена", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                stream.Close();
                isLoaded = false;
                check = false;
                Close();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Структура файла нарушена", "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                stream.Close();
                isLoaded = false;
                check = false;
                Close();
            }
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru"));
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
            if (isLoaded)
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
                float fontSize = ScaleFont();
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
                        textBoxes[col, row].KeyPress += KeyPress;
                        textBoxes[col, row].SelectionAlignment = HorizontalAlignment.Center;
                        textBoxes[col, row].Font = new Font(textBoxes[col, row].Font.FontFamily, fontSize, FontStyle.Regular);
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
        }

        private void InitializeSolution()
        {
            if (isLoaded)
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
                float fontSize = ScaleFont();
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
                        textBoxes[col, row].KeyPress += KeyPress;
                        textBoxes[col, row].SelectionAlignment = HorizontalAlignment.Center;
                        textBoxes[col, row].Font = new Font(textBoxes[col, row].Font.FontFamily, fontSize, FontStyle.Regular);
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
                labelHelpCount.Text = helpCount.ToString();
                tableContainer.Update();
            }
        }

        private float ScaleFont()
        {
            float fontSize = 10f;
            float coeff = 2.14f;
            switch (width)
            {
                case 7:
                    return fontSize + coeff * 14;
                case 8:
                    return fontSize + coeff * 13;
                case 9:
                    return fontSize + coeff * 12;
                case 10:
                    return fontSize + coeff * 11;
                case 11:
                    return fontSize + coeff * 10;
                case 12:
                    return fontSize + coeff * 9;
                case 13:
                    return fontSize + coeff * 8;
                case 14:
                    return fontSize + coeff * 7;
                case 15:
                    return fontSize + coeff * 6;
                case 16:
                    return fontSize + coeff * 5;
                case 17:
                    return fontSize + coeff * 4;
                case 18:
                    return fontSize + coeff * 3;
                case 19:
                    return fontSize + coeff * 2;
                case 20:
                    return fontSize + coeff * 1;
            }
            return 0;
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

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if (keyChar < 'А' || 'Я' < keyChar)
            {
                e.Handled = true;
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
            Enabled = false;
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
            Enabled = true;
        }

        private bool checkWord(Word word)
        {
            bool check = false;
            if (word.GetDirection().Equals(Direction.Vertical))
            {
                for (int i = word.GetI(), j = 0; i < word.GetI() + word.GetNotion().Length; i++, j++)
                {
                    if (textBoxes[word.GetJ(), i].Text.Equals(textBoxes[word.GetJ(), i].Tag.ToString()))
                    {
                        check = true;
                    } 
                    else
                    {
                        check = false;
                    }
                }
            }
            else
            {
                for (int i = word.GetJ(), j = 0; i < word.GetJ() + word.GetNotion().Length; i++, j++)
                {
                    if (textBoxes[i, word.GetI()].Text.Equals(textBoxes[i, word.GetI()].Tag.ToString()))
                    {
                        check = true;
                    }
                    else
                    {
                        check = false;
                    }
                }
            }
            return check;
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            if (helpCount > 0 && (listBoxHor.SelectedIndex > -1 || listBoxVert.SelectedIndex > -1))
            {
                if (listBoxVert.SelectedIndex > -1)
                {
                    string notion = FindKey(listBoxVert.SelectedItem.ToString());
                    Word word = grid.GetWord(notion);
                    if (!checkWord(word))
                    {
                        for (int i = word.GetI(), j = 0; i < word.GetI() + notion.Length; i++, j++)
                        {
                            textBoxes[word.GetJ(), i].Text = notion[j].ToString();
                        }
                        helpCount--;
                    }
                }
                else
                {
                    string notion = FindKey(listBoxHor.SelectedItem.ToString());
                    Word word = grid.GetWord(notion);
                    if (!checkWord(word))
                    {
                        for (int i = word.GetJ(), j = 0; i < word.GetJ() + notion.Length; i++, j++)
                        {
                            textBoxes[i, word.GetI()].Text = notion[j].ToString();
                        }
                        helpCount--;
                    }
                }
                labelHelpCount.Text = helpCount.ToString();
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
                try
                {
                    if (openFileDialog.FileName.EndsWith("slt"))
                    {
                        isSolution = true;
                        solution = (Solution)deserializer.Deserialize(stream);
                        isLoaded = true;
                        InitializeSolution();
                    }
                    else
                    {
                        isSolution = false;
                        deserializer.Deserialize(stream);
                        mainCross = (CrosswordCont)deserializer.Deserialize(stream);
                        isLoaded = true;
                        InitializeCrwd();
                    }
                    buttonHelp.Enabled = true;
                    buttonSaveSolution.Enabled = true;
                    pictureBoxUp.Visible = false;
                    pictureBoxDown.Visible = false;
                    stream.Close();
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    MessageBox.Show("Структура файла нарушена", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    stream.Close();
                    isLoaded = false;
                }
                catch(IndexOutOfRangeException)
                {
                    MessageBox.Show("Структура файла нарушена", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    stream.Close();
                    isLoaded = false;
                }
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
            try
            {
                Process.Start(@"D:\Repository\Crossword\index.html");
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("Файл справки не найден", "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonCheckSolution_Click(object sender, EventArgs e)
        {
            if (CheckGrid())
            {
                pictureBoxUp.Visible = true;
                pictureBoxDown.Visible = false;
                buttonHelp.Enabled = false;
                buttonSaveSolution.Enabled = false;
                MessageBox.Show("Красава!!!");
                File.Delete(fileName);
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
