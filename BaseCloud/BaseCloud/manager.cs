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
    public partial class manager : Form
    {
        public manager(register rgst0)
        {
            InitializeComponent();
            mstc = new Manager_sttc();
            mng = new Manger_mng();
            mauth = new Manager_author(this);
            mstc.TopLevel = false;
            mng.TopLevel = false;
            mauth.TopLevel = false;
            mstc.Parent = panel1;
            mng.Parent = panel1;
            mauth.Parent = panel1;
            rgst = rgst0;
        }

        private void 管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(mng);
            mng.Show();
        }

        private void 统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(mstc);
            mstc.Show();
        }

        private void 返回登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            rgst.Show();
        }

        private void 申请管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(mauth);
            mauth.refreshList();
            mauth.Show();
        }
    }
}
