using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossword.Admin.CreateEditDict
{
    public partial class FormCreateDict : Form
    {

        private FormAdmin formAdmin;
        private Dictionary<string, string> dictionary;
        private string fileName;

        public FormCreateDict(FormAdmin formAdmin)
        {
            dictionary = new Dictionary<string, string>();
            this.formAdmin = formAdmin;
            InitializeComponent();
        }

        public FormCreateDict(FormAdmin formAdmin, string fileName)
        {
            dictionary = new Dictionary<string, string>();
            this.fileName = fileName;
            this.formAdmin = formAdmin;
            StreamReader streamReader = new StreamReader(fileName, Encoding.GetEncoding("Windows-1251"));
            InitializeComponent();
            try
            {                
                string dataFromFile = "";
                while (dataFromFile != null)
                {
                    dataFromFile = streamReader.ReadLine();
                    if (dataFromFile != null)
                    {
                        if (!dataFromFile.Equals(""))
                        {
                            string[] stringArr = dataFromFile.Split(' ');
                            string notion = stringArr[0];
                            string def = "";
                            for (int i = 1; i < stringArr.Length - 1; i++)
                            {
                                def += stringArr[i] + " ";
                            }
                            def += stringArr[stringArr.Length - 1];
                            dictionary.Add(notion.ToUpper(), def.ToLower());
                        }
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Понятие повторяется!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                streamReader.Close();
            }
        }

        private void updateGrid()
        {
            if (dictionary.Count != 0)
            {
                dataGridView.Rows.Clear();
                progressBar1.Value = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dictionary.Count;
                progressBar1.Step = 1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    dataGridView.Rows.Add();
                    dataGridView.Rows[i].Cells[0].Value = dictionary.ElementAt(i).Key;
                    dataGridView.Rows[i].Cells[1].Value = dictionary.ElementAt(i).Value;
                    progressBar1.PerformStep();
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormCreateDict_Load(object sender, EventArgs e)
        {
        }
        private FormAddNotion formAdd;
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            formAdd = new FormAddNotion();
            formAdd.Show();
            formAdd.buttonSave.Click += new EventHandler(buttonSaveNotion_Click);
            formAdd.FormClosing += FormAddEditNotion_Closing;
            Enabled = false;
        }

        private void FormAddEditNotion_Closing(object sender, FormClosingEventArgs e)
        {
            buttonAdd.Enabled = true;
            buttonEdit.Enabled = true;
            buttonDelete.Enabled = true;
            Enabled = true;
        }

        private void buttonSaveNotion_Click(object sender, EventArgs e)
        {
            try
            {
                string notion = formAdd.textBoxNotion.Text.ToUpper();
                string def = formAdd.richTextBoxDef.Text.ToLower();
                if (notion.Equals("") || def.Equals(""))
                {
                    MessageBox.Show("Поля не должны быть пустыми!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    dictionary.Add(notion, def);
                    dataGridView.Rows.Add();
                    dataGridView.Rows[dataGridView.Rows.Count - 2].Cells[0].Value = notion;
                    dataGridView.Rows[dataGridView.Rows.Count - 2].Cells[1].Value = def;
                    formAdd.Close();
                }
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Понятие повторяется!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private string oldNot;
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.Cells[0].Value != null)
            {
                string not = dataGridView.CurrentRow.Cells[0].Value.ToString();
                string def = dataGridView.CurrentRow.Cells[1].Value.ToString();
                oldNot = not;
                formAdd = new FormAddNotion(not, def);
                formAdd.buttonSave.Click += new EventHandler(buttonSaveEditedNotion_Click);
                formAdd.Show();
                Enabled = false; 
            }
        }

        private void buttonSaveEditedNotion_Click(object sender, EventArgs e)
        {
            try
            {
                string not = formAdd.textBoxNotion.Text.ToUpper();
                string def = formAdd.richTextBoxDef.Text.ToLower();
                if (not.Equals("") || def.Equals(""))
                {
                    MessageBox.Show("Поля не должны быть пустыми!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    if (oldNot.Equals(not))
                    {
                        dictionary[not] = def;
                        dataGridView.CurrentRow.Cells[1].Value = def;
                        formAdd.Close();
                    }
                    else
                    {
                        dictionary.Remove(oldNot);
                        dictionary.Add(not, def);
                        dataGridView.CurrentRow.Cells[0].Value = not;
                        dataGridView.CurrentRow.Cells[1].Value = def;
                        dataGridView.Rows.Add();
                        formAdd.Close();
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Понятие повторяется!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.Cells[0].Value != null)
            {
                string not = dataGridView.CurrentRow.Cells[0].Value.ToString();
                string def = dataGridView.CurrentRow.Cells[1].Value.ToString();
                dictionary.Remove(not);
                dataGridView.Rows.Remove(dataGridView.CurrentRow);
            }
        }

        private bool isSortLen;
        private void sortLen()
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < dictionary.Count; i++)
            {
                strings.Add(dictionary.ElementAt(i).Key);
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
            dataGridView.Rows.Clear();
            for (int i = 0; i < strings.Count; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells[0].Value = strings[i];
                dataGridView.Rows[i].Cells[1].Value = dictionary[strings[i]];
                progressBar1.PerformStep();
            }
        }

        private void buttonSortLen_Click(object sender, EventArgs e)
        {
            Enabled = false;
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = dictionary.Count;
            progressBar1.Step = 1;
            sortLen();
            Enabled = true;
        }

        private void buttonSaveDict_Click(object sender, EventArgs e)
        {
            Enabled = false;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text |*.txt";
            saveFileDialog.Title = "Сохранить словарь";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                Stream s = new FileStream(saveFileDialog.FileName, FileMode.Create);
                StreamWriter stream = new StreamWriter(s, Encoding.GetEncoding("Windows-1251"));
                progressBar1.Value = 0;
                progressBar1.Maximum = dictionary.Count;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    stream.WriteLine(dictionary.ElementAt(i).Key + " " + dictionary.ElementAt(i).Value);
                    progressBar1.PerformStep();
                }
                stream.Close();
            }
            Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBoxSearch.Text.Equals(""))
            {
                Enabled = false;
                dataGridView.Rows.Clear();
                progressBar1.Value = 0;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = dictionary.Count;
                progressBar1.Step = 1;
                for (int i = 0, j = 0; i < dictionary.Count; i++)
                {
                    string key = dictionary.ElementAt(i).Key;
                    if (key.StartsWith(textBoxSearch.Text.ToUpper()))
                    {
                        dataGridView.Rows.Add();
                        dataGridView.Rows[j].Cells[0].Value = key;
                        dataGridView.Rows[j].Cells[1].Value = dictionary.ElementAt(i).Value;
                        j++;
                    }
                    progressBar1.PerformStep();
                }
                Enabled = true;
            }  
            else
            {
                updateGrid();
            }
        }

        private void FormCreateDict_FormClosing(object sender, FormClosingEventArgs e)
        {
            formAdmin.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void FormCreateDict_Shown(object sender, EventArgs e)
        {
            updateGrid();
        }
    }
}
