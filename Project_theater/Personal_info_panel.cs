using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_theater
{
    public partial class Personal_info_panel : UserControl
    {
        public int userid;
        My_Account account;
        public Change_info f;
        public Personal_info_panel(My_Account ac)
        {
            account = ac;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f = new Change_info(userid,0);
            f.Changed += account.Fields_fill;
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f = new Change_info(userid, 1);
            f.Changed += account.Fields_fill;
            f.Show();
        }
    }
}
