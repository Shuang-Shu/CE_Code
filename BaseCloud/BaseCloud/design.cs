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
    public partial class design : Form
    {
        public design()
        {
            InitializeComponent();
            stageDataTran.parent = this;
            this.d_d = new design_design();
            this.d_p = new design_soil();
            this.d_s = new design_stage();

            d_d.TopLevel = false;
            d_d.Parent = panel1;
            d_p.TopLevel = false;
            d_p.Parent = panel1;
            panel1.Controls.Add(d_d);
            d_d.Show();
            d_s.TopLevel = false;
            d_s.Parent = panel1;

            soilData = this.d_p.dataGridView1;
        }

        // 导入所有信息
        public void importData(string dnum)
        {
            // 导入荷载信息
            string cmdStr = "SELECT * FROM loads WHERE dnum=\'" + dnum + "\'";
            SqlCommand cmd = new SqlCommand(cmdStr, myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            d_d.textBox7.Text = (string)reader[0];
            d_d.textBox5.Text = reader[1].ToString();
            d_d.textBox12.Text = reader[2].ToString();
            d_d.textBox13.Text = reader[3].ToString();
            d_d.textBox15.Text = reader[4].ToString();
            d_d.textBox16.Text = reader[5].ToString();
            reader.Close();
            // 导入基桩和承台信息
            cmdStr= "SELECT * FROM pileInfo WHERE dnum=\'" + dnum + "\'";
            cmd = new SqlCommand(cmdStr, myconn);
            reader = cmd.ExecuteReader();
            reader.Read();
            d_d.textBox8.Text = reader[10].ToString();
            d_d.comboBox1.Text = (string)reader[12];
            d_d.comboBox2.Text = (string)reader[11];

            d_s.textBox1.Text = reader[1].ToString();
            d_s.textBox2.Text = reader[2].ToString();
            d_s.textBox3.Text = reader[3].ToString();
            d_s.textBox4.Text = reader[4].ToString();
            d_s.textBox5.Text = reader[5].ToString();
            d_s.textBox6.Text = reader[6].ToString();
            d_s.textBox7.Text = reader[7].ToString();
            d_s.textBox8.Text = reader[8].ToString();
            d_s.textBox11.Text = reader[9].ToString();
            reader.Close();
            // 导入土层信息
            // 删除旧土层
            int rowsCount = d_p.dataGridView1.Rows.Count - 1;
            for (int i = 0; i < rowsCount; ++i)
                d_p.dataGridView1.Rows.RemoveAt(0);
            // 插入新数据
            cmdStr= "SELECT * FROM soilInfo WHERE dnum=\'" + dnum + "\'";
            cmd = new SqlCommand(cmdStr, myconn);
            reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                d_p.dataGridView1.Rows.Add();
                for (int i = 0; i < 7; ++i)
                {
                    d_p.dataGridView1.Rows[count].Cells[i].Value = reader[i + 1].ToString();
                }
                count++;
            }
            reader.Close();
            d_s.button2_Click(null, null);
            // 导入相应log
            cmdStr = "SELECT * FROM design WHERE dnum=\'" + dnum + "\'";
            cmd = new SqlCommand(cmdStr, myconn);
            reader = cmd.ExecuteReader();
            reader.Read();
            designData.dnum = dnum;
            designData.workno = (string)reader[1];
            designData.area = (string)reader[2];
            designData.describe = (string)reader[3];
            reader.Close();
        }

        public void connectDB(string addr, string userid, string pwd)
        {
            string constr = "server=" + addr + ";uid=" + userid + ";pwd=" + pwd + ";database=BaseCloud" + ";Connect Timeout=3";
            myconn = new SqlConnection(constr);
            myconn.Open();
        }
        
        private void 设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(d_d);
            d_d.Show();
        }

        private void 土层参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(d_p);
            d_p.Show();
        }

        private void 承台设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(d_s);
            d_s.Show();
        }
    }
}
