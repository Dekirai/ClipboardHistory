namespace ClipboardHistory
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            cms = new ContextMenuStrip(components);
            btn_OpenHistoryLogs = new ToolStripMenuItem();
            cb_LaunchWithWindows = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            btn_Exit = new ToolStripMenuItem();
            notify = new NotifyIcon(components);
            cms.SuspendLayout();
            SuspendLayout();
            // 
            // cms
            // 
            cms.Items.AddRange(new ToolStripItem[] { btn_OpenHistoryLogs, cb_LaunchWithWindows, toolStripSeparator1, btn_Exit });
            cms.Name = "cms";
            cms.Size = new Size(192, 76);
            // 
            // btn_OpenHistoryLogs
            // 
            btn_OpenHistoryLogs.Name = "btn_OpenHistoryLogs";
            btn_OpenHistoryLogs.Size = new Size(191, 22);
            btn_OpenHistoryLogs.Text = "Open History Logs";
            btn_OpenHistoryLogs.Click += btn_OpenHistoryLogs_Click;
            // 
            // cb_LaunchWithWindows
            // 
            cb_LaunchWithWindows.CheckOnClick = true;
            cb_LaunchWithWindows.Name = "cb_LaunchWithWindows";
            cb_LaunchWithWindows.Size = new Size(191, 22);
            cb_LaunchWithWindows.Text = "Launch with Windows";
            cb_LaunchWithWindows.Click += cb_LaunchWithWindows_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(188, 6);
            // 
            // btn_Exit
            // 
            btn_Exit.Name = "btn_Exit";
            btn_Exit.Size = new Size(191, 22);
            btn_Exit.Text = "Exit";
            btn_Exit.Click += btn_Exit_Click;
            // 
            // notify
            // 
            notify.ContextMenuStrip = cms;
            notify.Icon = (Icon)resources.GetObject("notify.Icon");
            notify.Text = "Clipboard History";
            notify.Visible = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 37);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Clipboard History";
            cms.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip cms;
        private ToolStripMenuItem btn_OpenHistoryLogs;
        private ToolStripMenuItem cb_LaunchWithWindows;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem btn_Exit;
        private NotifyIcon notify;
    }
}
