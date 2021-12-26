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
    public partial class InsertSoil : Form
    {
        public InsertSoil(design_soil pre0)
        {
            InitializeComponent();
            pre = pre0;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string area = textBox1.Text;
            string soilType = textBox2.Text;
            string soil = textBox3.Text;

            double[] para = new double[5];
            try
            {
                para[0] = double.Parse(pre.textBox1.Text);
                para[1] = double.Parse(pre.textBox2.Text);
                para[2] = double.Parse(pre.textBox4.Text);
                para[3] = double.Parse(pre.textBox5.Text);
                para[4] = double.Parse(pre.textBox6.Text);
            }
            catch
            {
                MessageBox.Show("检查土层参数的合法性");
                return;
            }

            // 参数
            design parent = stageDataTran.parent;
            string cmdStr = "INSERT INTO soil VALUES("+
                "N\'"+area+"\',"+
                "N\'" + soilType + "\'," +
                "N\'" + soil + "\'," +
                para[0].ToString()+","+
                para[1].ToString() + "," +
                para[2].ToString() + "," +
                para[3].ToString() + "," +
                para[4].ToString() +
                "); ";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("来自数据库的通知:"+ex.Errors[1].Message);
                return;
            }
            pre.refreshSoilType(null, null);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
