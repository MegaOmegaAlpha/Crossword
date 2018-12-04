using Crossword.Admin;
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
    }
}
