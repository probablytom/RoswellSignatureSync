namespace LocalSignatureManager
{
    partial class SyncManagerForm
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
            this.SaveAndCloseButton = new System.Windows.Forms.Button();
            this.RevertButton = new System.Windows.Forms.Button();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.VerifyPasswordLabel = new System.Windows.Forms.Label();
            this.PasswordBox2 = new System.Windows.Forms.TextBox();
            this.PasswordBox1 = new System.Windows.Forms.TextBox();
            this.VerifyButton = new System.Windows.Forms.Button();
            this.LocalSigEventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.LocalSigEventLog)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveAndCloseButton
            // 
            this.SaveAndCloseButton.Location = new System.Drawing.Point(289, 119);
            this.SaveAndCloseButton.Name = "SaveAndCloseButton";
            this.SaveAndCloseButton.Size = new System.Drawing.Size(98, 23);
            this.SaveAndCloseButton.TabIndex = 4;
            this.SaveAndCloseButton.Text = "Save and Close";
            this.SaveAndCloseButton.UseVisualStyleBackColor = true;
            this.SaveAndCloseButton.Click += new System.EventHandler(this.SaveAndCloseButton_Click);
            // 
            // RevertButton
            // 
            this.RevertButton.Location = new System.Drawing.Point(208, 119);
            this.RevertButton.Name = "RevertButton";
            this.RevertButton.Size = new System.Drawing.Size(75, 23);
            this.RevertButton.TabIndex = 5;
            this.RevertButton.Text = "Revert";
            this.RevertButton.UseVisualStyleBackColor = true;
            this.RevertButton.Click += new System.EventHandler(this.RevertButton_Click);
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(12, 15);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(66, 13);
            this.AddressLabel.TabIndex = 6;
            this.AddressLabel.Text = "Login (email)";
            // 
            // UsernameBox
            // 
            this.UsernameBox.Location = new System.Drawing.Point(100, 12);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(287, 20);
            this.UsernameBox.TabIndex = 7;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(12, 41);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.PasswordLabel.TabIndex = 8;
            this.PasswordLabel.Text = "Password";
            // 
            // VerifyPasswordLabel
            // 
            this.VerifyPasswordLabel.AutoSize = true;
            this.VerifyPasswordLabel.Location = new System.Drawing.Point(12, 67);
            this.VerifyPasswordLabel.Name = "VerifyPasswordLabel";
            this.VerifyPasswordLabel.Size = new System.Drawing.Size(82, 13);
            this.VerifyPasswordLabel.TabIndex = 9;
            this.VerifyPasswordLabel.Text = "Verify Password";
            // 
            // PasswordBox2
            // 
            this.PasswordBox2.Location = new System.Drawing.Point(100, 64);
            this.PasswordBox2.Name = "PasswordBox2";
            this.PasswordBox2.PasswordChar = '●';
            this.PasswordBox2.Size = new System.Drawing.Size(287, 20);
            this.PasswordBox2.TabIndex = 10;
            // 
            // PasswordBox1
            // 
            this.PasswordBox1.Location = new System.Drawing.Point(100, 38);
            this.PasswordBox1.Name = "PasswordBox1";
            this.PasswordBox1.PasswordChar = '●';
            this.PasswordBox1.Size = new System.Drawing.Size(287, 20);
            this.PasswordBox1.TabIndex = 11;
            // 
            // VerifyButton
            // 
            this.VerifyButton.Location = new System.Drawing.Point(289, 90);
            this.VerifyButton.Name = "VerifyButton";
            this.VerifyButton.Size = new System.Drawing.Size(98, 23);
            this.VerifyButton.TabIndex = 12;
            this.VerifyButton.Text = "Verify connection";
            this.VerifyButton.UseVisualStyleBackColor = true;
            // 
            // LocalSigEventLog
            // 
            this.LocalSigEventLog.Log = "RoswellSignatureSync";
            this.LocalSigEventLog.Source = "RoswellSignatureSyncManager";
            this.LocalSigEventLog.SynchronizingObject = this;
            // 
            // SyncManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 153);
            this.Controls.Add(this.VerifyButton);
            this.Controls.Add(this.PasswordBox1);
            this.Controls.Add(this.PasswordBox2);
            this.Controls.Add(this.VerifyPasswordLabel);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UsernameBox);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.RevertButton);
            this.Controls.Add(this.SaveAndCloseButton);
            this.Name = "SyncManagerForm";
            this.Text = "Signature Sync Manager Local Edition";
            this.Load += new System.EventHandler(this.SyncManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LocalSigEventLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveAndCloseButton;
        private System.Windows.Forms.Button RevertButton;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label VerifyPasswordLabel;
        private System.Windows.Forms.TextBox PasswordBox2;
        private System.Windows.Forms.TextBox PasswordBox1;
        private System.Windows.Forms.Button VerifyButton;
        private System.Diagnostics.EventLog LocalSigEventLog;
    }
}

