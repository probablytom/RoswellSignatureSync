using System;
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
            List<List<string>> userDetails = new List<List<string>>();  //  [[o365 username, o365 pass, displayname]]

            
            
            // TODO: GET USER DETAILS AND PUT IN userDetails TODO TODO TODO TODO



            // Add the displayname of each user to the list to be displayed. 
            List<List<string>> details = RoswellCrypto.getCurrentUserDetails();
            

            // Add the displayname of each user found to the list displayed. 
            UserList.DataSource = UserListContents;
        }
    }
}
