using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.AccountManagement;
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
        ExchangeService exchangeService;
        UserConfiguration userConfig;


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

            
            
            // TODO: GET USER DETAILS AND PUT IN BOXES TODO TODO TODO TODO TODO

            

            // Add the displayname of each user to the list to be displayed.
            RoswellCrypto.PasswordHash = PasswordBox1.Text;
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
            
            // First, let's make sure the passwords match. 
            if (passwordsMatch())
            {

                // We have valid details, now let's verify that they're correct!
                if (verifyConnection())
                {
                    
                    // Now, we've set up the connection and verified & validated the details.
                    // Save those details for later and exit. 

                    // Save details
                    List<string> details = new List<string>();
                    details.Add(UsernameBox.Text);
                    details.Add(PasswordBox1.Text);
                    details.Add(UserPrincipal.Current.DisplayName);
                    List<List<string>> toSet = new List<List<string>>();
                    toSet.Add(details);

                    // Exit
                    RoswellCrypto.PasswordHash = PasswordBox1.Text;
                    RoswellCrypto.setCurrentUserDetails(toSet);

                    Close();

                }

            }
            else
            {
                // Passwords didn't match!
                MessageBox.Show("ERROR. Passwords don't match.");
                EventLog.WriteEntry("Couldn't save and exit: Passwords did not match.");
            }

        }

        // HELPER FUNCTIONS BEGIN =============================================

        public bool passwordsMatch()
        {
            return PasswordBox1.Text == PasswordBox2.Text;
        }

        // Check whether we can access o345 using the username and password in the boxes provided. 
        private bool verifyConnection()
        {

            if (passwordsMatch())
            {

                if (setupExchangeConnection())
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("ERROR: Couldn't set up connection  to office365 with the details provided.\nYour password for " + UsernameBox.Text + " may be incorrect, or you may not be connected to the internet.");
                }

            }
            else  // Passwords didn't match!
            {
                MessageBox.Show("ERROR: Passwords don't match.");
            }

            return false;
            
        }

        protected bool setupExchangeConnection()
        {
            // Trash the old connection, if there is one. 
            exchangeService = new ExchangeService();
            userConfig = null;

            string username = UsernameBox.Text;
            string password = PasswordBox1.Text;
            try
            {
                // Connect to office365 Exchange
                exchangeService = new ExchangeService(ExchangeVersion.Exchange2010_SP2); // For office365, this appears to be right -- see working powershell script.
                exchangeService.Credentials = new WebCredentials(username, password);
                exchangeService.UseDefaultCredentials = false; // CAN WE USE THIS?! UNSAFE WITH OFFICE365?! https://msdn.microsoft.com/en-us/library/office/dn567668.aspx#Create
                exchangeService.AutodiscoverUrl(username, RedirectionUrlValidationCallback);

                // Get a user config object
                userConfig = UserConfiguration.Bind(exchangeService,
                                                                      "OWA.UserOptions",
                                                                      WellKnownFolderName.Root,
                                                                      UserConfigurationProperties.All);

                // We SHOULD now be set up! In theory.
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't make a connection to office365.\n\nAre you connected to the internet?\nIf so, the details entered may be incorrect.");
                EventLog.WriteEntry("Couldn't make a connection to office365.\nEither the internet connection is down, or the password entered for user " + username + "is incorrect.\n\nException raised reads:" + ex.Message);
                return false;
            }
        }



        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }



        // HELPER FUNCTIONS END ===============================================

    }
}
