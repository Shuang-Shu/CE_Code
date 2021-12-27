using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseCloud
{
    public partial class design_stage : Form
    {
        public design_stage()
        {
            InitializeComponent();
        }

        private void draw()
        {
            // 计算参数
            int picW = this.pictureBox1.Width;
            int picH = pictureBox1.Height;
            double w = 0;
            double h = 0;
            double d = 0;

            try
            {
                w = double.Parse(textBox1.Text);
                h = double.Parse(textBox2.Text);
                d = double.Parse(textBox11.Text);
            }
            catch
            {
                MessageBox.Show("输入正确的长宽与桩径！");
            }

            double kp = (double)picW / (double)picH;
            double k = w / h;
            double rh, rw;
            if ((0.8 / k) * kp > 0.8)
            {
                rh = 0.8;
                rw = 0.8 * k / kp;
            }
            else
            {
                rh = 0.8 / k * kp;
                rw = 0.8;
            }
            // 准备画笔
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(b);

            Pen pen1 = new Pen(Color.Black, 1);
            Pen pen2 = new Pen(Color.Black, 2);
            // 绘制
            //      绘制外边缘
            Rectangle rec = new Rectangle((int)((0.5 - 0.5 * rw) * picW), (int)((0.5 - 0.5 * rh) * picH), (int)(picW*rw), (int)(picH*rh));
            g.DrawRectangle(pen2, rec);
            //      绘制桩
            int m = 0;
            int n = 0;

            double xDist = 0;
            double yDist = 0;
            try
            {
                m = int.Parse(textBox5.Text);
                n = int.Parse(textBox6.Text);
                xDist = double.Parse(textBox7.Text);
                yDist = double.Parse(textBox8.Text);
            }
            catch
            {
                MessageBox.Show("输入正确的间距！");
                return;
            }

            try
            {
                if ((m - 1) * xDist + d > w|| (n - 1) * yDist + d > h)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("桩数/距有误，超出边界");
                return;
            }

            double cx = (w - (m - 1) * xDist)/2.0;
            double cy = (h - (n - 1) * yDist) / 2.0;

            double[,] cornerX = new double[n, m];
            double[,] cornerY = new double[n, m];

            double rZJw = d * rw / w;
            double rZJh = d * rh / h;

            cornerX[0, 0] = 0.5 - 0.5 * rw + rw * (cx / w) - rZJw * 0.5;
            cornerY[0, 0] = 0.5 - 0.5 * rh + rh * (cy / h) - rZJh * 0.5;
            double rdw = xDist / w;
            double rdh = yDist / h;

            for(int i = 1; i < n; ++i)
            {
                cornerX[i, 0] = cornerX[i - 1, 0];
                cornerY[i, 0] = cornerY[i - 1, 0] + rdh * rh;
            }
            for(int j = 1; j < m; ++j)
            {
                for(int i = 0; i < n; ++i)
                {
                    cornerX[i, j] = cornerX[i, j - 1] + rdw * rw;
                    cornerY[i, j] = cornerY[i, j - 1];
                }
            }
            for(int i = 0; i < n; ++i)
            {
                for(int j = 0; j < m; ++j)
                {
                    Rectangle temp = new Rectangle((int)(cornerX[i, j] * picW), (int)(cornerY[i, j] * picH), (int)(picW * rZJw), (int)(picH * rZJh));
                    g.DrawRectangle(pen1, temp);
                }
            }
            // 图形显示
            pictureBox1.Image = b;
            // 释放资源
            pen1.Dispose();
            pen2.Dispose();
            g.Dispose();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            draw();
        }

        public void button2_Click(object sender, EventArgs e)
        {
            design parent = stageDataTran.parent;
            double w = 0;
            double wl = 0;
            double d = 0;
            double h = 0;
            int m = 0;
            int n = 0;
            double dw = 0;
            double dwl = 0;
            double ZJ = 0;

            try
            {
                w = double.Parse(textBox1.Text);
                wl = double.Parse(textBox2.Text);
                d = double.Parse(textBox3.Text);
                h = double.Parse(textBox4.Text);
                m = int.Parse(textBox5.Text);
                n = int.Parse(textBox6.Text);
                dw= double.Parse(textBox7.Text);
                dwl= double.Parse(textBox8.Text);
                ZJ= double.Parse(textBox11.Text);
            }
            catch
            {
                MessageBox.Show("输入正确的参数！");
                return;
            }
            parent.d_d.textBox1.Text = w.ToString();
            parent.d_d.textBox9.Text = wl.ToString();
            parent.d_d.textBox2.Text = (m * n).ToString();
            parent.d_d.textBox3.Text = h.ToString();
            parent.d_d.textBox4.Text = d.ToString();
            parent.d_d.textBox6.Text = ZJ.ToString();
            stageDataTran.w = w;
            stageDataTran.wl = wl;
            stageDataTran.d = d;
            stageDataTran.h = h;
            stageDataTran.m = m;
            stageDataTran.n = n;
            stageDataTran.dw = dw;
            stageDataTran.dwl = dwl;
            stageDataTran.ZJ = ZJ;
            // stageDataTran为用于在不同窗口间传递数据的静态类
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
