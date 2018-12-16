using Crossword.User;
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
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string pass = textBoxPassword.Text;
            string passY = textBoxPasswordYet.Text;
            if (!login.Equals("") && !pass.Equals("") && !passY.Equals(""))
            {
                if (login.Length > 5 && pass.Length > 5)
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
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Filter = "Crossword |*.crwd; *.slt";
                            openFileDialog.Title = "Открыть кроссворд";
                            openFileDialog.ShowDialog();
                            if (openFileDialog.FileName != "")
                            {
                                FormUser formUser = new FormUser(formMain, openFileDialog.FileName);
                                formUser.Show();
                                Visible = false;
                            }
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
                else
                {
                    MessageBox.Show("Слишком короткий логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void FormRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            formMain.Visible = true;
        }
    }
}
