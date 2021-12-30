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
    public partial class Manger_mng : Form
    {
        public Manger_mng()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            design parent = stageDataTran.parent;
            string dnum = textBox1.Text;
            string cmdStr = "SELECT describe FROM design JOIN designer ON design.workerno=designer.workerno" +
            " WHERE designer.company = N\'"+stageDataTran.company+"\' AND design.dnum = \'"+dnum+"\'; ";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Errors[0].Message);
                //MessageBox.Show("检查设计号是否输入正确");
                return;
            }
            if (!reader.Read())
            {
                MessageBox.Show("检查设计号是否输入正确");
                reader.Close();
                return;
            }
            textBox2.Text = (string)reader[0];
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            design parent = stageDataTran.parent;
            string dnum = textBox1.Text;
            parent.d_d.deleteDnum(dnum);
            refreshData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            design parent = stageDataTran.parent;
            string cmdStr = "UPDATE design SET describe=N\'" + textBox2.Text + "\' WHERE dnum=\'" + textBox1.Text + "\';";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("出现错误");
                return;
            }
            refreshData();
        }

        public void refreshData()
        {
            SqlDataAdapter myAdapter = null;
            try
            {
                design parent = stageDataTran.parent;
                string cmdStr= "SELECT dnum AS 设计号, design.workerno AS 工号, area AS 地区, describe AS 描述, date0 AS 日期 FROM design JOIN designer ON design.workerno=designer.workerno WHERE company=N\'" + stageDataTran.company+"\';";
                myAdapter = new SqlDataAdapter(cmdStr, parent.myconn);
                DataSet myDataset = new DataSet();
                myAdapter.Fill(myDataset, "table");
                this.dataGridView1.DataSource = myDataset.Tables["table"];
                label1.Text = stageDataTran.company + "公司设计汇总";
            }
            catch
            {
                this.dataGridView1.DataSource = null;
            }
        }
    }
}
