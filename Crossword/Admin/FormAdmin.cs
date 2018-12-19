using Crossword.Admin;
using Crossword.Admin.CreateEditDict;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossword
{
    public partial class FormAdmin : Form
    {
        private FormMain formMain;
        public FormAdmin(FormMain formMain)
        {
            this.formMain = formMain;
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {           
            Close();
        }

        private void buttonOpenCreateCros_Click(object sender, EventArgs e)
        {
            FormBeforeCreate formBefore = new FormBeforeCreate(this);
            formBefore.Show();
            Visible = false;
        }

        private void buttonOpenCreateDict_Click(object sender, EventArgs e)
        {
            FormCreateDict formDict = new FormCreateDict(this);
            formDict.Show();
            Visible = false;
        }

        private void buttonOpenEditDict_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text |*.txt";
            openFileDialog.Title = "Открыть словарь";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                FormCreateDict formDict = new FormCreateDict(this, openFileDialog.FileName);
                formDict.Show();
                Visible = false;
            }
        }

        private void buttonOpenEditCros_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CrosswordFile |*.crwd";
            openFileDialog.Title = "Открыть кроссворд";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                bool check = true;
                FormHandMadeCros formDict = new FormHandMadeCros(this, openFileDialog.FileName, ref check);
                if (check)
                {
                    formDict.Show();
                    Visible = false;
                }
            }
        }

        private void FormAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            formMain.Visible = true;
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

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
