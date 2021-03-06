﻿using MetroFramework;
using MetroFramework.Forms;
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

namespace Project_theater
{
    public partial class Ticket_purchase : MetroForm
    {
        Performance perf_form;
        int perf_info_id;
        float price;

        public Ticket_purchase(Performance form, int info)
        {
            InitializeComponent();
            perf_form = form;
            perf_info_id = info;
        }

        private void Ticket_purchase_FormClosing(object sender, FormClosingEventArgs e)
        {
            perf_form.Show();
        }

        private void Price_count()
        {
            price = 0;
            for(int i = 0; i < panel2.Controls.Count; i++)
            { 
                if(panel2.Controls["button" + (i + 1)].BackColor == Color.MediumTurquoise)
                {
                    if (i < 44)
                        price += Performance_class.Price;
                    else
                        price += Performance_class.Price - 10;
                    if (checkBox1.Checked)
                        price = price * (float)0.75;
                }
            }
            label4.Text = "Цена: " + price + " грн.";
            if (price == 0)
            {
                button77.Enabled = false;
                button77.BackColor = Color.DarkGray;
            }
        }

        private async void Ticket_purchase_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT Afisha_dates.Date, Afisha_dates.Time, Afisha.Duration, Afisha.Image, Afisha.Id, Afisha.Price FROM Afisha_dates LEFT JOIN Afisha ON Afisha_dates.Id_performance = Afisha.Id WHERE Afisha_dates.Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", perf_info_id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {    
                    Performance_class.Date = Convert.ToDateTime(reader.GetValue(0));
                    Performance_class.Id = Convert.ToInt32(reader.GetValue(4));
                    Performance_class.Price = Convert.ToSingle(reader.GetValue(5));
                    label5.Text = "Дата: " + Performance_class.Date.ToShortDateString();
                    label1.Text = "Начало: " + reader.GetValue(1).ToString();
                    label2.Text = reader.GetValue(2).ToString();
                    string s = DB_connection.current_directory + "images_afisha\\" + reader.GetValue(3).ToString();
                    panel1.BackgroundImage = new Bitmap(@s);
                }
            }
            foreach(Button b in panel2.Controls)
            {
                b.TabStop = false;
                b.Click += new System.EventHandler(this.button_Click);
                b.BackColor = Color.White;
            }
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT Seat FROM Tickets WHERE Date = @Date", connection);
                command.Parameters.AddWithValue("@Date", Performance_class.Date);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        panel2.Controls["button" + (Convert.ToInt32((reader.GetValue(0))) + 1)].BackColor = Color.DarkGray;
                        panel2.Controls["button" + (Convert.ToInt32((reader.GetValue(0))) + 1)].Enabled = false;
                    }
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.White)
                ((Button)sender).BackColor = Color.MediumTurquoise;
            else
                ((Button)sender).BackColor = Color.White;
            label1.Select();
            button77.Enabled = true;
            button77.BackColor = Color.MediumTurquoise;
            Price_count();
        }

        private async void button77_Click(object sender, EventArgs e)
        {
            int k = 0;
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                for (int i = 0; i < panel2.Controls.Count; i++)
                {
                    if (panel2.Controls["button" + (i+1)].BackColor == Color.MediumTurquoise)
                    {
                        SqlCommand command = new SqlCommand("INSERT INTO [Tickets] (User_Id, Performance_Id, Date, Seat, Price) VALUES(@User, @Perf, @Date, @Seat, @Price)", connection);
                        command.Parameters.AddWithValue("@User", User.ID);
                        command.Parameters.AddWithValue("@Perf", Performance_class.Id);
                        command.Parameters.AddWithValue("@Date", Performance_class.Date);
                        command.Parameters.AddWithValue("@Seat", i);
                        command.Parameters.AddWithValue("@Price", price);
                        await command.ExecuteNonQueryAsync();
                        k++;
                    }

                }
                if(k!=0)
                    MetroMessageBox.Show(this, "Билеты были успешно заказаны!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 100);
                    
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Price_count();
        }
    }
}
