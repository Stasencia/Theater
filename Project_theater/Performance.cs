using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_theater
{
    public partial class Performance : MetroForm
    {
        public Performance()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button[] b = new Button[35];
            for (int i = 0; i < 35; i++)
            {
                b[i] = new Button();
                b[i].Size = new Size(80, 80);
                b[i].Location = new Point(129 + (79 * (i%7)), 138 + (79 * (int)Math.Floor(i/7.0)));
                b[i].Text = (i + 1).ToString();
                b[i].FlatStyle = FlatStyle.Flat;
                b[i].TextAlign = ContentAlignment.TopLeft;
                b[i].BackgroundImageLayout = ImageLayout.Stretch;
                b[i].MouseEnter += new EventHandler(button_MouseEnter);
                b[i].MouseLeave += new EventHandler(button_MouseLeave);
                Controls.Add(b[i]);
            }
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            p.Height = 80;
            p.Width = 80;
            p.Location = new Point(p.Location.X + 10, p.Location.Y + 10);
            p.BackgroundImage = null;
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            Button p = (Button)sender;
            p.Height = 100;
            p.Width = 100;
            p.Location = new Point(p.Location.X - 10, p.Location.Y - 10);
            p.BackgroundImage = new Bitmap(@"C:\Users\Stasia\Desktop\uni_2018\Project_theater\Theater\Project_theater\Resources\111477-theatre.png");
            p.BringToFront();
        }

        private void Afisha_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
