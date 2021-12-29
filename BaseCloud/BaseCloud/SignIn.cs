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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("设计者");
            comboBox1.Items.Add("管理员");
            comboBox1.Items.Add("删除");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string workerno = textBox1.Text;
            string pwd1 = textBox2.Text;
            string pwd2 = textBox3.Text;
            if (pwd1 != pwd2)
            {
                MessageBox.Show("两次输入的密码不一致，请重新输入！");
                return;
            }
            if (pwd1.Length < 6)
            {
                MessageBox.Show("密码长度应大于6位");
                return;
            }
            design parent = stageDataTran.parent;
            // 检查该工号是否已注册
            string cmdStr = "SELECT * FROM designer WHERE workerno=\'" + workerno + "\';";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            bool b1 = false;
            bool b2 = false;
            if (reader.Read())
                b1 = true;
            reader.Close();
            cmdStr= "SELECT * FROM tempDesigner WHERE workerno=\'" + workerno + "\';";
            cmd = new SqlCommand(cmdStr, parent.myconn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
                b2 = true;
            reader.Close();
            if (b1 | b2)
            {
                MessageBox.Show("工号已存在，请重新选择工号");
                return;
            }
            string[] temp = new string[5];
            temp[0] = textBox4.Text;
            temp[1] = textBox5.Text;
            temp[2] = textBox6.Text;
            temp[3] = textBox7.Text;
            temp[4] = comboBox1.Text;
            int author = 0;
            if (temp[4] == "管理员")
                author = 1;
            else if (temp[4] == "删除")
                author = -1;
            int lenProd = 1;
            for (int i = 0; i < 5; ++i)
                lenProd *= temp[i].Length;
            if (lenProd == 0)
            {
                MessageBox.Show("属性不可为空!");
                return;
            }
            cmdStr = "INSERT INTO tempDesigner VALUES " +
                                "(\'" + workerno + "\'," +
                                "N\'" + temp[0] + "\'," +
                                "N\'" + temp[1] + "\'," +
                                "N\'" + temp[2] + "\'," +
                                "N\'" + temp[3] + "\'," +
                                author + "," +
                                "N\'" + pwd1 + "\'" +
                                "); ";
            if (checkBox1.Checked)
            {
                cmdStr = "SELECT * FROM designer WHERE company =\'" + temp[3] + "\';";
                cmd = new SqlCommand(cmdStr, parent.myconn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("该公司已注册！");
                    return;
                }
                reader.Close();
                cmdStr= "INSERT INTO designer VALUES " +
                                "(\'" + workerno + "\'," +
                                "N\'" + temp[0] + "\'," +
                                "N\'" + temp[1] + "\'," +
                                "N\'" + temp[2] + "\'," +
                                "N\'" + temp[3] + "\'," +
                                1 + "," +
                                "N\'" + pwd1 + "\'" +
                                "); ";
            }
            cmd = new SqlCommand(cmdStr, parent.myconn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("密码必须有字母组合!");
                return;
            }
            this.Close();
        }
    }
}
