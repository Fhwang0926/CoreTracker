namespace CoreTracker
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.pb_download = new System.Windows.Forms.ProgressBar();
            this.lb_download_status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pb_download
            // 
            this.pb_download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_download.Location = new System.Drawing.Point(12, 46);
            this.pb_download.Name = "pb_download";
            this.pb_download.Size = new System.Drawing.Size(463, 23);
            this.pb_download.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_download.TabIndex = 0;
            // 
            // lb_download_status
            // 
            this.lb_download_status.AutoSize = true;
            this.lb_download_status.Location = new System.Drawing.Point(13, 13);
            this.lb_download_status.Name = "lb_download_status";
            this.lb_download_status.Size = new System.Drawing.Size(41, 12);
            this.lb_download_status.TabIndex = 1;
            this.lb_download_status.Text = "Ready";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 81);
            this.Controls.Add(this.lb_download_status);
            this.Controls.Add(this.pb_download);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb_download;
        private System.Windows.Forms.Label lb_download_status;
    }
}