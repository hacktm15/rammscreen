namespace RammScreen
{
    partial class MainForm
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
            this.captureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.captureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // captureBox
            // 
            this.captureBox.Location = new System.Drawing.Point(0, 0);
            this.captureBox.Margin = new System.Windows.Forms.Padding(4);
            this.captureBox.Name = "captureBox";
            this.captureBox.Size = new System.Drawing.Size(116, 66);
            this.captureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.captureBox.TabIndex = 0;
            this.captureBox.TabStop = false;
            this.captureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.captureBox_MouseDownUp);
            this.captureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.captureBox_MouseMove);
            this.captureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.captureBox_MouseDownUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(332, 341);
            this.Controls.Add(this.captureBox);
            this.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RammScreen";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.captureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox captureBox;
    }
}

