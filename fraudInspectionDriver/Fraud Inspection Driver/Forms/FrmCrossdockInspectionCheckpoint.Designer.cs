namespace RLJones.FraudInspectionDriver.Forms
{
    partial class FrmCrossdockInspectionCheckpoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCrossdockInspectionCheckpoint));
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_orderNumber = new System.Windows.Forms.TextBox();
            this.prompt = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblsessionCount = new System.Windows.Forms.Label();
            this.lblStationName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(10, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(581, 33);
            this.label2.TabIndex = 13;
            this.label2.Text = "Scan Order Number after inspected:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_orderNumber
            // 
            this.textBox_orderNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_orderNumber.Location = new System.Drawing.Point(12, 86);
            this.textBox_orderNumber.Name = "textBox_orderNumber";
            this.textBox_orderNumber.Size = new System.Drawing.Size(315, 61);
            this.textBox_orderNumber.TabIndex = 14;
            this.textBox_orderNumber.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox_orderNumber_PreviewKeyDown);
            // 
            // prompt
            // 
            this.prompt.BackColor = System.Drawing.SystemColors.MenuText;
            this.prompt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prompt.ForeColor = System.Drawing.Color.Lime;
            this.prompt.Location = new System.Drawing.Point(6, 366);
            this.prompt.Name = "prompt";
            this.prompt.Size = new System.Drawing.Size(843, 96);
            this.prompt.TabIndex = 15;
            this.prompt.Text = "";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(7, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 35);
            this.label1.TabIndex = 13;
            this.label1.Text = "Scan log:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblsessionCount
            // 
            this.lblsessionCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsessionCount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblsessionCount.Location = new System.Drawing.Point(528, 328);
            this.lblsessionCount.Name = "lblsessionCount";
            this.lblsessionCount.Size = new System.Drawing.Size(301, 35);
            this.lblsessionCount.TabIndex = 13;
            this.lblsessionCount.Text = "Session count:";
            this.lblsessionCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStationName
            // 
            this.lblStationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStationName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblStationName.Location = new System.Drawing.Point(528, 282);
            this.lblStationName.Name = "lblStationName";
            this.lblStationName.Size = new System.Drawing.Size(301, 35);
            this.lblStationName.TabIndex = 13;
            this.lblStationName.Text = "Worstation:";
            this.lblStationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmCrossdockInspectionCheckpoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(861, 474);
            this.Controls.Add(this.prompt);
            this.Controls.Add(this.textBox_orderNumber);
            this.Controls.Add(this.lblStationName);
            this.Controls.Add(this.lblsessionCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCrossdockInspectionCheckpoint";
            this.Text = "NFL Crossdock Inspection Point";
            this.Load += new System.EventHandler(this.FrmCrossdockInspectionCheckpoint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_orderNumber;
        private System.Windows.Forms.RichTextBox prompt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblsessionCount;
        private System.Windows.Forms.Label lblStationName;
    }
}