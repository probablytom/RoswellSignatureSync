namespace RoswellSignatureSyncManager
{
    partial class SignatureManagerHome
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
            this.SigPathBox = new System.Windows.Forms.TextBox();
            this.SigPathLabel = new System.Windows.Forms.Label();
            this.SigPathBrowse = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SigPathBox
            // 
            this.SigPathBox.Location = new System.Drawing.Point(100, 17);
            this.SigPathBox.Name = "SigPathBox";
            this.SigPathBox.Size = new System.Drawing.Size(272, 20);
            this.SigPathBox.TabIndex = 0;
            // 
            // SigPathLabel
            // 
            this.SigPathLabel.AutoSize = true;
            this.SigPathLabel.Location = new System.Drawing.Point(12, 20);
            this.SigPathLabel.Name = "SigPathLabel";
            this.SigPathLabel.Size = new System.Drawing.Size(82, 13);
            this.SigPathLabel.TabIndex = 1;
            this.SigPathLabel.Text = "Signature path: ";
            // 
            // SigPathBrowse
            // 
            this.SigPathBrowse.Location = new System.Drawing.Point(378, 14);
            this.SigPathBrowse.Name = "SigPathBrowse";
            this.SigPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.SigPathBrowse.TabIndex = 2;
            this.SigPathBrowse.Text = "Browse";
            this.SigPathBrowse.UseVisualStyleBackColor = true;
            this.SigPathBrowse.Click += new System.EventHandler(this.SigPathBrowse_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(458, 73);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(377, 73);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(459, 14);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 5;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // SignatureManagerHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 108);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SigPathBrowse);
            this.Controls.Add(this.SigPathLabel);
            this.Controls.Add(this.SigPathBox);
            this.Name = "SignatureManagerHome";
            this.Text = "Roswell Signature Sync Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SigPathBox;
        private System.Windows.Forms.Label SigPathLabel;
        private System.Windows.Forms.Button SigPathBrowse;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button UpdateButton;
    }
}

