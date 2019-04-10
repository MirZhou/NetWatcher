namespace NetWatcher.GetAreaForGuoJiaTongJiJu
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetAreaInfo = new System.Windows.Forms.Button();
            this.txtAreaAddress = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeArea = new System.Windows.Forms.TreeView();
            this.lbMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbMessage);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnGetAreaInfo);
            this.panel1.Controls.Add(this.txtAreaAddress);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(932, 85);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "地址：";
            // 
            // btnGetAreaInfo
            // 
            this.btnGetAreaInfo.Location = new System.Drawing.Point(500, 34);
            this.btnGetAreaInfo.Name = "btnGetAreaInfo";
            this.btnGetAreaInfo.Size = new System.Drawing.Size(105, 21);
            this.btnGetAreaInfo.TabIndex = 6;
            this.btnGetAreaInfo.Text = "获取区域信息";
            this.btnGetAreaInfo.UseVisualStyleBackColor = true;
            this.btnGetAreaInfo.Click += new System.EventHandler(this.btnGetAreaInfo_Click);
            // 
            // txtAreaAddress
            // 
            this.txtAreaAddress.Location = new System.Drawing.Point(57, 34);
            this.txtAreaAddress.Name = "txtAreaAddress";
            this.txtAreaAddress.Size = new System.Drawing.Size(437, 21);
            this.txtAreaAddress.TabIndex = 5;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(149, 12);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "统计用区划和城乡划分代码";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeArea);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 85);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(932, 489);
            this.panel2.TabIndex = 1;
            // 
            // treeArea
            // 
            this.treeArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeArea.Location = new System.Drawing.Point(0, 0);
            this.treeArea.Name = "treeArea";
            this.treeArea.Size = new System.Drawing.Size(932, 489);
            this.treeArea.TabIndex = 0;
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Location = new System.Drawing.Point(10, 65);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(0, 12);
            this.lbMessage.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 574);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "从国家统计局获取区域信息";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetAreaInfo;
        private System.Windows.Forms.TextBox txtAreaAddress;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView treeArea;
        private System.Windows.Forms.Label lbMessage;
    }
}

