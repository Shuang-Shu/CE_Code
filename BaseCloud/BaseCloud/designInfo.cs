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
            designData.workno = comboBox1.Text;
            designData.area = textBox2.Text;
            designData.describe = textBox3.Text;
            pre.textBox7.Text = textBox1.Text;
            this.Hide();
        }

        public void refreshDesigner()
        {
            comboBox1.Items.Clear();
            design parent = stageDataTran.parent;

            string cmdStr = "SELECT workerno, wname FROM designer;";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader[0]+" "+reader[1]);
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ndsg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
