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
    public partial class Ticket_purchase : MetroForm
    {
        public Ticket_purchase()
        {
            InitializeComponent();
        }

        private void Ticket_purchase_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
        }
    }
}
