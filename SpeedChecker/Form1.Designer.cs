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
            contextMenuStrip1 = new ContextMenuStrip(components);
            ShowToolStripMenuItem = new ToolStripMenuItem();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            DownloadTextBox = new TextBox();
            checkBox2 = new CheckBox();
            DbWritableCheckBox = new CheckBox();
            TimerTextBox = new TextBox();
            HourCheckCheckBox = new CheckBox();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Font = new Font("Yu Gothic UI", 30F);
            button1.Location = new Point(12, 43);
            button1.Name = "button1";
            button1.Size = new Size(379, 129);
            button1.TabIndex = 0;
            button1.Text = "計測(&C)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // userNameTextBox
            // 
            userNameTextBox.Location = new Point(81, 6);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(310, 31);
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
            AutoRestartCheckBox.Location = new Point(12, 367);
            AutoRestartCheckBox.Name = "AutoRestartCheckBox";
            AutoRestartCheckBox.Size = new Size(199, 29);
            AutoRestartCheckBox.TabIndex = 3;
            AutoRestartCheckBox.Text = "PC再起動に自動起動";
            AutoRestartCheckBox.UseVisualStyleBackColor = true;
            AutoRestartCheckBox.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "SpeedChecker";
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ShowToolStripMenuItem, ExitToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(142, 68);
            // 
            // ShowToolStripMenuItem
            // 
            ShowToolStripMenuItem.Name = "ShowToolStripMenuItem";
            ShowToolStripMenuItem.Size = new Size(141, 32);
            ShowToolStripMenuItem.Text = "表示(&S)";
            ShowToolStripMenuItem.Click += ShowToolStripMenuItem_Click;
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new Size(141, 32);
            ExitToolStripMenuItem.Text = "終了(&X)";
            ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // DownloadTextBox
            // 
            DownloadTextBox.Location = new Point(12, 251);
            DownloadTextBox.Multiline = true;
            DownloadTextBox.Name = "DownloadTextBox";
            DownloadTextBox.ReadOnly = true;
            DownloadTextBox.ScrollBars = ScrollBars.Vertical;
            DownloadTextBox.Size = new Size(379, 110);
            DownloadTextBox.TabIndex = 4;
            DownloadTextBox.WordWrap = false;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(211, 367);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(180, 29);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "タスクトレイに入れる";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // DbWritableCheckBox
            // 
            DbWritableCheckBox.AutoSize = true;
            DbWritableCheckBox.Location = new Point(12, 216);
            DbWritableCheckBox.Name = "DbWritableCheckBox";
            DbWritableCheckBox.Size = new Size(226, 29);
            DbWritableCheckBox.TabIndex = 9;
            DbWritableCheckBox.Text = "計測結果をDBに書き込む";
            DbWritableCheckBox.UseVisualStyleBackColor = true;
            // 
            // TimerTextBox
            // 
            TimerTextBox.Location = new Point(310, 178);
            TimerTextBox.Name = "TimerTextBox";
            TimerTextBox.ReadOnly = true;
            TimerTextBox.Size = new Size(81, 31);
            TimerTextBox.TabIndex = 10;
            TimerTextBox.Text = "00:00:00";
            TimerTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // HourCheckCheckBox
            // 
            HourCheckCheckBox.AutoSize = true;
            HourCheckCheckBox.Location = new Point(12, 181);
            HourCheckCheckBox.Name = "HourCheckCheckBox";
            HourCheckCheckBox.Size = new Size(189, 29);
            HourCheckCheckBox.TabIndex = 11;
            HourCheckCheckBox.Text = "毎時間事に計測する";
            HourCheckCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 407);
            Controls.Add(HourCheckCheckBox);
            Controls.Add(TimerTextBox);
            Controls.Add(DbWritableCheckBox);
            Controls.Add(checkBox2);
            Controls.Add(DownloadTextBox);
            Controls.Add(AutoRestartCheckBox);
            Controls.Add(label1);
            Controls.Add(userNameTextBox);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "SpeedChecker";
            FormClosing += Form1_FormClosing;
            Resize += Form1_Resize;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox userNameTextBox;
        private Label label1;
        private CheckBox AutoRestartCheckBox;
        private NotifyIcon notifyIcon1;
        private TextBox DownloadTextBox;
        private CheckBox checkBox2;
        private CheckBox DbWritableCheckBox;
        private TextBox TimerTextBox;
        private CheckBox HourCheckCheckBox;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ShowToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
    }
}
