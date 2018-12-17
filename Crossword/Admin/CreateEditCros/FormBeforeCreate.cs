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

namespace Crossword.Admin
{
    public partial class FormBeforeCreate : Form
    {
        private FormAdmin formAdmin;
        public FormBeforeCreate(FormAdmin formAdmin)
        {
            this.formAdmin = formAdmin;
            InitializeComponent();
        }

        private void FormCreateCros_Load(object sender, EventArgs e)
        {
            
        }
        private int width, height;
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (textBoxFile.Text != "")
            {
                width = int.Parse(numericUpDownWidth.Value.ToString());
                height = int.Parse(numericUpDownHeight.Value.ToString());
                FormHandMadeCros form = new FormHandMadeCros(this, width, height, textBoxFile.Text);
                form.Show();
                Visible = false;
            }
            else
            {
                MessageBox.Show("Укажите файл словаря", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonGen_Click(object sender, EventArgs e)
        {

        }

        private void FormBeforeCreate_FormClosing(object sender, FormClosingEventArgs e)
        {
            formAdmin.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\nikit\Documents\GitHub\Crossword\index.html");
        }

        private void buttonDir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text |*.txt";
            openFileDialog.Title = "Открыть словарь";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != null)
            {
                textBoxFile.Text = openFileDialog.FileName;
            }
        }
    }
}
