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
        bool firstRun;

        // Constructor
        public SignatureManagerHome()
        {
            InitializeComponent();
            o365Details = new List<List<string>>();
            sigPath = ""; // Should be updated from remembered details in app.config
            sigDestination = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\Microsoft\Signatures\newRosTest.htm";
            SigPathBox.Text = sigPath;

            // Check to see whether this is the first run of the program. 
            if (Properties.Settings.Default.signaturePath == "")
            {
                // It's the first run! 
                MessageBox.Show("Looks like this is your first run of Roswell Signature Sync Manager, as no signature location is currently configured.\n" +
                 "Click 'Browse' to find a signature for your initial setup.");
                firstRun = true;
            }
            else
            {
                firstRun = false;
                SigPathBox.Text = Properties.Settings.Default.signaturePath;
                sigDestinationBox.Text = Properties.Settings.Default.signatureDestination;
            }

        }

        // USER MANAGEMENT FUNCTIONS BEGIN ====================================

        private void loadUsers()
        {
            string filepath = Properties.Settings.Default.userDataFile; // Do we want this here, or the details kept in AppSettings?
            RoswellCrypto encryptionMgr = new RoswellCrypto();
            MessageBox.Show(filepath);
            this.o365Details = encryptionMgr.loadFile(filepath);
        }


        // USER MANAGEMENT FUNCTIONS END ======================================
        

        
        // UI INTERACTION METHODS BEGIN ======================================


        // Move the file from its old location to the new one specified by the filePicker. 
        private void SaveButton_Click(object sender, EventArgs e)
        {
            updateSignature();
            updateDestination();
            Properties.Settings.Default.Save();
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

            if (firstRun) Properties.Settings.Default["signaturePath"] = filepath;


            Properties.Settings.Default.Save();

        }


        // Selects a folder via a FolderBrowserDialog.
        private void SigDestinationBrowse_Click(object sender, EventArgs e)
        {
            string dirpath = sigDestinationBox.Text;

            // Create & configure a directory picker. 
            FolderBrowserDialog dirPicker = new FolderBrowserDialog();
            dirPicker.ShowNewFolderButton = true;
            dirPicker.Description = "Please select a place to put the signature to be synced.";
            if (dirPicker.ShowDialog() == DialogResult.OK)
            {
                dirpath = dirPicker.SelectedPath;
            }

            sigDestinationBox.Text = dirpath;

            if (firstRun) Properties.Settings.Default["signatureDestination"] = dirpath;
            MessageBox.Show(Properties.Settings.Default.signatureDestination);


            Properties.Settings.Default.Save();

        }

        private void PathUpdateButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(SigPathBox.Text))
            {
                Properties.Settings.Default["signaturePath"] = SigPathBox.Text;
                MessageBox.Show("Update successful!\n\nDestination changed to:\n" + sigDestinationBox.Text);
            }
            else
            {
                MessageBox.Show("Could not update signature.\nFile does not exist.");
            }
        }

        private void sigDestinationUpdate_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(sigDestinationBox.Text))
            {
                Properties.Settings.Default["signatureDestination"] = sigDestinationBox.Text;
                MessageBox.Show("Update successful!\n\nDestination changed to:\n" + sigDestinationBox.Text);
            }
            else
            {
                MessageBox.Show("Could not update hosting destination.\nDirectory does not exist.");
            }
        }

        

        // UI INTERACTION METHODS END =========================================

        // HELPER FUNCTIONS BEGIN =============================================

        // Update signature.
        public void updateSignature()
        {
            if (File.Exists(SigPathBox.Text))
            {
                try
                {
                    File.Copy(SigPathBox.Text, Properties.Settings.Default.signatureDestination + '\\' + filenameOf(SigPathBox.Text), true);
                    Properties.Settings.Default["signaturePath"] = SigPathBox.Text; // update App.config
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
                MessageBox.Show("The specified file doesn't exist.\n" +
                    "Please select another file.");
            }
        }


        // Update hosting destination
        public void updateDestination()
        {
            if (Directory.Exists(sigDestinationBox.Text))
            {
                try
                {
                    string path = Properties.Settings.Default.signaturePath;
                    string destination = Properties.Settings.Default.signatureDestination;
                    File.Copy(path, sigDestinationBox.Text + filenameOf(path), true);
                    Properties.Settings.Default["signatureDestination"] = sigDestinationBox.Text;
                    MessageBox.Show("Signature destination changed.");
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.signatureDestination == "")
                        MessageBox.Show("Error updating destination! It appears to be blank. ");
                    MessageBox.Show("Error changing the destination! Please contact Roswell I.T. for help.");
                    
                }
            }
            else
            {
                MessageBox.Show("The specified directory doesn't exist.\n" + 
                    "Please select another directory.");
            }
            Properties.Settings.Default.Save();
        }

        public void transferSig()
        {
            if (File.Exists(Properties.Settings.Default.signaturePath))
            {
                if (Directory.Exists(Properties.Settings.Default.signatureDestination))
                {
                    // Everything exists! Let's transfer.
                    // try-catch incase there's a problem with permissions &c.
                    File.Copy(Properties.Settings.Default.signaturePath, Properties.Settings.Default.signaturePath + Properties.Settings.Default.hostedSigName);
                }
                else
                {
                    MessageBox.Show("The directory at the saved destination path doesn't exist!\n\nAborting.");
                }
            }
            else
            {
                MessageBox.Show("The file at the saved signature path doesn't exist!\n\nAborting.");
            }

            Properties.Settings.Default.Save();

        }


        private string filenameOf(string path)
        {
            string[] splitPath = path.Split('\\');
            return splitPath[splitPath.Length - 1];
        }



        // HELPER FUNCTIONS END ===============================================

    }
}
