using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace RoswellSignatureSyncManager
{
    public partial class SignatureManagerHome : Form
    {

        List<List<string>> o365Details;
        string sigPath;
        string sigDestination;

        // Constructor
        public SignatureManagerHome()
        {
            InitializeComponent();
            o365Details = new List<List<string>>();
            sigPath = ""; // Should be updated from remembered details in app.config
            sigDestination = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\Microsoft\Signatures\newRosTest.htm";
            SigPathBox.Text = sigPath;

            // Check to see whether this is the first run of the program. 
            if (ConfigurationManager.AppSettings["sigPath"] == "")
            {
                // It's the first run! 
                MessageBox.Show("Looks like this is your first run of Roswell Signature Sync Manager, as no signature location is currently configured.\n" + 
                 "Click 'Browse' to find a signature for your initial setup.");
            }

        }

        // USER MANAGEMENT FUNCTIONS BEGIN ====================================

        private void loadUsers()
        {
            string filepath = ConfigurationManager.AppSettings["userDataFile"]; // Do we want this here, or the details kept in AppSettings?
            RoswellCrypto encryptionMgr = new RoswellCrypto();
            MessageBox.Show(filepath);
            this.o365Details = encryptionMgr.loadFile(filepath);
        }


        // USER MANAGEMENT FUNCTIONS END ======================================
        

        
        // UI INTERACTION METHODS BEGIN ======================================


        // Move the file from its old location to the new one specified by the filePicker. 
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(SigPathBox.Text))
            {
                try
                {
                    File.Copy(SigPathBox.Text, sigDestination, true);
                    ConfigurationManager.AppSettings["sigPath"] = SigPathBox.Text; // update App.config
                    MessageBox.Show("Signature changed.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error copying new signature!\nAborted process.");
                    MessageBox.Show("Error reads: \n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("The specified file doesn't exist.");
            }
        }


        // Simply close the program. (In future, save settings? Is this done as and when settings are changed?)
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Launches a filepicker and inserts the new file path, if it's found, into SigPathBox.
        // TODO: CHECK THE FILE IS OPENABLE FOR READING BY THE CURRENT USER FIXFIXFIXFIXFIXFIXFIXFIXFIXFIXFIXFIXFIXFIXFIXFIXFIXFIX
        private void SigPathBrowse_Click(object sender, EventArgs e)
        {
            string filepath = SigPathBox.Text; // So that if we don't get a DialogResult.OK, we keep the original path.

            // Create & configure a file picker.
            OpenFileDialog filePicker = new OpenFileDialog();
            filePicker.InitialDirectory = "C:\\";
            filePicker.Filter = "HTML Documents (*.html;*.htm)|*.html;*.htm|Text Files (*.txt)|*.txt";
            filePicker.RestoreDirectory = true;

            if (filePicker.ShowDialog() == DialogResult.OK)
            { // Insert a try here so we know we can open the file for reading first!
                filepath = filePicker.FileName;
            }

            SigPathBox.Text = filepath;

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {

        }

        private void UpdateButton_Hover(object sender, EventArgs e)
        {

        }
        

        // UI INTERACTION METHODS END ========================================

    }
}
