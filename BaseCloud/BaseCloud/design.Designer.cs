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
    partial class design
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
            this.设计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.土层参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.承台设计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返回登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.申请权限ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设计ToolStripMenuItem,
            this.土层参数ToolStripMenuItem,
            this.承台设计ToolStripMenuItem,
            this.申请权限ToolStripMenuItem,
            this.返回登录ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(787, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设计ToolStripMenuItem
            // 
            this.设计ToolStripMenuItem.Name = "设计ToolStripMenuItem";
            this.设计ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.设计ToolStripMenuItem.Text = "基桩设计";
            this.设计ToolStripMenuItem.Click += new System.EventHandler(this.设计ToolStripMenuItem_Click);
            // 
            // 土层参数ToolStripMenuItem
            // 
            this.土层参数ToolStripMenuItem.Name = "土层参数ToolStripMenuItem";
            this.土层参数ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.土层参数ToolStripMenuItem.Text = "土层参数";
            this.土层参数ToolStripMenuItem.Click += new System.EventHandler(this.土层参数ToolStripMenuItem_Click);
            // 
            // 承台设计ToolStripMenuItem
            // 
            this.承台设计ToolStripMenuItem.Name = "承台设计ToolStripMenuItem";
            this.承台设计ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.承台设计ToolStripMenuItem.Text = "承台设计";
            this.承台设计ToolStripMenuItem.Click += new System.EventHandler(this.承台设计ToolStripMenuItem_Click);
            // 
            // 返回登录ToolStripMenuItem
            // 
            this.返回登录ToolStripMenuItem.Name = "返回登录ToolStripMenuItem";
            this.返回登录ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.返回登录ToolStripMenuItem.Text = "返回登录";
            this.返回登录ToolStripMenuItem.Click += new System.EventHandler(this.返回登录ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 31);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 586);
            this.panel1.TabIndex = 3;
            // 
            // 申请权限ToolStripMenuItem
            // 
            this.申请权限ToolStripMenuItem.Name = "申请权限ToolStripMenuItem";
            this.申请权限ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.申请权限ToolStripMenuItem.Text = "申请权限";
            this.申请权限ToolStripMenuItem.Click += new System.EventHandler(this.申请权限ToolStripMenuItem_Click);
            // 
            // design
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 628);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "design";
            this.Text = "设计";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 土层参数ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 承台设计ToolStripMenuItem;
        public design_design d_d;
        public design_soil d_p;
        public design_stage d_s;
        public DataGridView soilData;
        public SqlConnection myconn = null;
        public register rgst;
        private ToolStripMenuItem 返回登录ToolStripMenuItem;
        private ToolStripMenuItem 申请权限ToolStripMenuItem;
    }
}