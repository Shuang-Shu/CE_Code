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
    public partial class newDesigner : Form
    {
        public newDesigner(designInfo dif)
        {
            InitializeComponent();
            pre = dif;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            design parent = stageDataTran.parent;
            string wn = textBox1.Text;
            string name = textBox2.Text;
            string title = textBox3.Text;
            string major = textBox4.Text;

            string cmdStr = "INSERT INTO designer" +
            " VALUES(" +
            " \'"+wn+"\'," +
            " N\'"+name+"\'," +
            " N\'"+title+"\'," +
            " N\'"+major+"\'); ";

            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("检查参数是否满足规定");
            }
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
