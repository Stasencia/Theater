using MetroFramework;
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
    public partial class Performance : MetroForm
    {
        public int Month_id { get; set; }
        int perf_id, perf_info_id;
        Afisha a;
        public Performance(int perf_id, Afisha a, int Month_id)
        {
            InitializeComponent();
            this.perf_id = perf_id;
            this.a = a;
            this.Month_id = Month_id;
        }

        private async void Button_customization(object sender, EventArgs args)
        {
            string[] words = ((Control)sender).Tag.ToString().Split(';');
            DateTime d = new DateTime(Convert.ToInt32(words[1]), Convert.ToInt32(words[0]), 1);
            while (d.DayOfWeek != DayOfWeek.Monday)
            {
                d = d.AddDays(-1);
            }
            for (int i = 0;i<42;i++)
            {
                Controls["b" + (i + 1)].Text = d.Day.ToString();
                Controls["b" + (i + 1)].Enabled = false;
                Controls["b" + (i + 1)].BackgroundImage = null;
                Controls["b" + (i + 1)].Tag = d.Year + "-" + d.Month + "-" + d.Day;
                d = d.AddDays(1);
            }
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT Id_performance, Date, DAY(Date) AS day, Afisha.Small_image FROM [Afisha_dates] LEFT JOIN[Afisha] ON Afisha_dates.Id_performance = Afisha.Id WHERE Id_performance = @Id AND MONTH(Date) = @month AND YEAR(Date) = @year", connection);
                command.Parameters.AddWithValue("@Id", perf_id);
                command.Parameters.AddWithValue("@month", Convert.ToInt32(words[0]));
                command.Parameters.AddWithValue("@year", Convert.ToInt32(words[1]));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        for (int i = 0; i < 42; i++)
                        {
                            string[] dateparts = Controls["b" + (i + 1)].Tag.ToString().Split('-');
                            if (dateparts[2] == reader.GetValue(2).ToString() && (Convert.ToInt32(dateparts[2]) >= DateTime.Now.Day || Convert.ToInt32(dateparts[0]) > DateTime.Now.Year) && dateparts[1] == (words[0]))
                            {
                                string s = DB_connection.current_directory + "images_afisha\\" + reader.GetValue(3).ToString();
                                Controls["b" + (i + 1)].Enabled = true;
                                Controls["b" + (i + 1)].BackgroundImage = new Bitmap(@s);
                            }
                        }
                    }
                } 
            }
            
        }

        private async void Performance_Load(object sender, EventArgs e)
        {
            Button push = new Button();
            AutoSize = false;
            Size = new Size(797, 530);
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM [Afisha] WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", perf_id);
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
            label9.Height = (int)(((label9.Text.Length * label9.Font.SizeInPoints) / label9.Width + label9.Text.Where(x=>x == '\n').Count()) * label9.Font.Height);
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                int i = 0;
                Months m;
                SqlCommand command2 = new SqlCommand("SELECT DISTINCT MONTH(Date), YEAR(Date) FROM Afisha_dates WHERE Id_performance = @Id AND ((MONTH(Date) >= MONTH(GETDATE()) AND YEAR(Date) >= YEAR(GETDATE())) OR YEAR(Date) > YEAR(GETDATE())) ORDER BY YEAR(Date)", connection);
                command2.Parameters.AddWithValue("@Id", perf_id);
                SqlDataReader reader2 = command2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        Button top = new Button();
                        top.FlatStyle = FlatStyle.Flat;
                        m = (Months)reader2.GetInt32(0);
                        top.Text = m + "\n" + reader2.GetValue(1).ToString();
                        top.AutoSize = false;
                        top.Size = new Size(82, 49);
                        top.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                        top.Tag = reader2.GetValue(0) + ";" + reader2.GetValue(1);
                        if (Convert.ToInt32(reader2.GetValue(0)) == Month_id)
                            push = top;
                        top.BringToFront();
                        top.Click += new System.EventHandler(this.Button_customization);
                        top.Name = "top" + (i + 1);
                        Controls.Add(top);
                        i++;
                    }
                }
                int k = (797 - (82 * i + (i - 1) * 22)) / 2;
                for (int j = 0; j < i; j++)
                {
                    Controls["top" + (j + 1)].Location = new Point(k + j * 82 + j * 22, label9.Bottom);
                }
            }
            Button[] b = new Button[42];
            for (int i = 0; i < 42; i++)
            {
                b[i] = new Button();
                b[i].Size = new Size(90, 90);
                b[i].Location = new Point(75 + (89 * (i % 7)), Controls["top1"].Bottom + 15 + (89 * (int)Math.Floor(i / 7.0)));
                b[i].FlatStyle = FlatStyle.Flat;
                b[i].TextAlign = ContentAlignment.TopLeft;
                b[i].BackgroundImageLayout = ImageLayout.Stretch;
                b[i].MouseEnter += new EventHandler(button_MouseEnter);
                b[i].MouseLeave += new EventHandler(button_MouseLeave);
                b[i].Font = new Font("Century Gothic", 9, FontStyle.Regular);
                b[i].Name = "b" + (i + 1);
                b[i].Click += new System.EventHandler(this.Day_pushed);
                Controls.Add(b[i]);
            }
            //push.Select();
            push.PerformClick();
            panel2.Size = new Size(15, 15);
            panel2.Location = new Point(0, b[41].Bottom);
            panel2.BringToFront();
        }
        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            p.Height = 90;
            p.Width = 90;
            p.Location = new Point(p.Location.X + 10, p.Location.Y + 10);
            p.Text = ((Button)sender).Tag.ToString().Split('-')[2];
            p.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            p.ForeColor = Color.Black;
            p.TextAlign = ContentAlignment.TopLeft;
        }

        private async void button_MouseEnter(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            p.Height = 110;
            p.Width = 110;
            p.Location = new Point(p.Location.X - 10, p.Location.Y - 10);
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM [Afisha] WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", perf_id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string s = DB_connection.current_directory + "images_afisha\\" + reader.GetValue(11).ToString();
                    p.BackgroundImage = new Bitmap(@s);
                }
            }
            string date = ((Button)sender).Tag.ToString();
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT Time, Id FROM Afisha_dates WHERE Id_performance = @Id AND Date = @date", connection);
                command.Parameters.AddWithValue("@Id", perf_id);
                command.Parameters.AddWithValue("@date", date);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    p.Text = reader.GetValue(0).ToString();
                    p.Font = new Font("Century Gothic", 11, FontStyle.Bold);
                    p.ForeColor = Color.White;
                    p.TextAlign = ContentAlignment.BottomLeft;
                    perf_info_id = Convert.ToInt32(reader.GetValue(1));
                }
            }
            p.BringToFront();
        }

        private void Performance_Activated(object sender, EventArgs e)
        {
            panel1.Size = new Size(780, 380);
        }

        private void Performance_Shown(object sender, EventArgs e)
        {
            VerticalScroll.Value = 0;
        }

        private void Performance_FormClosing(object sender, FormClosingEventArgs e)
        {
            a.Show();
        }

        private void Day_pushed(object sender, EventArgs e)
        {
          /*  if (User.ID == 0) 
            {
                MetroMessageBox.Show(this, "Для того, чтобы заказать билеты, Вам необходимо быть авторизированным в системе", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 120);  
            }
            else*/
            {
                Ticket_purchase t = new Ticket_purchase(this, perf_info_id);
                t.Show();
                this.Hide();
            }
        }
    }
}
