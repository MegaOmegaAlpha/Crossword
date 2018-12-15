using Crossword.Admin;
using Crossword.Admin.CreateEditDict;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            formMain.Visible = true;
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
                FormHandMadeCros formDict = new FormHandMadeCros(this, openFileDialog.FileName);
                formDict.Show();
                Visible = false;
            }
        }
    }
}
