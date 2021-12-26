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
        public manager()
        {
            InitializeComponent();
            mstc = new Manager_sttc();
            mng = new Manger_mng();
            mstc.TopLevel = false;
            mng.TopLevel = false;
            mstc.Parent = panel1;
            mng.Parent = panel1;
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
    }
}
