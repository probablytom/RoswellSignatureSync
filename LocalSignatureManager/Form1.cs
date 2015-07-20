﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalSignatureManager
{

    public partial class SyncManagerForm : Form
    {
        
        List<List<string>> userDetailsList;
        string originalDetails = "";


        public SyncManagerForm()
        {
            InitializeComponent();
        }

        private void ChangePassButton_Click(object sender, EventArgs e)
        {

        }

        private void UserList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SyncManagerForm_Load(object sender, EventArgs e)
        {
            List<string> UserListContents = new List<string>(); 

            
            
            // TODO: GET USER DETAILS AND PUT IN userDetails TODO TODO TODO TODO



            // Add the displayname of each user to the list to be displayed. 
            userDetailsList = RoswellCrypto.getCurrentUserDetails();  //  [[o365 username, o365 pass, displayname]]
            for (int i = 0; i < userDetailsList.Count; i++)
            {
                UserListContents.Add(userDetailsList[i][2]);
            }

            // Add the displayname of each user found to the list displayed. 
            UsernameBox.Text = UserListContents[0];

            // Remember this detail in case we need to revert!
            originalDetails = UserListContents[0];
        }

        private void RevertButton_Click(object sender, EventArgs e)
        {
            UsernameBox.Text = originalDetails;
        }

        private void SaveAndCloseButton_Click(object sender, EventArgs e)
        {
            // Get password and check login
            
        }
    }
}
