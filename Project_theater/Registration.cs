using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using MetroFramework;

namespace Project_theater
{
    public partial class Registration : MetroForm
    {
        int ID = 0;

        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm f = new MainForm(ID+1);
            f.Show();
            this.Hide();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            
        }

        private async void metroButton1_Click(object sender, EventArgs e)
        {   
            if (!string.IsNullOrWhiteSpace(metroTextBox1.Text) && !string.IsNullOrEmpty(metroTextBox1.Text) &&
            !string.IsNullOrWhiteSpace(metroTextBox2.Text) && !string.IsNullOrEmpty(metroTextBox2.Text))
            {
                using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand selection_com = new SqlCommand("SELECT * FROM Users", connection);
                    SqlDataReader reader = await selection_com.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ID = int.Parse(reader.GetValue(0).ToString());
                            if (reader.GetValue(1).ToString() == metroTextBox1.Text)
                            {
                                ID = 0;
                                break;
                            }
                        }
                    }  
                    reader.Close();
                }
                using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
                {
                    await connection.OpenAsync();
                    if (ID != 0)
                    {
                        SqlCommand command = new SqlCommand("INSERT INTO [Users] (Login, Password) VALUES(@Login, @Password)", connection);
                        command.Parameters.AddWithValue("Login", metroTextBox1.Text);
                        command.Parameters.AddWithValue("Password", metroTextBox2.Text);
                        await command.ExecuteNonQueryAsync();
                        MetroMessageBox.Show(this, "Вы были успешно зарегистрированы!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 120);  
                    }
                    else
                        MetroMessageBox.Show(this, "Данный логин уже занят", "Значение логина", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 120);
                }
                
            }
            else
                MetroMessageBox.Show(this, "Заполните все необходимые поля", "Ошибка заполнения", MessageBoxButtons.OK, MessageBoxIcon.Error, 120);   
        } 
    }
}
