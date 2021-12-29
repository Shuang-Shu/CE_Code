using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Collections;

namespace BaseCloud
{
    public partial class Manager_sttc : Form
    {
        public Manager_sttc()
        {
            InitializeComponent();
            string[] temp1 = { "地区", "设计者专业", "设计者职称" };
            string[] temp2 = { "饼图", "条形统计图" };
            for (int i = 0; i < temp1.Length; ++i)
                comboBox1.Items.Add(temp1[i]);
            for (int i = 0; i < temp2.Length; ++i)
                comboBox2.Items.Add(temp2[i]);
        }

        public void fullYears()
        {
            design parent = stageDataTran.parent;
            string company = stageDataTran.company;
            string cmdStr = "SELECT DISTINCT YEAR(date0)"+
            " FROM"+
            " design JOIN designer ON design.workerno = designer.workerno"+
            " WHERE company = N\'"+company+"\';";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            comboBox3.Items.Clear();
            comboBox3.Items.Add("全部年份");
            while (reader.Read())
                comboBox3.Items.Add(((int)reader[0]).ToString());
            reader.Close();
        }

        public void drawchart(Chart chart, string[] name, int[] num, string title, int chartForm)
        {
            int sum = 0;
            for (int i = 0; i < num.Length; i++)
                sum = sum + num[i];
            chart.Titles.Clear();
            chart.Series.Clear();
            chart.Titles.Add(title);
            chart.Titles[0].Font = new Font("宋体", 15f);
            chart.Titles.Add("合计：" + Convert.ToString(sum) + "");
            chart.Titles[0].Font = new Font("宋体", 15f);
            Series series1 = new Series();
            Legend legend1 = new Legend();
            legend1.Title = "图例";
            legend1.TitleFont = new Font("宋体", 15f);
            legend1.Font = new Font("宋体", 12f);
            series1.Label = "#VALX：#VAL";
            switch (chartForm) {
                case 1:
                    series1.ChartType = SeriesChartType.Pie;
                    break;
                case 2:
                    series1.ChartType = SeriesChartType.Bar;
                    break;
             }
            series1.Points.DataBindXY(name, num);
            chart.Series.Add(series1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            design parent = stageDataTran.parent;
            string temp = comboBox1.Text;
            string cmdStr;
            string isYear = " YEAR(date0)=\'" + comboBox3.Text + "\' AND";
            if (comboBox3.Text == "全部年份")
                isYear = "";
            if (temp == "地区")
            {
                cmdStr = "SELECT area, COUNT(area)" +
                " FROM design JOIN designer ON design.workerno = designer.workerno WHERE" +
                isYear+
                " designer.company=N\'"+stageDataTran.company+"\'"+
                " GROUP BY area; ";
            }
            else if (temp == "设计者专业")
            {
                cmdStr = "SELECT designer.major, COUNT(designer.major)" +
                " FROM design JOIN designer ON design.workerno = designer.workerno WHERE" +
                isYear+
                " designer.company=N\'" + stageDataTran.company + "\'" +" GROUP BY designer.major; ";
            }
            else if (temp == "设计者职称")
            {
                cmdStr = "SELECT designer.title, COUNT(designer.title)" +
                " FROM design JOIN designer ON design.workerno = designer.workerno WHERE" +
                isYear+
                " designer.company=N\'" + stageDataTran.company + "\'" +" GROUP BY designer.title; ";
            }
            else
            {
                MessageBox.Show("请选择统计类别！");
                return;
            }
            
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            ArrayList cityList = new ArrayList();
            ArrayList numList = new ArrayList();
            while (reader.Read())
            {
                cityList.Add((string)reader[0]);
                numList.Add((int)reader[1]);
            }
            reader.Close();
            string[] names = new string[numList.Count];
            int[] nums = new int[numList.Count];
            for (int i = 0; i < numList.Count; ++i)
            {
                nums[i] = (int)numList[i];
                names[i] = (string)cityList[i];
            }
            if (comboBox2.Text == "条形统计图")
                drawchart(chart1, names, nums, comboBox1.Text + "在所有设计中占比", 0);
            else
                drawchart(chart1, names, nums, comboBox1.Text + "在所有设计中占比", 1);
        }

        private void areaSearch(string[] names, int[] nums)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fullYears();
            button1_Click(null, null);
        }
    }
}
