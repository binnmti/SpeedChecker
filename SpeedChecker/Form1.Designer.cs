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
            AutoRestartCheckBox = new CheckBox();
            notifyIcon1 = new NotifyIcon(components);
            timer1 = new System.Windows.Forms.Timer(components);
            DownloadTextBox = new TextBox();
            checkBox2 = new CheckBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(12, 56);
            button1.Name = "button1";
            button1.Size = new Size(607, 129);
            button1.TabIndex = 0;
            button1.Text = "計測";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // userNameTextBox
            // 
            userNameTextBox.Location = new Point(81, 6);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(538, 31);
            userNameTextBox.TabIndex = 1;
            userNameTextBox.TextChanged += userNameTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(48, 25);
            label1.TabIndex = 2;
            label1.Text = "名前";
            // 
            // AutoRestartCheckBox
            // 
            AutoRestartCheckBox.AutoSize = true;
            AutoRestartCheckBox.Location = new Point(12, 191);
            AutoRestartCheckBox.Name = "AutoRestartCheckBox";
            AutoRestartCheckBox.Size = new Size(160, 29);
            AutoRestartCheckBox.TabIndex = 3;
            AutoRestartCheckBox.Text = "自動的に再起動";
            AutoRestartCheckBox.UseVisualStyleBackColor = true;
            AutoRestartCheckBox.CheckedChanged += checkBox1_CheckedChanged;
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
            DownloadTextBox.Location = new Point(12, 226);
            DownloadTextBox.Multiline = true;
            DownloadTextBox.Name = "DownloadTextBox";
            DownloadTextBox.ReadOnly = true;
            DownloadTextBox.ScrollBars = ScrollBars.Vertical;
            DownloadTextBox.Size = new Size(607, 155);
            DownloadTextBox.TabIndex = 4;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(178, 191);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(217, 29);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "このアプリをタスクトレイ化";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(631, 391);
            Controls.Add(checkBox2);
            Controls.Add(DownloadTextBox);
            Controls.Add(AutoRestartCheckBox);
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
        private CheckBox AutoRestartCheckBox;
        private NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private TextBox DownloadTextBox;
        private CheckBox checkBox2;
    }
}
