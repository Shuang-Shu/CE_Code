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
    public partial class designInfo : Form
    {
        public designInfo(design_design pre0)
        {
            InitializeComponent();
            ndsg = new newDesigner(this);
            pre = pre0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            designData.dnum = textBox1.Text;
            designData.area = textBox2.Text;
            designData.describe = textBox3.Text;
            pre.textBox7.Text = textBox1.Text;
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
