using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseCloud
{
    public partial class ChangeDB : Form
    {
        public ChangeDB(register pre0)
        {
            InitializeComponent();
            pre = pre0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
             pre.dbAddr = textBox1.Text;
            pre.dbId = textBox2.Text;
            pre.dbPwd = textBox3.Text;
            pre.connect2DB();
            this.Close();
        }
    }
}
