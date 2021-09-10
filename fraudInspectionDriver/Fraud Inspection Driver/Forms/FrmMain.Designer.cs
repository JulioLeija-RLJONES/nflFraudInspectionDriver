namespace RLJones.FraudInspectionDriver.Forms
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            this.LblStatus = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openCrossdockInspectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.LblBlinker = new System.Windows.Forms.Label();
            this.prompt = new System.Windows.Forms.RichTextBox();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblStatus
            // 
            this.LblStatus.BackColor = System.Drawing.Color.Black;
            this.LblStatus.Font = new System.Drawing.Font("Courier New", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatus.ForeColor = System.Drawing.Color.Lime;
            this.LblStatus.Location = new System.Drawing.Point(0, 0);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(1606, 51);
            this.LblStatus.TabIndex = 0;
            this.LblStatus.Text = "Fraud Inspection Driver Running...";
            this.LblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCrossdockInspectionToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(258, 76);
            // 
            // openCrossdockInspectionToolStripMenuItem
            // 
            this.openCrossdockInspectionToolStripMenuItem.Name = "openCrossdockInspectionToolStripMenuItem";
            this.openCrossdockInspectionToolStripMenuItem.Size = new System.Drawing.Size(257, 24);
            this.openCrossdockInspectionToolStripMenuItem.Text = "Open Crossdock Inspection";
            this.openCrossdockInspectionToolStripMenuItem.Click += new System.EventHandler(this.openCrossdockInspectionToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(257, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(257, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Interval = 1000;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // LblBlinker
            // 
            this.LblBlinker.BackColor = System.Drawing.Color.Lime;
            this.LblBlinker.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblBlinker.Dock = System.Windows.Forms.DockStyle.Right;
            this.LblBlinker.ForeColor = System.Drawing.Color.Black;
            this.LblBlinker.Location = new System.Drawing.Point(1904, 0);
            this.LblBlinker.Name = "LblBlinker";
            this.LblBlinker.Size = new System.Drawing.Size(38, 73);
            this.LblBlinker.TabIndex = 2;
            // 
            // prompt
            // 
            this.prompt.BackColor = System.Drawing.Color.Black;
            this.prompt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prompt.ForeColor = System.Drawing.Color.Lime;
            this.prompt.Location = new System.Drawing.Point(10, 43);
            this.prompt.Name = "prompt";
            this.prompt.Size = new System.Drawing.Size(595, 20);
            this.prompt.TabIndex = 3;
            this.prompt.Text = "";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1942, 73);
            this.ContextMenuStrip = this.MainMenu;
            this.Controls.Add(this.prompt);
            this.Controls.Add(this.LblBlinker);
            this.Controls.Add(this.LblStatus);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMain";
            this.ShowInTaskbar = false;
            this.Text = "FrmMain";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.MainMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.ContextMenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.Label LblBlinker;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCrossdockInspectionToolStripMenuItem;
        private System.Windows.Forms.RichTextBox prompt;
    }
}