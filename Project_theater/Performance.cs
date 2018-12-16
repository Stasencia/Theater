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
        int perf_id;
        Afisha a;
        public Performance(int perf_id, Afisha a, int Month_id)
        {
            InitializeComponent();
            this.perf_id = perf_id;
            this.a = a;
            this.Month_id = Month_id;
        }

        private void Button_customization()
        {
            DateTime d = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            while( d.DayOfWeek != DayOfWeek.Monday)
            {
                d = d.AddDays(-1);
            }
            for(int i = 0;i<42;i++)
            {
               Controls["b" + (i+1)].Text = d.Day.ToString();
                if (d.Month != DateTime.Today.Month)
                    Controls["b" + (i + 1)].Enabled = false;
               d = d.AddDays(1);
            }
        }

        private async void Performance_Load(object sender, EventArgs e)
        {
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
            Button[] b = new Button[42];
            for (int i = 0; i < 42; i++)
            {
                b[i] = new Button();
                b[i].Size = new Size(90, 90);
                b[i].Location = new Point(75 + (89 * (i % 7)), label9.Bottom + (89 * (int)Math.Floor(i / 7.0)));
                b[i].FlatStyle = FlatStyle.Flat;
                b[i].TextAlign = ContentAlignment.TopLeft;
                b[i].BackgroundImageLayout = ImageLayout.Stretch;
                b[i].MouseEnter += new EventHandler(button_MouseEnter);
                b[i].MouseLeave += new EventHandler(button_MouseLeave);
                b[i].Font = new Font("Century Gothic", 9, FontStyle.Regular);
                b[i].Name = "b" + (i + 1);
                Controls.Add(b[i]);
            }
            Button_customization();
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
            p.BackgroundImage = null;
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            p.Height = 110;
            p.Width = 110;
            p.Location = new Point(p.Location.X - 10, p.Location.Y - 10);
            p.BackgroundImage = new Bitmap(@"C:\Users\Stasia\Desktop\uni_2018\Project_theater\Theater\Project_theater\Resources\111477-theatre.png");
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
    }
}
