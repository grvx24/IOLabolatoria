namespace Bitmapy
{
    partial class Form1
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
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            this.LoadImgButton = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.PathLabel = new System.Windows.Forms.Label();
            this.LoadAsyncButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPictureBox
            // 
            this.MainPictureBox.Location = new System.Drawing.Point(12, 12);
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.Size = new System.Drawing.Size(739, 503);
            this.MainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MainPictureBox.TabIndex = 0;
            this.MainPictureBox.TabStop = false;
            // 
            // LoadImgButton
            // 
            this.LoadImgButton.Location = new System.Drawing.Point(757, 132);
            this.LoadImgButton.Name = "LoadImgButton";
            this.LoadImgButton.Size = new System.Drawing.Size(372, 84);
            this.LoadImgButton.TabIndex = 1;
            this.LoadImgButton.Text = "Load image";
            this.LoadImgButton.UseVisualStyleBackColor = true;
            this.LoadImgButton.Click += new System.EventHandler(this.LoadImgButton_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(757, 32);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(372, 22);
            this.PathTextBox.TabIndex = 2;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(757, 60);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(372, 45);
            this.BrowseButton.TabIndex = 4;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.Location = new System.Drawing.Point(757, 12);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(37, 17);
            this.PathLabel.TabIndex = 5;
            this.PathLabel.Text = "Path";
            // 
            // LoadAsyncButton
            // 
            this.LoadAsyncButton.Location = new System.Drawing.Point(757, 238);
            this.LoadAsyncButton.Name = "LoadAsyncButton";
            this.LoadAsyncButton.Size = new System.Drawing.Size(372, 84);
            this.LoadAsyncButton.TabIndex = 6;
            this.LoadAsyncButton.Text = "Load image Async";
            this.LoadAsyncButton.UseVisualStyleBackColor = true;
            this.LoadAsyncButton.Click += new System.EventHandler(this.LoadAsyncButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 610);
            this.Controls.Add(this.LoadAsyncButton);
            this.Controls.Add(this.PathLabel);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.LoadImgButton);
            this.Controls.Add(this.MainPictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MainPictureBox;
        private System.Windows.Forms.Button LoadImgButton;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.Button LoadAsyncButton;
    }
}

