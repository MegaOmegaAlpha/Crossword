using Crossword.Autentification;
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

namespace Crossword
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Repository\Crossword\Crossword\UsersDB.mdf;Integrated Security=True";
                sqlConnection = new SqlConnection(connect);
                sqlConnection.Open();
                labelConnect.ForeColor = Color.Green;
                labelConnect.Text = "Соединено";
                labelConnect.Visible = true;
            }
            catch(Exception)
            {
                labelConnect.ForeColor = Color.Red;
                labelConnect.Text = "Соединение отсутствует";
                labelConnect.Visible = true;
                MessageBox.Show("Проблемы с соединением. Требуется перезапуск", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPas.Text;
            if (!login.Equals("") && !password.Equals(""))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT* FROM Users Where login ='" + login + "' " +
                    "and password ='" + password + "' and isAdmin = 1;", sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    FormAdmin formAdmin = new FormAdmin(this);
                    formAdmin.Show();
                    textBoxLogin.Clear();
                    textBoxPas.Clear();
                    Visible = false;
                }
                else
                {
                    MessageBox.Show("Логин или пароль введен неверно", "Ошибка", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 
                        MessageBoxOptions.DefaultDesktopOnly);
                }
                reader.Close();
            }
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            FormRegistration registration = new FormRegistration(sqlConnection, this);
            registration.Show();
            Visible = false;
        }
    }
}
