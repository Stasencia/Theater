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
          //  DB_connection.connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Stasia\Desktop\uni_2018\Project_theater\Theater\Project_theater\DB_Theater.mdf;Integrated Security=True";
            DB_connection.current_directory = Environment.CurrentDirectory + "\\";
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
            Afisha form = new Afisha(this);
            form.Show();
            this.Hide();
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
    }
}
