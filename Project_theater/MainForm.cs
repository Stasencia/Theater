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
    public partial class MainForm : MetroForm
    {
        //public User user = new User();
        public MainForm()
        {
            InitializeComponent(); 
        }

        public MainForm(int k)
        {
            InitializeComponent();
            if (k>0)
            {
                User.ID = k;
                metroLabel1.Visible = false;
                metroLabel2.Visible = false;
                metroLabel7.Text = "Личный кабинет";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            metroLabel6.Text = "Театр имени \nОльги Кобылянской";
            DB_connection.connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Stasia\Desktop\uni_2018\Project_theater\Theater\Project_theater\DB_Theater.mdf;Integrated Security=True";
            DB_connection.current_directory = Environment.CurrentDirectory + "\\";
            if(User.Right == 1)
            {
                label1.Text = "Редактирование афиши";
                label1.AutoSize = true;
                label1.Location = new Point(label1.Location.X-110,label1.Location.Y);
            }
        }

        private void metroLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            Registration form = new Registration();
            form.Show();
            this.Hide();
        }

        private void metroLabel2_MouseClick(object sender, MouseEventArgs e)
        {
            Authorization form = new Authorization();
            form.Show();
            this.Hide();
        }

        private void metroLabel3_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void metroLabel7_MouseClick(object sender, MouseEventArgs e)
        {
            My_Account form = new My_Account();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (User.Right == 1)
            {
                Editing form = new Editing(this);
                form.Show();
            }
            else
            {
                Afisha form = new Afisha(this);
                form.Show();
            }
            this.Hide();
        }
    }
}
