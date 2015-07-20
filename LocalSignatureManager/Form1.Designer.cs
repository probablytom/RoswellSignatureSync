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
            this.LoginLabel = new System.Windows.Forms.Label();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SaveAndCloseButton
            // 
            this.SaveAndCloseButton.Location = new System.Drawing.Point(289, 81);
            this.SaveAndCloseButton.Name = "SaveAndCloseButton";
            this.SaveAndCloseButton.Size = new System.Drawing.Size(98, 23);
            this.SaveAndCloseButton.TabIndex = 4;
            this.SaveAndCloseButton.Text = "Save and Close";
            this.SaveAndCloseButton.UseVisualStyleBackColor = true;
            this.SaveAndCloseButton.Click += new System.EventHandler(this.SaveAndCloseButton_Click);
            // 
            // RevertButton
            // 
            this.RevertButton.Location = new System.Drawing.Point(208, 81);
            this.RevertButton.Name = "RevertButton";
            this.RevertButton.Size = new System.Drawing.Size(75, 23);
            this.RevertButton.TabIndex = 5;
            this.RevertButton.Text = "Revert";
            this.RevertButton.UseVisualStyleBackColor = true;
            this.RevertButton.Click += new System.EventHandler(this.RevertButton_Click);
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Location = new System.Drawing.Point(12, 15);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(36, 13);
            this.LoginLabel.TabIndex = 6;
            this.LoginLabel.Text = "Login:";
            // 
            // UsernameBox
            // 
            this.UsernameBox.Location = new System.Drawing.Point(54, 12);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(333, 20);
            this.UsernameBox.TabIndex = 7;
            // 
            // SyncManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 116);
            this.Controls.Add(this.UsernameBox);
            this.Controls.Add(this.LoginLabel);
            this.Controls.Add(this.RevertButton);
            this.Controls.Add(this.SaveAndCloseButton);
            this.Name = "SyncManagerForm";
            this.Text = "Signature Sync Manager Local Edition";
            this.Load += new System.EventHandler(this.SyncManagerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveAndCloseButton;
        private System.Windows.Forms.Button RevertButton;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.TextBox UsernameBox;
    }
}

