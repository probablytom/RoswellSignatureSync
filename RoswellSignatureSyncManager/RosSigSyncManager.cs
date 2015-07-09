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
            sigDestination = @"C:\Users\tom.wallis\AppData\Roaming\Microsoft\Signatures\newRosTest.htm";
            SigPathBox.Text = sigPath;
            loadUsers();
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
            try
            {
                File.Copy(SigPathBox.Text, sigDestination, true); 
                MessageBox.Show("Signature changed.");
            } catch (Exception ex) {
                MessageBox.Show("Error copying new signature!\nAborted process.");
                MessageBox.Show("Error reads: \n" + ex.Message);
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

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented!");
        }

        private void RemoveUserButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented!");
        }

        

        // UI INTERACTION METHODS END ========================================

    }
}
