﻿using Crossword.Autentification;
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
                string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Пользователь\Documents\GitHub\Crossword\Crossword\bin\Debug\UsersDB.mdf;Integrated Security=True";
                sqlConnection = new SqlConnection(connect);
                sqlConnection.Open();
                labelConnect.ForeColor = Color.Green;
                labelConnect.Text = "Соединено";
                labelConnect.Visible = true;
                buttonLogIn.Enabled = true;
                buttonRegistration.Enabled = true;
            }
            catch (Exception)
            {
                labelConnect.ForeColor = Color.Red;
                labelConnect.Text = "Соединение отсутствует";
                labelConnect.Visible = true;
                MessageBox.Show("Проблемы с соединением. Требуется перезапуск", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPas.Text;
            if (!login.Equals("") && !password.Equals(""))
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT* FROM Users Where login ='" + login + "' " +
                    "and password ='" + password + "';", sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    bool isAdmin = (bool)reader["isAdmin"];
                    if (isAdmin)
                    {
                        FormAdmin formAdmin = new FormAdmin(this);
                        formAdmin.Show();
                    }
                    else
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "Crossword |*.crwd; *.slt";
                        openFileDialog.Title = "Открыть кроссворд";
                        openFileDialog.ShowDialog();
                        if (openFileDialog.FileName != "")
                        {
                            FormUser formUser = new FormUser(this, openFileDialog.FileName);
                            formUser.Show();
                            Visible = false;
                        }                       
                    }
                    textBoxLogin.Clear();
                    textBoxPas.Clear();
                    Visible = false;
                }
                else
                {
                    MessageBox.Show("Логин или пароль введен неверно", "Ошибка", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("КЛАССИЧЕСКИЙ КРОССВОРД!\nРазработчики ПО:\n 1. Байрамов Владимир Алексеевич\n 2. Мавлютов Владимир Дмитриевич\n 3. Перевозчиков Никита Дмитриевич \n 4. Фёдоров Сергей Владимирович\n Самарский университет\n Группа 6402-090301D", "АВТОРЫ");
        }

        
    }
}
