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
            this.PathUpdateButton = new System.Windows.Forms.Button();
            this.sigDestinationUpdate = new System.Windows.Forms.Button();
            this.SigDestinationBrowse = new System.Windows.Forms.Button();
            this.sigDestinationLabel = new System.Windows.Forms.Label();
            this.sigDestinationBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SigPathBox
            // 
            this.SigPathBox.Location = new System.Drawing.Point(127, 16);
            this.SigPathBox.Name = "SigPathBox";
            this.SigPathBox.Size = new System.Drawing.Size(455, 20);
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
            this.SigPathBrowse.Location = new System.Drawing.Point(588, 15);
            this.SigPathBrowse.Name = "SigPathBrowse";
            this.SigPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.SigPathBrowse.TabIndex = 2;
            this.SigPathBrowse.Text = "Browse";
            this.SigPathBrowse.UseVisualStyleBackColor = true;
            this.SigPathBrowse.Click += new System.EventHandler(this.SigPathBrowse_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(669, 101);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(588, 101);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // PathUpdateButton
            // 
            this.PathUpdateButton.Location = new System.Drawing.Point(669, 14);
            this.PathUpdateButton.Name = "PathUpdateButton";
            this.PathUpdateButton.Size = new System.Drawing.Size(75, 23);
            this.PathUpdateButton.TabIndex = 5;
            this.PathUpdateButton.Text = "Update";
            this.PathUpdateButton.UseVisualStyleBackColor = true;
            this.PathUpdateButton.Click += new System.EventHandler(this.PathUpdateButton_Click);
            // 
            // sigDestinationUpdate
            // 
            this.sigDestinationUpdate.Location = new System.Drawing.Point(669, 43);
            this.sigDestinationUpdate.Name = "sigDestinationUpdate";
            this.sigDestinationUpdate.Size = new System.Drawing.Size(75, 23);
            this.sigDestinationUpdate.TabIndex = 9;
            this.sigDestinationUpdate.Text = "Update";
            this.sigDestinationUpdate.UseVisualStyleBackColor = true;
            this.sigDestinationUpdate.Click += new System.EventHandler(this.sigDestinationUpdate_Click);
            // 
            // SigDestinationBrowse
            // 
            this.SigDestinationBrowse.Location = new System.Drawing.Point(588, 43);
            this.SigDestinationBrowse.Name = "SigDestinationBrowse";
            this.SigDestinationBrowse.Size = new System.Drawing.Size(75, 23);
            this.SigDestinationBrowse.TabIndex = 8;
            this.SigDestinationBrowse.Text = "Browse";
            this.SigDestinationBrowse.UseVisualStyleBackColor = true;
            this.SigDestinationBrowse.Click += new System.EventHandler(this.SigDestinationBrowse_Click);
            // 
            // sigDestinationLabel
            // 
            this.sigDestinationLabel.AutoSize = true;
            this.sigDestinationLabel.Location = new System.Drawing.Point(12, 48);
            this.sigDestinationLabel.Name = "sigDestinationLabel";
            this.sigDestinationLabel.Size = new System.Drawing.Size(109, 13);
            this.sigDestinationLabel.TabIndex = 7;
            this.sigDestinationLabel.Text = "Signature destination:";
            // 
            // sigDestinationBox
            // 
            this.sigDestinationBox.Location = new System.Drawing.Point(127, 46);
            this.sigDestinationBox.Name = "sigDestinationBox";
            this.sigDestinationBox.Size = new System.Drawing.Size(455, 20);
            this.sigDestinationBox.TabIndex = 6;
            // 
            // SignatureManagerHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 134);
            this.Controls.Add(this.sigDestinationUpdate);
            this.Controls.Add(this.SigDestinationBrowse);
            this.Controls.Add(this.sigDestinationLabel);
            this.Controls.Add(this.sigDestinationBox);
            this.Controls.Add(this.PathUpdateButton);
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
        private System.Windows.Forms.Button PathUpdateButton;
        private System.Windows.Forms.Button sigDestinationUpdate;
        private System.Windows.Forms.Button SigDestinationBrowse;
        private System.Windows.Forms.Label sigDestinationLabel;
        private System.Windows.Forms.TextBox sigDestinationBox;
    }
}

