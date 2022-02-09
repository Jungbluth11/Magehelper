namespace Magehelper.Updater
{
    partial class UpdaterForm
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
            this.StringHeadline = new System.Windows.Forms.Label();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.StringStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StringHeadline
            // 
            this.StringHeadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StringHeadline.Location = new System.Drawing.Point(14, 10);
            this.StringHeadline.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StringHeadline.Name = "StringHeadline";
            this.StringHeadline.Size = new System.Drawing.Size(428, 27);
            this.StringHeadline.TabIndex = 0;
            this.StringHeadline.Text = "Update wird durchgeführt.";
            this.StringHeadline.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(14, 40);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(428, 27);
            this.ProgressBar.TabIndex = 1;
            // 
            // StringStatus
            // 
            this.StringStatus.AutoSize = true;
            this.StringStatus.Location = new System.Drawing.Point(14, 78);
            this.StringStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StringStatus.Name = "StringStatus";
            this.StringStatus.Size = new System.Drawing.Size(0, 15);
            this.StringStatus.TabIndex = 2;
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 107);
            this.Controls.Add(this.StringStatus);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.StringHeadline);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdaterForm";
            this.ShowIcon = false;
            this.Text = "Magehelper Updater";
            this.Shown += new System.EventHandler(this.UpdaterForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StringHeadline;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label StringStatus;
    }
}
