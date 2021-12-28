namespace BaseCloud
{
    partial class manager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返回登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.申请管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理ToolStripMenuItem,
            this.统计ToolStripMenuItem,
            this.申请管理ToolStripMenuItem,
            this.返回登录ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(779, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 管理ToolStripMenuItem
            // 
            this.管理ToolStripMenuItem.Name = "管理ToolStripMenuItem";
            this.管理ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.管理ToolStripMenuItem.Text = "管理";
            this.管理ToolStripMenuItem.Click += new System.EventHandler(this.管理ToolStripMenuItem_Click);
            // 
            // 统计ToolStripMenuItem
            // 
            this.统计ToolStripMenuItem.Name = "统计ToolStripMenuItem";
            this.统计ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.统计ToolStripMenuItem.Text = "统计";
            this.统计ToolStripMenuItem.Click += new System.EventHandler(this.统计ToolStripMenuItem_Click);
            // 
            // 返回登录ToolStripMenuItem
            // 
            this.返回登录ToolStripMenuItem.Name = "返回登录ToolStripMenuItem";
            this.返回登录ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.返回登录ToolStripMenuItem.Text = "返回登录";
            this.返回登录ToolStripMenuItem.Click += new System.EventHandler(this.返回登录ToolStripMenuItem_Click);
            // 
            // 申请管理ToolStripMenuItem
            // 
            this.申请管理ToolStripMenuItem.Name = "申请管理ToolStripMenuItem";
            this.申请管理ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.申请管理ToolStripMenuItem.Text = "申请管理";
            this.申请管理ToolStripMenuItem.Click += new System.EventHandler(this.申请管理ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 412);
            this.panel1.TabIndex = 1;
            // 
            // manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 451);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "manager";
            this.Text = "数据管理";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 统计ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        public Manager_sttc mstc;
        public Manger_mng mng;
        private System.Windows.Forms.ToolStripMenuItem 返回登录ToolStripMenuItem;
        public register rgst;
        public Manager_author mauth;
        private System.Windows.Forms.ToolStripMenuItem 申请管理ToolStripMenuItem;
    }
}