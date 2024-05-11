namespace p_tech_lab_6
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
            pbMain = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            txtLog = new RichTextBox();
            newObjTimer = new System.Windows.Forms.Timer(components);
            scoreTxt = new Label();
            ((System.ComponentModel.ISupportInitialize)pbMain).BeginInit();
            SuspendLayout();
            // 
            // pbMain
            // 
            pbMain.Location = new Point(23, 14);
            pbMain.Margin = new Padding(2);
            pbMain.Name = "pbMain";
            pbMain.Size = new Size(600, 429);
            pbMain.TabIndex = 0;
            pbMain.TabStop = false;
            pbMain.Paint += pbMain_Paint;
            pbMain.MouseClick += pbMain_MouseClick;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 30;
            timer1.Tick += timer1_Tick;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(635, 15);
            txtLog.Margin = new Padding(2);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(251, 429);
            txtLog.TabIndex = 1;
            txtLog.Text = "";
            // 
            // newObjTimer
            // 
            newObjTimer.Enabled = true;
            newObjTimer.Interval = 10000;
            newObjTimer.Tick += newObjTimer_Tick;
            // 
            // scoreTxt
            // 
            scoreTxt.AutoSize = true;
            scoreTxt.Location = new Point(545, 28);
            scoreTxt.Name = "scoreTxt";
            scoreTxt.Size = new Size(48, 15);
            scoreTxt.TabIndex = 2;
            scoreTxt.Text = "Очки: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(893, 461);
            Controls.Add(scoreTxt);
            Controls.Add(txtLog);
            Controls.Add(pbMain);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbMain).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbMain;
        private System.Windows.Forms.Timer timer1;
        private RichTextBox txtLog;
        private System.Windows.Forms.Timer newObjTimer;
        private Label scoreTxt;
    }
}
