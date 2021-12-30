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
    public partial class Manager_author : Form
    {
        public Manager_author(manager pre0)
        {
            InitializeComponent();
            pre = pre0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i<listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
            }
        }

        // 更新listBox
        public void refreshList()
        {
            listBox1.Items.Clear();
            string workerno = stageDataTran.workerno;
            string cmdStr = "SELECT * FROM tempDesigner WHERE company =N\'"+stageDataTran.company+"\';";
            SqlCommand cmd = new SqlCommand(cmdStr, stageDataTran.parent.myconn);
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Errors[0].Message);
                return;
            }
            while (reader.Read())
            {
                string applyAuthor = "设计者";
                if ((int)(reader[5]) == 1)
                    applyAuthor = "管理员";
                else if((int)(reader[5]) == -1)
                    applyAuthor = "删除"; 
                listBox1.Items.Add("工号:" + reader[0] + ",申请权限:" + applyAuthor);
            }
            reader.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            refreshList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++){
                listBox1.SetSelected(i, false);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            changeAuthor("同意申请");
        }


        private void button7_Click(object sender, EventArgs e)
        {
            changeAuthor("拒绝申请");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string cmdStr;
            SqlCommand cmd;
            SqlDataReader reader;
            design parent = stageDataTran.parent;
            for (int i = 0; i < listBox1.Items.Count; ++i)
            {
                try
                {
                    string temp = listBox1.Items[i].ToString();
                    string workerno = temp.Substring(3, 5);
                    if (temp.Contains("拒绝"))
                    {
                        cmdStr = "DELETE FROM tempDesigner WHERE workerno=\'" + workerno + "\'";
                        cmd = new SqlCommand(cmdStr, parent.myconn);
                        cmd.ExecuteNonQuery();
                    }
                    else if (temp.Contains("同意"))
                    {
                        if (temp.Contains("删除"))
                        {
                            cmdStr = "SELECT * FROM designer WHERE workerno=\'" + workerno + "\';";
                            cmd = new SqlCommand(cmdStr, parent.myconn);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                reader.Close();
                            }
                            else
                            {
                                reader.Close();
                                cmdStr = "DELETE FROM designer WHERE workerno=\'" + workerno + "\'";
                                cmd = new SqlCommand(cmdStr, parent.myconn);
                                cmd.ExecuteNonQuery();
                            }
                            continue;
                        }
                        cmdStr = "SELECT * FROM tempDesigner WHERE workerno=\'" + workerno + "\';";
                        cmd = new SqlCommand(cmdStr, parent.myconn);
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        string[] paras = new string[7];
                        for (int k = 0; k < 7; ++k)
                            if (k != 5)
                                paras[k] = (string)(reader[k]);
                        int para5 = (int)(reader[5]);
                        reader.Close();
                        // 检查原表里是否已有该designer
                        cmdStr = "SELECT * FROM designer WHERE workerno=\'" + workerno + "\';";
                        cmd = new SqlCommand(cmdStr, parent.myconn);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            reader.Close();
                            cmdStr = "UPDATE designer SET isManager=" + para5.ToString() + "WHERE  workerno=\'"+workerno+"\';";
                            cmd = new SqlCommand(cmdStr, parent.myconn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            reader.Close();
                            cmdStr = "INSERT INTO designer VALUES " +
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
                        cmdStr = "DELETE FROM tempDesigner WHERE workerno=\'" + workerno + "\'";
                        cmd = new SqlCommand(cmdStr, parent.myconn);
                        cmd.ExecuteNonQuery();
                    }
                }catch
                {
                    MessageBox.Show("出现错误！");
                }
            }
            refreshList();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // 移除之前的选择
            int i = 0;
            for (int j = 0; j < listBox1.SelectedIndices.Count; ++i)
            {
                i = listBox1.SelectedIndices[j];
                string temp = listBox1.Items[i].ToString();
                if (temp.Length > 17)
                {
                    temp = temp.Substring(0, 17);
                }
                listBox1.Items.RemoveAt(i);
                listBox1.Items.Insert(i, temp);
            }
        }

        private void changeAuthor(string act)
        {
            int i = 0;
            for (int j = 0; j < listBox1.SelectedIndices.Count; ++i)
            {
                i = listBox1.SelectedIndices[j];
                string temp = listBox1.Items[i].ToString();
                if (temp.Length > 17)
                {
                    temp = temp.Substring(0, 17);
                }
                temp += ("   "+act);
                listBox1.Items.RemoveAt(i);
                listBox1.Items.Insert(i, temp);
            }
        }
    }
}
