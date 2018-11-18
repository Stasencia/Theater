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
    public partial class Afisha : MetroForm
    {
        Performance_list f;
        public Afisha()
        {
            InitializeComponent();
        }

        private void Performance_Load(object sender, EventArgs e)
        {
            f = new Performance_list();
            f.MdiParent = this;
            f.Top = metroPanel1.Bottom;
            f.Show();
        }

        private void Performance_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.Refresh(11);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f.Refresh(12);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            f.Refresh(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            f.Refresh(2);
        }
    }
}
