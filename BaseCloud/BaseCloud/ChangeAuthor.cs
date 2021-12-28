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
    public partial class ChangeAuthor : Form
    {
        public ChangeAuthor()
        {
            InitializeComponent();
            comboBox1.Items.Add("管理");
            comboBox1.Items.Add("设计");
            comboBox1.Items.Add("删除");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string author = comboBox1.Text;
            if (author == "")
            {
                MessageBox.Show("请选择权限");
                return;
            }
            int para5 = 0;
            if (author == "管理")
                para5 = 1;
            else if(author=="删除")
                para5 = -1;
            design parent = stageDataTran.parent;
            string cmdStr = "SELECT * FROM designer WHERE workerno=\'" + stageDataTran.workerno + "\';";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string[] paras = new string[7];
            for (int k = 0; k < 7; ++k)
                if (k != 5)
                    paras[k] = (string)(reader[k]);
            reader.Close();
            try
            {
                cmdStr = "INSERT INTO tempDesigner VALUES " +
                                    "(\'" + paras[0] + "\'," +
                                    "N\'" + paras[1] + "\'," +
                                    "N\'" + paras[2] + "\'," +
                                    "N\'" + paras[3] + "\'," +
                                    "N\'" + paras[4] + "\'," +
                                    para5.ToString() + "," +
                                    "N\'" + paras[6] + "\'" +
                                    "); ";
                cmd = new SqlCommand(cmdStr, parent.myconn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("上次的申请尚未处理，请等待处理完成");
                return;
            }
            this.Close();
        }
    }
}
