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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
            dsgn = new design(this);
            mng = new manager(this);
            textBox3.PasswordChar = '*';
            dbAddr = "8.136.81.68";
            dbId = "sa";
            dbPwd = "1234Abcd";
            connect2DB();
        }

        public void connect2DB()
        {
            try
            {
                dsgn.connectDB(dbAddr, dbId, dbPwd);
            }
            catch
            {
                MessageBox.Show("连接失败!");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userid = textBox2.Text;
            string pwd = textBox3.Text;
            design parent = stageDataTran.parent;
            try
            {
                dsgn.connectDB(dbAddr, dbId, dbPwd);
            }
            catch
            {
                MessageBox.Show("连接失败!");
                return;
            }
            string cmdStr = "SELECT isManager, pwd, company FROM designer WHERE workerno=\'" + userid+"\';";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("无管理权限，请先注册！");
                return;
            }
            else
            {
                if (((int)reader[0]) != 1)
                {
                    MessageBox.Show("无管理权限，请先注册！");
                    return;
                }
                else if ((string)reader[1] != pwd)
                {
                    MessageBox.Show("密码错误，请重试");
                    return;
                }
                stageDataTran.workerno = userid;
                stageDataTran.company = (string)reader[2];
                reader.Close();
                parent.d_p.readDB();
                this.Hide();
                mng.Show();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string userid = textBox2.Text;
            string pwd = textBox3.Text;
            design parent = stageDataTran.parent;
            try
            {
                dsgn.connectDB(dbAddr, dbId, dbPwd);
            }
            catch
            {
                MessageBox.Show("连接失败!");
                return;
            }
            string cmdStr = "SELECT isManager, pwd, company FROM designer WHERE workerno=\'" + userid + "\';";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("无设计权限，请先注册！");
                return;
            }
            else if ((string)reader[1] != pwd)
            {
                MessageBox.Show("密码错误，请重试");
                return;
            }
            stageDataTran.workerno = userid;
            stageDataTran.company = (string)reader[2];
            reader.Close();
            parent.d_p.readDB();
            this.Hide();
            dsgn.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SignIn temp = new SignIn();
            temp.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeDB temp = new ChangeDB(this);
            temp.Show();
        }
    }
}
