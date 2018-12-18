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

namespace Crossword.Admin.CreateEditDict
{
    public partial class FormAddNotion : Form
    {
        public FormAddNotion()
        {
            InitializeComponent();
        }

        public FormAddNotion(string notion, string def)
        {
            InitializeComponent();
            textBoxNotion.Text = notion;
            richTextBoxDef.Text = def;
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru"));
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormAddNotion_FormClosing(object sender, FormClosingEventArgs e)
        {
            
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

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void textBoxNotion_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((keyChar < 'а' || keyChar > 'я') && (keyChar < 'А' || keyChar > 'Я'))
            {
                e.Handled = true;
            }
        }

        private void FormAddNotion_Load(object sender, EventArgs e)
        {

        }

        private void richTextBoxDef_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if ((keyChar < 'а' || keyChar > 'я') && (keyChar < 'А' || keyChar > 'Я') && !e.KeyChar.Equals(' '))
            {
                e.Handled = true;
            }
        }
    }
}
