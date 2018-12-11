using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossword.Autentification
{
    public partial class FormRegistration : Form
    {
        SqlConnection sqlConnection;
        FormMain formMain;
        public FormRegistration(SqlConnection sqlConnection, FormMain formMain)
        {
            this.sqlConnection = sqlConnection;
            this.formMain = formMain;
            InitializeComponent();
        }

        private void FormRegistration_Load(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
            formMain.Visible = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string pass = textBoxPassword.Text;
            string passY = textBoxPasswordYet.Text;
            if (!login.Equals("") && !pass.Equals("") && !passY.Equals(""))
            {
                if (pass.Equals(passY))
                {
                    SqlCommand sqlCommand = new SqlCommand("Select* from Users Where login = '" + login + "';", 
                        sqlConnection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        sqlCommand = new SqlCommand("Insert into Users (login, password, isAdmin) " +
                            "values ('" + login + "', '" + pass +
                            "', 0);", sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Такой пользователь уже есть", "Ошибка", MessageBoxButtons.OK, 
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }
    }
}
