using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalSignatureManager
{
    class UserFunctions
    {
        //Constructor
        public UserFunctions()
        {
            // Nothing to do, this shouldn't be called and is only here for completeness...
        }

        public static bool setCurrentUserDetails(List<List<string>> userDetailsList)
        {

            string toWrite = "";

            // Convert the nested list to an encrypted string
            foreach (List<string> detailSet in userDetailsList)
            {
                if (toWrite != "")
                    toWrite += '\n';
                toWrite += RoswellCrypto.ByteArrToString(RoswellCrypto.Encrypt(detailSet[0] + ' ' + detailSet[1] + ' ' + detailSet[2])); // Add all of the details to the detail set. 
            }


            // Attempt to write. 
            try
            {
                // Check directory to write to exists (and create it if not)
                string filepath = Environment.SpecialFolder.ApplicationData + "\\Roswell Signature Sync\\";
                System.IO.FileInfo file = new System.IO.FileInfo(filepath);
                file.Directory.Create(); // If the directory already exists, this method does nothing.

                // Write to the encrypted username.
                string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name; // Current username
                filepath += RoswellCrypto.ByteArrToString(RoswellCrypto.Encrypt(username)) + ".dts";
                System.IO.File.WriteAllText(filepath, toWrite);

                return true;
            }
            catch (Exception ex)
            {
                // We need error logging here! TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO 
                Console.WriteLine("Had trouble writing to path; could not save data.\nError reads:\n" + ex.Message); // This should be an error log write. 
                MessageBox.Show("Had trouble writing to path; could not save data.blarp.\nError reads:\n" + ex.Message); // Message should be changed once error logging is implemented. 
                return false;
            }
        }


        public static List<string> getCurrentUserDetails()
        {
            List<string> currentDetails = new List<string>();

            // Search directory of files for this program for relevant user details
            string filepath = Environment.SpecialFolder.ApplicationData + "\\Roswell Signature Sync\\";
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name; // Current username
            filepath += RoswellCrypto.ByteArrToString(RoswellCrypto.Encrypt(username)) + ".dts";

            if (!(File.Exists(filepath)))
            {
                return null;
            }


            MessageBox.Show(filepath);
            foreach (string encryptedDetails in File.ReadAllLines(filepath))
            {

                MessageBox.Show(encryptedDetails);
                string[] unSortedDetails = RoswellCrypto.Decrypt(encryptedDetails).Split(' ');
                currentDetails.Add(unSortedDetails[0]);
                currentDetails.Add(unSortedDetails[1]);

                // Extract and concatenate the display name
                string displayname = unSortedDetails[2];
                for (int i = 3; i < unSortedDetails.Length; i++)
                {
                    displayname += ' ' + unSortedDetails[i];
                }
                currentDetails.Add(displayname);
            }

            return currentDetails;

        }



    }
}
