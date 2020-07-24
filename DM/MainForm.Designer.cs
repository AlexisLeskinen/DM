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
            this.start = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.thuner_path = new System.Windows.Forms.Label();
            this.path_text_box = new System.Windows.Forms.TextBox();
            this.path_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DHXY_text_box = new System.Windows.Forms.TextBox();
            this.DHXY_button = new System.Windows.Forms.Button();
            this.account_list = new System.Windows.Forms.ListView();
            this.column_account = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_password = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.read_account = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(599, 398);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "启动";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(694, 398);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // thuner_path
            // 
            this.thuner_path.AutoSize = true;
            this.thuner_path.Location = new System.Drawing.Point(48, 27);
            this.thuner_path.Name = "thuner_path";
            this.thuner_path.Size = new System.Drawing.Size(127, 15);
            this.thuner_path.TabIndex = 2;
            this.thuner_path.Text = "雷电模拟器路径：";
            // 
            // path_text_box
            // 
            this.path_text_box.Location = new System.Drawing.Point(181, 24);
            this.path_text_box.Name = "path_text_box";
            this.path_text_box.Size = new System.Drawing.Size(387, 25);
            this.path_text_box.TabIndex = 3;
            // 
            // path_button
            // 
            this.path_button.Location = new System.Drawing.Point(599, 23);
            this.path_button.Name = "path_button";
            this.path_button.Size = new System.Drawing.Size(75, 23);
            this.path_button.TabIndex = 4;
            this.path_button.Text = "确认";
            this.path_button.UseVisualStyleBackColor = true;
            this.path_button.Click += new System.EventHandler(this.path_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "大话西游路径：";
            // 
            // DHXY_text_box
            // 
            this.DHXY_text_box.Location = new System.Drawing.Point(181, 70);
            this.DHXY_text_box.Name = "DHXY_text_box";
            this.DHXY_text_box.Size = new System.Drawing.Size(387, 25);
            this.DHXY_text_box.TabIndex = 6;
            // 
            // DHXY_button
            // 
            this.DHXY_button.Location = new System.Drawing.Point(599, 70);
            this.DHXY_button.Name = "DHXY_button";
            this.DHXY_button.Size = new System.Drawing.Size(75, 23);
            this.DHXY_button.TabIndex = 7;
            this.DHXY_button.Text = "确认";
            this.DHXY_button.UseVisualStyleBackColor = true;
            this.DHXY_button.Click += new System.EventHandler(this.DHXY_button_Click);
            // 
            // account_list
            // 
            this.account_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_account,
            this.column_password,
            this.column_status});
            this.account_list.GridLines = true;
            this.account_list.HideSelection = false;
            this.account_list.Location = new System.Drawing.Point(51, 116);
            this.account_list.Name = "account_list";
            this.account_list.Size = new System.Drawing.Size(517, 305);
            this.account_list.TabIndex = 8;
            this.account_list.UseCompatibleStateImageBehavior = false;
            this.account_list.View = System.Windows.Forms.View.Details;
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
            this.column_password.Width = 120;
            // 
            // column_status
            // 
            this.column_status.Text = "状态";
            this.column_status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_status.Width = 120;
            // 
            // read_account
            // 
            this.read_account.Location = new System.Drawing.Point(599, 116);
            this.read_account.Name = "read_account";
            this.read_account.Size = new System.Drawing.Size(75, 23);
            this.read_account.TabIndex = 9;
            this.read_account.Text = "导入账号";
            this.read_account.UseVisualStyleBackColor = true;
            this.read_account.Click += new System.EventHandler(this.read_account_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.read_account);
            this.Controls.Add(this.account_list);
            this.Controls.Add(this.DHXY_button);
            this.Controls.Add(this.DHXY_text_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.path_button);
            this.Controls.Add(this.path_text_box);
            this.Controls.Add(this.thuner_path);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "一个窗口";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_Closed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label thuner_path;
        private System.Windows.Forms.TextBox path_text_box;
        private System.Windows.Forms.Button path_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DHXY_text_box;
        private System.Windows.Forms.Button DHXY_button;
        private System.Windows.Forms.ListView account_list;
        private System.Windows.Forms.Button read_account;
        private System.Windows.Forms.ColumnHeader column_account;
        private System.Windows.Forms.ColumnHeader column_password;
        private System.Windows.Forms.ColumnHeader column_status;
    }
}

