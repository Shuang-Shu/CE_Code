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
    public partial class confirm : Form
    {
        public confirm(loadDesign pre0)
        {
            InitializeComponent();
            pre = pre0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dnum = pre.comboBox2.Text;
            stageDataTran.parent.importData(dnum);
            this.Hide();
            pre.Hide();
        }
    }
}
