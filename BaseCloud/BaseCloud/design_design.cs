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
    public partial class design_design : Form
    {
        public design_design()
        {
            InitializeComponent();
            comboBox1.Items.Add("方桩");
            comboBox1.Items.Add("圆桩");
            comboBox2.Items.Add("灌注桩");
            comboBox2.Items.Add("预制桩");
            dif = new designInfo(this);
            ldsg = new loadDesign(this);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        // 返回一个6元素数组，按 （长边方向：轴力，弯矩，剪力）
        // （短边方向：轴力，弯矩，剪力）的顺序存储荷载值
        /*
        private double[] getLoad()
        {
            double[] loads = new double[6];
            loads[0] = double.Parse(textBox5.Text);
            loads[1] = double.Parse(textBox12.Text);
            loads[2] = double.Parse(textBox13.Text);
            loads[3] = double.Parse(textBox14.Text);
            loads[4] = double.Parse(textBox15.Text);
            loads[5] = double.Parse(textBox16.Text);
            return loads;
        }*/

        private double[] getSoilData(int idx)
        {
            design parent = stageDataTran.parent;
            DataGridView soil = parent.soilData;
            int validRows = soil.Rows.Count - 1;
            double[] result = new double[validRows];
            for(int i = 0; i < validRows; ++i)
                result[i] = double.Parse((string)soil.Rows[i].Cells[idx].Value);
            return result;
        }

        private void showResult(string name, double result, string unit)
        {
            string temp = name + ": " + result.ToString("0.00") + unit+ "\r\n";
            textBox10.Text += temp;
        }

        private double singlePile()
        {
            // 获取土层数据
            double result = 0;
            double[] d = getSoilData(3);
            d[d.Length - 1] = 64710000;
            double[] pc = getSoilData(4);
            double[] pe = getSoilData(5);
            double ZJ = double.Parse(textBox6.Text);
            double pileArea = 0;
            double pilePermi = 0;
            if (comboBox1.Text == "方桩")
            {
                pileArea = ZJ * ZJ;
                pilePermi = 4.0 * ZJ;
            }
            else
            {
                pileArea = 3.14159 * ZJ * ZJ / 4.0;
                pilePermi = 3.14159 * ZJ;
            }

            for(int i = 1; i < d.Length; ++i)
                d[i] = d[i - 1] + d[i];
            double pileTop = double.Parse(textBox4.Text);
            double pileButtom = double.Parse(textBox8.Text) + pileTop;
            double layerTop = pileTop;

            double capEnd = 0;
            double capSide = 0;

            for(int i = 0; i < d.Length; ++i)
            {
                if (layerTop <= d[i])
                {
                    double buttom = Math.Min(pileButtom, d[i]);
                    capSide += pc[i] * pilePermi * (buttom-layerTop);
                    layerTop = buttom;
                }
            }
            for(int i = 0; i < d.Length; ++i)
            {
                if (pileButtom < d[i])
                {
                    capEnd = pe[i] * pileArea;
                    break;
                } 
                else if (pileButtom == d[i])
                {
                    capEnd = pe[i + 1] * pileArea;
                    break;
                }
            }
            result = capEnd + capSide;
            return result;
        }

        private double[] pileTopVerify()
        {
            // 返回最大/最小单桩应力
            double[] result = new double[2];
            design parent = stageDataTran.parent;
            int m = stageDataTran.m;
            int n = stageDataTran.n;
            double dw = stageDataTran.dw;
            double dwl = stageDataTran.dwl;
            double w = stageDataTran.w;
            double wl = stageDataTran.wl;
            double d = stageDataTran.d;
            double ZJ = stageDataTran.ZJ;
            double pileArea = 0;
            if (comboBox1.Text == "方桩")
                pileArea = ZJ * ZJ;
            else
                pileArea = 3.14159 * ZJ * ZJ / 4.0;
            try
            {
                double N = double.Parse(textBox5.Text);
                double G = w * wl * d * 20;

                double avgN = (N + G) / ((double)(m * n));

                // 长边方向
                double Mm = double.Parse(textBox12.Text);
                double Vm = double.Parse(textBox13.Text);
                Mm += Vm * d;

                double yMax = (m - 1) * dw / 2.0;
                double yJSqrt = 0;
                for(int i = 0; i < m; ++i)
                    yJSqrt += (i * dw - yMax) * (i * dw - yMax);
                yJSqrt = n * yJSqrt;
                double MmN = Mm * yMax / yJSqrt;
                // 短边方向
                double Mn = double.Parse(textBox15.Text);
                double Vn = double.Parse(textBox16.Text);
                Mn += Vn * d;

                double xMax = (n - 1) * dwl / 2.0;
                double xJSqrt = 0;
                for (int i = 0; i < n; ++i)
                    xJSqrt += (i * dwl - xMax) * (i * dwl - xMax);
                xJSqrt = m * xJSqrt;
                double MnN = Mn * xMax / xJSqrt;

                result[0] = avgN + Math.Abs(MmN) + Math.Abs(MnN);
                result[1] = avgN - Math.Abs(MmN) - Math.Abs(MnN);
                result[0] /= pileArea;
                result[1] /= pileArea;
            }
            catch
            {
                MessageBox.Show("输入正确的荷载！");
                return result;
            }
            return result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox10.Text += ">>>\r\n";
            double sp = singlePile();
            showResult("单桩承载力:", sp, "kN");
            double[] topVerify = pileTopVerify();
            showResult("最大单桩压应力:", topVerify[0], "kPa");
            showResult("最小单桩压应力:", topVerify[1], "kPa");
            double settlement = getSettlement();
            showResult("整体沉降为:", settlement, "mm");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void design_design_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dif.Show();
            dif.refreshDesigner();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ldsg != null)
                ldsg.Show();
            else
                ldsg = new loadDesign(this);
            // loadDesig为导入设计用的窗口
        }

        private void deleteDnum(string dnum)
        {
            design parent = stageDataTran.parent;
            // 删除土层信息
            string cmdStr = "DELETE FROM soilInfo WHERE dnum="+"\'"+dnum+"\'";
            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            cmd.ExecuteNonQuery();
            // 删除基桩信息
            cmdStr= "DELETE FROM pileInfo WHERE dnum=" + "\'" + dnum + "\'";
            cmd = new SqlCommand(cmdStr, parent.myconn);
            cmd.ExecuteNonQuery();
            // 删除荷载信息
            cmdStr = "DELETE FROM loads WHERE dnum=" + "\'" + dnum + "\'";
            cmd = new SqlCommand(cmdStr, parent.myconn);
            cmd.ExecuteNonQuery();
            // 删除设计记录
            cmdStr = "DELETE FROM design WHERE dnum=" + "\'" + dnum + "\'";
            cmd = new SqlCommand(cmdStr, parent.myconn);
            cmd.ExecuteNonQuery();
        }

        private void saveAll()
        {
            // 承台部分
            design parent = stageDataTran.parent;
            design_stage d_s = parent.d_s;
            design_design d_d = parent.d_d;
            double[] stgParas = new double[9];
            // 长 宽 埋深 高度 长度方向桩数(4) 宽度方向桩数(5) 长度方向间距 宽度方向间距 桩径
            try
            {
                stgParas[0] = double.Parse(d_s.textBox1.Text);
                stgParas[1] = double.Parse(d_s.textBox2.Text);
                stgParas[2] = double.Parse(d_s.textBox3.Text);
                stgParas[3] = double.Parse(d_s.textBox4.Text);
                stgParas[4] = double.Parse(d_s.textBox5.Text);
                stgParas[5] = double.Parse(d_s.textBox6.Text);
                stgParas[6] = double.Parse(d_s.textBox7.Text);
                stgParas[7] = double.Parse(d_s.textBox8.Text);
                stgParas[8] = double.Parse(d_s.textBox11.Text);
            }
            catch
            {
                MessageBox.Show("检查承台参数的正确性");
                return;
            }
            // 保存本视图中的内容
            double len = 0;
            string[] info = new string[2];
            try
            {
                len = double.Parse(textBox8.Text);
                info[0] = comboBox1.Text;
                info[1] = comboBox2.Text;
            }
            catch
            {
                MessageBox.Show("检查基桩参数是否正确");
                return;
            }
            // 保存荷载信息
            // 轴力 长边方向弯矩 长边方向剪力 短边方向弯矩 短边方向剪力
            double[] loads = new double[5];
            try
            {
                loads[0] = double.Parse(textBox5.Text);
                loads[1] = double.Parse(textBox12.Text);
                loads[2] = double.Parse(textBox13.Text);
                loads[3] = double.Parse(textBox15.Text);
                loads[4] = double.Parse(textBox16.Text);
            }
            catch
            {
                MessageBox.Show("输入有效的荷载值!");
                return;
            }
            // 保存土层信息
            design_soil d_p = parent.d_p;
            DataGridView soilData = d_p.dataGridView1;
            int rowsCount = soilData.Rows.Count - 1;
            double[,] soilPara = new double[rowsCount, 6];
            for(int i = 0; i < rowsCount; ++i)
                for(int j = 0; j < 6; ++j)
                    soilPara[i, j] = double.Parse((string)(soilData.Rows[i].Cells[j + 1].Value));
            // 上传信息
            string cmdStr;
            SqlCommand cmd;
            // 上传荷载信息
            cmdStr = "INSERT INTO loads VALUES (\'" + textBox7.Text+"\',"
                +loads[0].ToString()+","+ loads[1].ToString() 
                + ","+ loads[2].ToString() + ","+ loads[3].ToString() 
                + ","+ loads[4].ToString() + ");";
            cmd = new SqlCommand(cmdStr, parent.myconn);
            cmd.ExecuteNonQuery();
            // 上传承台信息
            cmdStr= "INSERT INTO pileInfo"+
                    " VALUES("+
                " \'"+textBox7.Text+"\',"+
                stgParas[0].ToString()+","+
                stgParas[1].ToString() + "," +
                ((int)stgParas[2]).ToString() + "," +
                ((int)stgParas[3]).ToString() + "," +
                stgParas[4].ToString() + "," +
                stgParas[5].ToString() + "," +
                stgParas[6].ToString() + "," +
                stgParas[7].ToString() + "," +
                stgParas[8].ToString() + "," +
                len.ToString() + "," +
                "N\'" + info[1] + "\',"+
                "N\'"+ info[0] + "\'"+
                "); ";
            cmd = new SqlCommand(cmdStr, parent.myconn);
            cmd.ExecuteNonQuery();
            // 上传土层信息
            for(int i = 0; i < rowsCount; ++i)
            {
                cmdStr= "INSERT INTO soilInfo VALUES("+
                    " \'"+textBox7.Text+"\',"+
                    (i+1).ToString() + "," +
                    soilPara[i, 0].ToString() + "," +
                    soilPara[i, 1].ToString() + "," +
                    soilPara[i, 2].ToString() + "," +
                    soilPara[i, 3].ToString() + "," +
                    soilPara[i, 4].ToString() + "," +
                    soilPara[i, 5].ToString() +
                "); ";
                cmd = new SqlCommand(cmdStr, parent.myconn);
                cmd.ExecuteNonQuery();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (saveDesignLog() < 0)
                return;
            saveAll();
        }

        private int saveDesignLog()
        {
            design parent = stageDataTran.parent;
            string temp= "SELECT dnum FROM design WHERE dnum=" + "\'" + textBox7.Text + "\'";
            SqlCommand tempCmd = new SqlCommand(temp, parent.myconn);
            SqlDataReader reader = tempCmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                deleteDnum(textBox7.Text);
            }
            reader.Close();

            if (textBox7.Text == "")
            {
                MessageBox.Show("请先确认设计信息");
                return -1;
            }
            
            string dnum = designData.dnum;
            string wnum = designData.workno;
            string area = designData.area;
            string des = designData.describe;

            string cmdStr = "INSERT INTO design"+
            " VALUES"+
            " ("+
            " \'"+dnum+"\',"+
            " \'"+wnum.Substring(0, 5)+"\',"+
            " N\'"+area+"\',"+
            " N\'"+des+"\'"+
            " ); ";

            SqlCommand cmd = new SqlCommand(cmdStr, parent.myconn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("执行成功");
            }
            catch
            {
                MessageBox.Show("出现错误");
                return -1;
            }
            return 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 获取数据
            double plen = 0;
            double w = 0;
            double dw = 0;
            double ZJ = 0;
            double d = 0;
            double h = 0;
            int m = 0;
            try
            {
                plen = double.Parse(textBox8.Text);
                w = double.Parse(textBox1.Text);
                dw = double.Parse(stageDataTran.parent.d_s.textBox7.Text);
                ZJ = double.Parse(textBox6.Text);
                d = double.Parse(textBox4.Text);
                h = double.Parse(textBox3.Text);
                m = int.Parse(stageDataTran.parent.d_s.textBox5.Text);
            }
            catch
            {
                MessageBox.Show("检查参数的是否正确输入");
                return;
            }
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(b);

            Pen pen1 = new Pen(Color.Black, 1);
            Pen pen2 = new Pen(Color.Black, 2);
            Pen pen3 = new Pen(Color.Brown, 1);

            int pw = pictureBox1.Width;
            int ph = pictureBox1.Height;

            double rh = ((3.0 / 4.0) * (double)ph) / (plen + d);
            double rw = (1.0 / 2.0) * (double)pw / w;
            // 绘制地表
            Point p1 = new Point(0, (int)(ph / 8.0));
            Point p2 = new Point((int)(pw * 3.0 / 7.0), (int)(ph / 8.0));
            Point p3 = new Point((int)(pw * 4.0 / 7.0), (int)(ph / 8.0));
            Point p4 = new Point(pw, (int)(ph / 8.0));
            g.DrawLine(pen2, p1, p2);
            g.DrawLine(pen2, p3, p4);
            // 绘制柱
            Rectangle rec1 = new Rectangle((int)(pw * 3.0 / 7.0), -1, (int)(1.0 / 7.0 * pw), (int)(ph / 8.0 + (d - h) * rh));
            g.DrawRectangle(pen1, rec1);
            // 绘制桩基
            Rectangle rec2 = new Rectangle((int)(1.0 / 4.0 * pw), (int)(ph / 8.0 + (d - h) * rh), (int)(pw / 2.0), (int)(h * rh));
            g.DrawRectangle(pen1, rec2);
            // 绘制桩
            Rectangle rec3;
            int pileW = (int)(rw * ZJ);
            int pileLen = (int)(rh * plen);
            int temp1 = (int)(1.0 / 4.0 * pw);
            int temp2 = (int)(((w - (m - 1) * dw) / 2.0 - ZJ / 2.0) * rw);
            int leftBound = temp1 + temp2;
            int offset = (int)(dw * rw);
            for(int i = 0; i < m; ++i)
            {
                rec3 = new Rectangle(leftBound, (int)(ph / 8.0 + (d - h) * rh) + (int)(h * rh), pileW, pileLen);
                g.DrawRectangle(pen1, rec3);
                leftBound += offset;
            }
            // 绘制土层
            DataGridView dgv = stageDataTran.parent.d_p.dataGridView1;
            int rowsCount = dgv.Rows.Count - 1;
            double[] ts = new double[rowsCount-1];
            for(int i = 0; i < rowsCount - 1; ++i)
                ts[i] = double.Parse((string)dgv.Rows[i].Cells[3].Value);
            for (int i = 1; i < rowsCount - 1; ++i)
                ts[i] = ts[i - 1] + ts[i];
            for(int i = 0; i < rowsCount-1; ++i)
            {
                int y = (int)(ph / 8 + (int)(ts[i] * rh));
                p1 = new Point(0, y);
                p2 = new Point((int)(9.0/40.0*pw), y);
                p3 = new Point((int)(31.0/40.0*pw), y);
                p4 = new Point(pw+1, y);
                g.DrawLine(pen3, p1, p2);
                g.DrawLine(pen3, p3, p4);
            }
            // 释放资源
            pictureBox1.Image = b;
            pen1.Dispose();
            pen2.Dispose();
            g.Dispose();
        }

        public double getSettlement()
        {
            int a = stageDataTran.parent.d_p.dataGridView1.Rows.Count - 1;
            double l = 0;
            double b = 0;
            double d = 0;
            double N = 0;
            double[] sd = new double[a];
            double[,] cs = new double[a, 2];

            try
            {
                l = double.Parse(textBox1.Text);
                b = double.Parse(textBox9.Text);
                d = double.Parse(textBox4.Text);
                N = double.Parse(textBox5.Text);
            }catch
            {
                MessageBox.Show("检查长款是否输入");
                return 0;
            }
            double fyc = 0;
            double zyc = 0;

            try
            {
                for (int i = 0; i < a; ++i)
                {
                    sd[i] = double.Parse((string)(stageDataTran.parent.d_p.dataGridView1.Rows[i].Cells[3].Value));
                    cs[i, 0]= double.Parse((string)(stageDataTran.parent.d_p.dataGridView1.Rows[i].Cells[2].Value));
                    cs[i, 1]= double.Parse((string)(stageDataTran.parent.d_p.dataGridView1.Rows[i].Cells[6].Value));
                }
            }
            catch
            {
                MessageBox.Show("检查厚度数据是否合法");
                return 0;
            }
            double gamma0 = cs[0, 0];
            fyc = (N + (d * l * b) * 20 - gamma0 * l * b) / (l * b);
            zyc = gamma0 * d;
            return caculateSettlement(a, l, b, fyc, zyc, sd, cs);
        }

        public double caculateSettlement(int a, double l, double b, double fyc, double zyc, double[] sd, double[,] cs)
        //a为土层个数；l为基础底面长边；b为基础底面短边；fyc表示基础底面附加应力；zyc表示基础底面自重应力；sd{i}表示分层土层深度(i表示土层编号）；cs[i,j]表示第i号土层的第j个参数（参数0表示重度，参数1表示压缩模量）
        {
            double z = 0;
            double ys = 0;
            for (int i = 0; i < a; i++)
            {
                z = z + sd[i];
                double m = z / b;
                double n = l / b;
                double c = 1 / (2 * Math.PI) * (m * n * (1 + Math.Pow(n, 2) + 2 * Math.Pow(m, 2)) / (Math.Sqrt(1 + Math.Pow(m, 2) + Math.Pow(n, 2)) * (Math.Pow(m, 2) + Math.Pow(n, 2)) * (1 + Math.Pow(m, 2)))) + Math.Atan(n / (m * Math.Sqrt(1 + Math.Pow(m, 2) + Math.Pow(n, 2))));
                double[] zy = new double[a];
                double[] fy = new double[a];
                double[] zyavg = new double[a];
                double[] fyavg = new double[a];
                if (i == 0)
                {
                    zy[i] = zyc + cs[i, 0] * sd[i];
                    fy[i] = 4 * c * fyc;
                    zyavg[i] = (zy[i] + zyc) / 2;
                    fyavg[i] = (fy[i] + fyc) / 2;
                    ys = ys + ((fyavg[i] + zyavg[i]) / 2 - zyavg[i]) / cs[i, 1] * sd[i];
                }
                if (i > 0)
                {
                    zy[i] = zy[i - 1] + cs[i, 0] * sd[i];
                    fy[i] = 4 * c * fyc;
                    if (fy[i] < zy[i])
                        break;
                    zyavg[i] = (zy[i] + zy[i - 1]) / 2;
                    fyavg[i] = (fy[i] + fy[i - 1]) / 2;
                    ys = ys + ((fyavg[i] + zyavg[i]) / 2 - zyavg[i]) / cs[i, 1] * sd[i];
                }
                if (fy[i] < zy[i])
                    break;
            }
            return ys;
        }
    }
}
