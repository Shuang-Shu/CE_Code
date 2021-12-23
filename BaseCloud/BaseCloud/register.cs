using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BaseCloud
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
            dsgn = new design();
            mng = new manager();
            textBox3.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string addr = textBox1.Text;
            string userid = textBox2.Text;
            string pwd = textBox3.Text;
            design parent = stageDataTran.parent;
            try
            {
                dsgn.connectDB(addr, userid, pwd);
            }
            catch
            {
                MessageBox.Show("连接失败!");
                return;
            }
            parent.d_p.readDB();
            this.Hide();
            mng.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string addr = textBox1.Text;
            string userid = textBox2.Text;
            string pwd = textBox3.Text;
            design parent = stageDataTran.parent;
            try
            {
                dsgn.connectDB(addr, userid, pwd);
            }
            catch
            {
                MessageBox.Show("连接失败!");
                return;
            }
            parent.d_p.readDB();
            this.Hide();
            dsgn.Show();
        }
    }
}
