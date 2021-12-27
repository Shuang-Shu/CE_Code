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
        public ChangeDB()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dbAddr = textBox1.Text;
            string dbId = textBox2.Text;
            string dbPwd = textBox3.Text;
            this.Close();
        }
    }
}
