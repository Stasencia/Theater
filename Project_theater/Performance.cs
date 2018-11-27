﻿using MetroFramework.Forms;
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
    public partial class Performance : MetroForm
    {
        int id;
        public Performance(int perf_id)
        {
            InitializeComponent();
            id = perf_id;
        }

        private async void Performance_Load(object sender, EventArgs e)
        {
            Button[] b = new Button[35];
            for (int i = 0; i < 35; i++)
            {
                b[i] = new Button();
                b[i].Size = new Size(100, 100);
                b[i].Location = new Point(11 + (99 * (i % 7)), 830 + (99 * (int)Math.Floor(i / 7.0)));
                b[i].Text = (i + 1).ToString();
                b[i].FlatStyle = FlatStyle.Flat;
                b[i].TextAlign = ContentAlignment.TopLeft;
                b[i].BackgroundImageLayout = ImageLayout.Stretch;
                b[i].MouseEnter += new EventHandler(button_MouseEnter);
                b[i].MouseLeave += new EventHandler(button_MouseLeave);
                Controls.Add(b[i]);
            }
            panel2.Size = new Size(15, 15);
            panel2.Location = new Point(0, b[34].Bottom);
            panel2.BringToFront();
            AutoSize = false;
            Size = new Size(797, 530);
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM [Afisha] WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string s = DB_connection.current_directory + "images_afisha\\" + reader.GetValue(6).ToString();
                    panel1.BackgroundImage = new Bitmap(@s);
                    label1.Text = reader.GetValue(10).ToString();
                    label2.Text = reader.GetValue(7).ToString() + "\n" + reader.GetValue(8).ToString() + "\n" + reader.GetValue(9).ToString();
                    label7.Text = reader.GetValue(3).ToString();
                    label8.Text = reader.GetValue(4).ToString();
                    label9.Text = reader.GetValue(5).ToString();
                }
            }
        }
        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            p.Height = 100;
            p.Width = 100;
            p.Location = new Point(p.Location.X + 10, p.Location.Y + 10);
            p.BackgroundImage = null;
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            p.Height = 120;
            p.Width = 120;
            p.Location = new Point(p.Location.X - 10, p.Location.Y - 10);
            p.BackgroundImage = new Bitmap(@"C:\Users\Stasia\Desktop\uni_2018\Project_theater\Theater\Project_theater\Resources\111477-theatre.png");
            p.BringToFront();
        }

        private void Performance_Activated(object sender, EventArgs e)
        {
            panel1.Size = new Size(780, 380);
            AutoSize = false;
            Size = new Size(797, 530);
        }

        private void Performance_Shown(object sender, EventArgs e)
        {
            VerticalScroll.Value = 0;
        }
    }
}
