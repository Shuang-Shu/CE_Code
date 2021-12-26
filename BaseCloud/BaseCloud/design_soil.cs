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
    public partial class design_soil : Form
    {
        public design_soil()
        {
            InitializeComponent();
        }

        public void readDB()
        {
            design parent = stageDataTran.parent;
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT city FROM soil;", parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader[0]);
            reader.Close();
        }

        public void refreshSoilType(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            string city = comboBox1.Text;
            design parent = stageDataTran.parent;
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT soilType FROM soil WHERE city=N\'" 
                + city + "\';", parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                comboBox2.Items.Add(reader[0]);
            reader.Close();
        }

        public void refreshSoil(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            string city = comboBox1.Text;
            string soilType = comboBox2.Text;
            design parent = stageDataTran.parent;
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT soil FROM soil WHERE city=N\'" 
                + city + "\' AND soilType=N\'" + soilType + "\';", parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                comboBox3.Items.Add(reader[0]);
            reader.Close();
        }

        private void design_para_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int idx = 0;
            int newRowNums = dataGridView1.Rows.Count-2;
            
            try
            {
                idx = int.Parse(this.textBox7.Text) - 1;
                if (idx > newRowNums)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("输入正确的编号！");
                return;
            }
            dataGridView1.Rows.RemoveAt(idx);
            for (int i = idx; i < newRowNums; ++i)
                dataGridView1.Rows[i].Cells[0].Value = (int.Parse((string)dataGridView1.Rows[i].Cells[0].Value) - 1).ToString();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            int idx = 0;
            try
            {
                idx = int.Parse(this.textBox7.Text)-1;
            }
            catch
            {
                MessageBox.Show("输入正确的编号！");
                return;
            }
            int num = 6;
            double[] datas = new double[num];
            getData(datas);
            for (int i = 0; i < num; ++i)
                this.dataGridView1.Rows[idx].Cells[i + 1].Value = datas[i].ToString();
        }

        int getData(double[] datas)
        {
            try
            {
                datas[0] = double.Parse(textBox1.Text);
                datas[1] = double.Parse(textBox2.Text);
                datas[2] = double.Parse(textBox3.Text);
                datas[3] = double.Parse(textBox4.Text);
                datas[4] = double.Parse(textBox5.Text);
                datas[5] = double.Parse(textBox6.Text);
            }
            catch
            {
                MessageBox.Show("输入合法的参数值");
                return -1;
            }
            return 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num = 6;
            double[] datas = new double[num];
            if (getData(datas) == -1)
                return;
            int idx = dataGridView1.Rows.Add();
            this.dataGridView1.Rows[idx].Cells[0].Value = (idx + 1).ToString();
            for (int i = 0; i < num; ++i)
                this.dataGridView1.Rows[idx].Cells[i + 1].Value = datas[i].ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string[] paras = new string[5];
            paras[0] = textBox1.Text;
            paras[1] = textBox2.Text;
            paras[2] = textBox4.Text;
            paras[3] = textBox5.Text;
            paras[4] = textBox6.Text;
            string area = comboBox1.Text;
            string soilType = comboBox2.Text;
            string soil = comboBox3.Text;

            design parent = stageDataTran.parent;
            string cmdStr = "INSERT INTO soil VALUES("+
                "N\'"+area+"\',"+
                "N\'" + soilType + "\'," +
                "N\'" + soil + "\'," +
                paras[0]+","+
                paras[1] + "," +
                paras[2] + "," +
                paras[3] + "," +
                paras[4]+
                "); ";

            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("检查参数值的正确性，并确保地区-土类-土种不与既有值重复");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
                refreshPara();
        }

        public void refreshPara()
        {
            string city = comboBox1.Text;
            string soilType = comboBox2.Text;
            string soil = comboBox3.Text;

            if (city == "" || soilType == "" || soil == "")
            {
                MessageBox.Show("选取土种");
                return;
            }
            design parent = stageDataTran.parent;
            SqlCommand cmd = new SqlCommand("SELECT erate, gamma, pc, pe, E" +
            " FROM soil" +
            " WHERE city = N\'" + city + "\' AND soilType = N\'" + soilType + "\' AND soil = N\'" + soil + "\'; ", parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            textBox1.Text = reader[0].ToString();
            textBox2.Text = reader[1].ToString();
            textBox4.Text = reader[2].ToString();
            textBox5.Text = reader[3].ToString();
            textBox6.Text = reader[4].ToString();
            reader.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            InsertSoil temp = new InsertSoil(this);
            temp.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string area = comboBox1.Text;
            string soilType = comboBox2.Text;
            string soil = comboBox3.Text;

            double[] para = new double[5];
            try
            {
                para[0] = double.Parse(textBox1.Text);
                para[1] = double.Parse(textBox2.Text);
                para[2] = double.Parse(textBox4.Text);
                para[3] = double.Parse(textBox5.Text);
                para[4] = double.Parse(textBox6.Text);
            }
            catch
            {
                MessageBox.Show("检查土层参数的合法性");
                return;
            }
            design parent = stageDataTran.parent;
            string cmdStr = "UPDATE soil SET " +
                            " eRate =" + para[0] + "," +
                            " gamma =" + para[1] + "," +
                            " pc =" + para[2] + "," +
                            " pe =" + para[3] + "," +
                            " E =" + para[4] +
                            " WHERE" +
                            " city = N\'" + area + "\' AND" +
                            " soilType = N\'" + soilType + "\' AND" +
                            " soil = N\'" + soil + "\'";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("来自数据库的错误:" + ex.Errors[0].Message);
                if (ex.Errors.Count>1)
                    MessageBox.Show("来自数据库的错误:" + ex.Errors[1].Message);
                return;
            }
            refreshPara();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string area = comboBox1.Text;
            string soilType = comboBox2.Text;
            string soil = comboBox3.Text;
            design parent = stageDataTran.parent;
            string cmdStr = "DELETE FROM soil WHERE" +
                " city= N\'" + area + "\' and" +
                " soilType= N\'" + soilType + "\' and" +
                " soil= N\'" + soil + "\';";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("来自数据库的错误:" + ex.Errors[0].Message);
                return;
            }
            catch
            {
                MessageBox.Show("检查地区、土类与土种是否已选择");
                return;
            }
            refreshSoilType(null, null);
        }
    }
}
