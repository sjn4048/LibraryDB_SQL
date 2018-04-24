namespace LibraryDB
{
    partial class Form1
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
            this.user_login_btn = new System.Windows.Forms.Button();
            this.user_register_btn = new System.Windows.Forms.Button();
            this.search_btn = new System.Windows.Forms.Button();
            this.add_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.name_label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // user_login_btn
            // 
            this.user_login_btn.Location = new System.Drawing.Point(570, 62);
            this.user_login_btn.Margin = new System.Windows.Forms.Padding(4);
            this.user_login_btn.Name = "user_login_btn";
            this.user_login_btn.Size = new System.Drawing.Size(75, 29);
            this.user_login_btn.TabIndex = 1;
            this.user_login_btn.Text = "登录";
            this.user_login_btn.UseVisualStyleBackColor = true;
            this.user_login_btn.Click += new System.EventHandler(this.user_login_btn_Click);
            // 
            // user_register_btn
            // 
            this.user_register_btn.Location = new System.Drawing.Point(491, 62);
            this.user_register_btn.Margin = new System.Windows.Forms.Padding(4);
            this.user_register_btn.Name = "user_register_btn";
            this.user_register_btn.Size = new System.Drawing.Size(71, 29);
            this.user_register_btn.TabIndex = 1;
            this.user_register_btn.Text = "注册";
            this.user_register_btn.UseVisualStyleBackColor = true;
            this.user_register_btn.Click += new System.EventHandler(this.user_register_btn_Click);
            // 
            // search_btn
            // 
            this.search_btn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.search_btn.Location = new System.Drawing.Point(345, 64);
            this.search_btn.Margin = new System.Windows.Forms.Padding(4);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(87, 27);
            this.search_btn.TabIndex = 2;
            this.search_btn.Text = "查询";
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // add_button
            // 
            this.add_button.Location = new System.Drawing.Point(709, 62);
            this.add_button.Margin = new System.Windows.Forms.Padding(4);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(83, 27);
            this.add_button.TabIndex = 3;
            this.add_button.Text = "添加图书";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(113, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(209, 25);
            this.textBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(28, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "关键词";
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name_label.Location = new System.Drawing.Point(28, 23);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(69, 20);
            this.name_label.TabIndex = 6;
            this.name_label.Text = "未登录";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(36, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(836, 421);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(800, 62);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "管理";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 562);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.name_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.search_btn);
            this.Controls.Add(this.user_register_btn);
            this.Controls.Add(this.user_login_btn);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "图书管理系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button user_login_btn;
        private System.Windows.Forms.Button user_register_btn;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}

