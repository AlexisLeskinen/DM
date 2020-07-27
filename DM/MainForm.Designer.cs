namespace DM
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Button_Start = new System.Windows.Forms.Button();
            this.Button_Test = new System.Windows.Forms.Button();
            this.Label_Thunder = new System.Windows.Forms.Label();
            this.TextBox_Thunder_Path = new System.Windows.Forms.TextBox();
            this.Button_Set_Thunder = new System.Windows.Forms.Button();
            this.Label_DHXY = new System.Windows.Forms.Label();
            this.TextBox_DHXY_Path = new System.Windows.Forms.TextBox();
            this.Button_Set_DHXY = new System.Windows.Forms.Button();
            this.ListView_Account = new System.Windows.Forms.ListView();
            this.column_account = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_password = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Button_Read_Account = new System.Windows.Forms.Button();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSM_Execute = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Start
            // 
            this.Button_Start.Location = new System.Drawing.Point(572, 398);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(75, 23);
            this.Button_Start.TabIndex = 0;
            this.Button_Start.Text = "启动";
            this.Button_Start.UseVisualStyleBackColor = true;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // Button_Test
            // 
            this.Button_Test.Location = new System.Drawing.Point(640, 317);
            this.Button_Test.Name = "Button_Test";
            this.Button_Test.Size = new System.Drawing.Size(75, 23);
            this.Button_Test.TabIndex = 1;
            this.Button_Test.Text = "功能测试";
            this.Button_Test.UseVisualStyleBackColor = true;
            this.Button_Test.Click += new System.EventHandler(this.Button_Test_Click);
            // 
            // Label_Thunder
            // 
            this.Label_Thunder.AutoSize = true;
            this.Label_Thunder.Location = new System.Drawing.Point(48, 27);
            this.Label_Thunder.Name = "Label_Thunder";
            this.Label_Thunder.Size = new System.Drawing.Size(127, 15);
            this.Label_Thunder.TabIndex = 2;
            this.Label_Thunder.Text = "雷电模拟器路径：";
            // 
            // TextBox_Thunder_Path
            // 
            this.TextBox_Thunder_Path.Location = new System.Drawing.Point(181, 24);
            this.TextBox_Thunder_Path.Name = "TextBox_Thunder_Path";
            this.TextBox_Thunder_Path.Size = new System.Drawing.Size(354, 25);
            this.TextBox_Thunder_Path.TabIndex = 3;
            // 
            // Button_Set_Thunder
            // 
            this.Button_Set_Thunder.Location = new System.Drawing.Point(618, 23);
            this.Button_Set_Thunder.Name = "Button_Set_Thunder";
            this.Button_Set_Thunder.Size = new System.Drawing.Size(75, 23);
            this.Button_Set_Thunder.TabIndex = 4;
            this.Button_Set_Thunder.Text = "确认";
            this.Button_Set_Thunder.UseVisualStyleBackColor = true;
            this.Button_Set_Thunder.Click += new System.EventHandler(this.path_button_Click);
            // 
            // Label_DHXY
            // 
            this.Label_DHXY.AutoSize = true;
            this.Label_DHXY.Location = new System.Drawing.Point(48, 73);
            this.Label_DHXY.Name = "Label_DHXY";
            this.Label_DHXY.Size = new System.Drawing.Size(112, 15);
            this.Label_DHXY.TabIndex = 5;
            this.Label_DHXY.Text = "大话西游路径：";
            // 
            // TextBox_DHXY_Path
            // 
            this.TextBox_DHXY_Path.Location = new System.Drawing.Point(181, 70);
            this.TextBox_DHXY_Path.Name = "TextBox_DHXY_Path";
            this.TextBox_DHXY_Path.Size = new System.Drawing.Size(354, 25);
            this.TextBox_DHXY_Path.TabIndex = 6;
            // 
            // Button_Set_DHXY
            // 
            this.Button_Set_DHXY.Location = new System.Drawing.Point(618, 70);
            this.Button_Set_DHXY.Name = "Button_Set_DHXY";
            this.Button_Set_DHXY.Size = new System.Drawing.Size(75, 23);
            this.Button_Set_DHXY.TabIndex = 7;
            this.Button_Set_DHXY.Text = "确认";
            this.Button_Set_DHXY.UseVisualStyleBackColor = true;
            this.Button_Set_DHXY.Click += new System.EventHandler(this.Button_Set_DHXY_Click);
            // 
            // ListView_Account
            // 
            this.ListView_Account.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_account,
            this.column_password,
            this.column_status});
            this.ListView_Account.GridLines = true;
            this.ListView_Account.HideSelection = false;
            this.ListView_Account.Location = new System.Drawing.Point(51, 116);
            this.ListView_Account.Name = "ListView_Account";
            this.ListView_Account.Size = new System.Drawing.Size(484, 305);
            this.ListView_Account.TabIndex = 8;
            this.ListView_Account.UseCompatibleStateImageBehavior = false;
            this.ListView_Account.View = System.Windows.Forms.View.Details;
            this.ListView_Account.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListView_Account_MouseUp);
            // 
            // column_account
            // 
            this.column_account.Text = "账号";
            this.column_account.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_account.Width = 240;
            // 
            // column_password
            // 
            this.column_password.Text = "密码";
            this.column_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_password.Width = 115;
            // 
            // column_status
            // 
            this.column_status.Text = "状态";
            this.column_status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_status.Width = 104;
            // 
            // Button_Read_Account
            // 
            this.Button_Read_Account.Location = new System.Drawing.Point(618, 116);
            this.Button_Read_Account.Name = "Button_Read_Account";
            this.Button_Read_Account.Size = new System.Drawing.Size(75, 23);
            this.Button_Read_Account.TabIndex = 9;
            this.Button_Read_Account.Text = "导入账号";
            this.Button_Read_Account.UseVisualStyleBackColor = true;
            this.Button_Read_Account.Click += new System.EventHandler(this.Button_Read_Account_Click);
            // 
            // Button_Stop
            // 
            this.Button_Stop.Location = new System.Drawing.Point(705, 397);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(75, 23);
            this.Button_Stop.TabIndex = 10;
            this.Button_Stop.Text = "停止";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSM_Execute});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(211, 56);
            // 
            // TSM_Execute
            // 
            this.TSM_Execute.Name = "TSM_Execute";
            this.TSM_Execute.Size = new System.Drawing.Size(210, 24);
            this.TSM_Execute.Text = "注册该账号";
            this.TSM_Execute.Click += new System.EventHandler(this.TSM_Execute_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Button_Stop);
            this.Controls.Add(this.Button_Read_Account);
            this.Controls.Add(this.ListView_Account);
            this.Controls.Add(this.Button_Set_DHXY);
            this.Controls.Add(this.TextBox_DHXY_Path);
            this.Controls.Add(this.Label_DHXY);
            this.Controls.Add(this.Button_Set_Thunder);
            this.Controls.Add(this.TextBox_Thunder_Path);
            this.Controls.Add(this.Label_Thunder);
            this.Controls.Add(this.Button_Test);
            this.Controls.Add(this.Button_Start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "大话西游批量创号脚本";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_Closed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.Button Button_Test;
        private System.Windows.Forms.Label Label_Thunder;
        private System.Windows.Forms.TextBox TextBox_Thunder_Path;
        private System.Windows.Forms.Button Button_Set_Thunder;
        private System.Windows.Forms.Label Label_DHXY;
        private System.Windows.Forms.TextBox TextBox_DHXY_Path;
        private System.Windows.Forms.Button Button_Set_DHXY;
        private System.Windows.Forms.ListView ListView_Account;
        private System.Windows.Forms.Button Button_Read_Account;
        private System.Windows.Forms.ColumnHeader column_account;
        private System.Windows.Forms.ColumnHeader column_password;
        private System.Windows.Forms.ColumnHeader column_status;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem TSM_Execute;
    }
}

