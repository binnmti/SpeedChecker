namespace SpeedChecker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            userNameTextBox = new TextBox();
            label1 = new Label();
            checkBox1 = new CheckBox();
            notifyIcon1 = new NotifyIcon(components);
            timer1 = new System.Windows.Forms.Timer(components);
            DownloadTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            UploadTextBox = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(223, 63);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // userNameTextBox
            // 
            userNameTextBox.Location = new Point(112, 12);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(223, 31);
            userNameTextBox.TabIndex = 1;
            userNameTextBox.TextChanged += userNameTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(94, 25);
            label1.TabIndex = 2;
            label1.Text = "UserName";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(12, 67);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(74, 29);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "常駐";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 3600000;
            timer1.Tick += timer1_Tick;
            // 
            // DownloadTextBox
            // 
            DownloadTextBox.Location = new Point(112, 114);
            DownloadTextBox.Name = "DownloadTextBox";
            DownloadTextBox.ReadOnly = true;
            DownloadTextBox.Size = new Size(223, 31);
            DownloadTextBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 114);
            label2.Name = "label2";
            label2.Size = new Size(94, 25);
            label2.TabIndex = 5;
            label2.Text = "Download";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(36, 167);
            label3.Name = "label3";
            label3.Size = new Size(70, 25);
            label3.TabIndex = 6;
            label3.Text = "Upload";
            // 
            // UploadTextBox
            // 
            UploadTextBox.Location = new Point(112, 164);
            UploadTextBox.Name = "UploadTextBox";
            UploadTextBox.ReadOnly = true;
            UploadTextBox.Size = new Size(223, 31);
            UploadTextBox.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 201);
            Controls.Add(UploadTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(DownloadTextBox);
            Controls.Add(checkBox1);
            Controls.Add(label1);
            Controls.Add(userNameTextBox);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox userNameTextBox;
        private Label label1;
        private CheckBox checkBox1;
        private NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private TextBox DownloadTextBox;
        private Label label2;
        private Label label3;
        private TextBox UploadTextBox;
    }
}
