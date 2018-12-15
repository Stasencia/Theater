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
    public enum Months
    {
        Январь = 1, Февраль, Март, Апрель, Май, Июнь, Июль, Август, Сентябрь, Октябрь, Ноябрь, Декабрь
    }

    public partial class Afisha : MetroForm
    {
        Performance_list f;
        public Afisha()
        {
            InitializeComponent();
        }

        private void Afisha_Load(object sender, EventArgs e)
        {
            int i = 0;
            Months m;
            f = new Performance_list();
            f.MdiParent = this;
            f.Top = metroPanel1.Bottom;
            f.Show();
            f.Performance_Create();
            using (SqlConnection connection = new SqlConnection(DB_connection.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT DISTINCT TOP 4 MONTH(Date), YEAR(Date) AS x FROM Afisha_dates WHERE(MONTH(Date) >= MONTH(GETDATE()) AND YEAR(Date) >= YEAR(GETDATE())) OR YEAR(Date) > YEAR(GETDATE()) ORDER BY x", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Button b = new Button();
                        b.FlatStyle = FlatStyle.Flat;
                        m = (Months)reader.GetInt32(0);
                        b.Text = m + "\n" + reader.GetValue(1).ToString();
                        b.AutoSize = false;
                        b.Size = new Size(82, 49);
                        b.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                        b.Tag = reader.GetValue(0);
                        b.BringToFront();
                        b.Click += new System.EventHandler(this.button1_Click);
                        panel1.Controls.Add(b);
                        i++;
                    }
                }
                int j = 0;
                int k = (797 - (82 * i + (i - 1) * 22)) / 2;
                foreach(Button b in panel1.Controls)
                {
                    panel1.Controls[j].Location = new Point(k + j * 82 + j * 22, 7);
                    j++;
                }
            } 
        }

        private void Afisha_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.Month_id = Convert.ToInt32(((Control)sender).Tag);
            f.Refresh(Convert.ToInt32(((Control)sender).Tag));
            f.Show();
        }
    }
}
