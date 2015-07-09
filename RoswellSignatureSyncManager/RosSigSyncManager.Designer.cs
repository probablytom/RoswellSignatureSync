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
            this.AddUserButton = new System.Windows.Forms.Button();
            this.RemoveUserButton = new System.Windows.Forms.Button();
            this.UserDetailGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.UserDetailGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SigPathBox
            // 
            this.SigPathBox.Location = new System.Drawing.Point(100, 17);
            this.SigPathBox.Name = "SigPathBox";
            this.SigPathBox.Size = new System.Drawing.Size(271, 20);
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
            this.SigPathBrowse.Location = new System.Drawing.Point(377, 15);
            this.SigPathBrowse.Name = "SigPathBrowse";
            this.SigPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.SigPathBrowse.TabIndex = 2;
            this.SigPathBrowse.Text = "Browse";
            this.SigPathBrowse.UseVisualStyleBackColor = true;
            this.SigPathBrowse.Click += new System.EventHandler(this.SigPathBrowse_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(377, 246);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(296, 246);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // AddUserButton
            // 
            this.AddUserButton.Location = new System.Drawing.Point(15, 59);
            this.AddUserButton.Name = "AddUserButton";
            this.AddUserButton.Size = new System.Drawing.Size(210, 23);
            this.AddUserButton.TabIndex = 5;
            this.AddUserButton.Text = "Add User";
            this.AddUserButton.UseVisualStyleBackColor = true;
            this.AddUserButton.Click += new System.EventHandler(this.AddUserButton_Click);
            // 
            // RemoveUserButton
            // 
            this.RemoveUserButton.Location = new System.Drawing.Point(242, 59);
            this.RemoveUserButton.Name = "RemoveUserButton";
            this.RemoveUserButton.Size = new System.Drawing.Size(210, 23);
            this.RemoveUserButton.TabIndex = 6;
            this.RemoveUserButton.Text = "Remove User";
            this.RemoveUserButton.UseVisualStyleBackColor = true;
            // 
            // UserDetailGrid
            // 
            this.UserDetailGrid.AllowUserToAddRows = false;
            this.UserDetailGrid.AllowUserToDeleteRows = false;
            this.UserDetailGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserDetailGrid.Location = new System.Drawing.Point(15, 88);
            this.UserDetailGrid.Name = "UserDetailGrid";
            this.UserDetailGrid.ReadOnly = true;
            this.UserDetailGrid.Size = new System.Drawing.Size(437, 152);
            this.UserDetailGrid.TabIndex = 7;
            // 
            // SignatureManagerHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.UserDetailGrid);
            this.Controls.Add(this.RemoveUserButton);
            this.Controls.Add(this.AddUserButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SigPathBrowse);
            this.Controls.Add(this.SigPathLabel);
            this.Controls.Add(this.SigPathBox);
            this.Name = "SignatureManagerHome";
            this.Text = "Roswell Signature Sync Manager";
            ((System.ComponentModel.ISupportInitialize)(this.UserDetailGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SigPathBox;
        private System.Windows.Forms.Label SigPathLabel;
        private System.Windows.Forms.Button SigPathBrowse;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button AddUserButton;
        private System.Windows.Forms.Button RemoveUserButton;
        private System.Windows.Forms.DataGridView UserDetailGrid;
    }
}

