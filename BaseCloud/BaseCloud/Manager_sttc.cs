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
            string[] temp2 = { "饼图", "条状统计图" };
            for (int i = 0; i < temp1.Length; ++i)
                comboBox1.Items.Add(temp1[i]);
            for (int i = 0; i < temp2.Length; ++i)
                comboBox2.Items.Add(temp2[i]);
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
            string cmdStr = "SELECT area, COUNT(area)"+
            " FROM design"+
            " GROUP BY area; ";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            SqlDataReader reader = cmd.ExecuteReader();
            ArrayList cityList = new ArrayList();
            ArrayList numList = new ArrayList();
            while (reader.Read())
            {
                cityList.Add((string)reader[0]);
                numList.Add((int)reader[1]);
            }

            string[] names = new string[numList.Count];
            int[] nums = new int[numList.Count];
            for (int i = 0; i < numList.Count; ++i)
            {
                nums[i] = (int)numList[i];
                names[i] = (string)cityList[i];
            }
            drawchart(chart1, names, nums, "hello", 1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
