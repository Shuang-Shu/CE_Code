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
    public partial class loadDesign : Form
    {
        public loadDesign(design_design pre0)
        {
            InitializeComponent();
            pre = pre0;
            cf = new confirm(this);
        }

        private void getChoices(int c)
        {
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            string temp;
            if (c == 0)
                temp = "area";
            else
                temp = "designer.workerno";
            design parent = stageDataTran.parent;
            string cmdStr = "SELECT DISTINCT "+temp+" FROM design JOIN designer ON design.workerno=designer.workerno WHERE designer.company=N\'"+stageDataTran.company+"\';";

            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader[0]);
            reader.Close();
        }

        private void getDesigns(int c)
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            string choice = comboBox1.Text;
            string temp;
            if (c == 0)
                temp = "area";
            else
                temp = "designer.workerno";
            design parent = stageDataTran.parent;

            string cmdStr = "SELECT dnum FROM design JOIN designer ON design.workerno=designer.workerno WHERE "+temp+"=N\'"+choice+"\' AND"+ " designer.company = N\'"+stageDataTran.company+"\';";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                comboBox2.Items.Add(reader[0]);
            reader.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                MessageBox.Show("请选择设计！");
                return;
            }
            design parent = stageDataTran.parent;
            string dnum = comboBox2.Text;
            string cmdStr = "SELECT describe FROM design WHERE dnum=\'" + dnum + "\';";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            MessageBox.Show((string)reader[0]);
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                MessageBox.Show("请先选择设计");
                return;
            }
            cf.Show();
        }

        private void loadDesigns(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                getDesigns(0);
            else
                getDesigns(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                getChoices(0);
            else if (radioButton2.Checked)
                getChoices(1);
            else
            {
                MessageBox.Show("请选择选项！");
                return;
            }
        }

        private void loadDesign_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
