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
            Process.Start(@"C:\Users\nikit\Documents\GitHub\Crossword\index.html");
        }
    }
}
