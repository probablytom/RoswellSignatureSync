using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalSignatureManager
{
    class RoswellCrypto
    {

        // Encryption variables
        private static readonly string PasswordHash = ""; // TODO: Is this the hashed password, or the plaintext?
        private static readonly string SaltKey = "RoswellSalt";
        private static readonly string VIKey = "123RoswellKey";


        // Constructors
        public RoswellCrypto()
        {
            // Use hard-coded values. (For now?)
        }



        // Taken from https://social.msdn.microsoft.com/Forums/vstudio/en-US/d6a2836a-d587-4068-8630-94f4fb2a2aeb/encrypt-and-decrypt-a-string-in-c?forum=csharpgeneral
        // TODO: Comment this!
        // Encryption & Decryption functions BEGIN ============================

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }


        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new System.IO.MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        // ToDo: decrypt the line structure for keeping usernames/passwords.
        private string[] decryptLine(string line)
        {
            return Decrypt(line).Split(' ');
        }


        // Encryption & Decryption functions END ==============================



        // File loading/saving functions BEGIN ================================


        // Loads a file of encrypted strings and returns a nested list of structure:
        // [ [ o365user, o365pass, displayname ] ]
        public List<List<string>> loadFile(string filePath)
        {
            string[] fileContents = { };
            List<List<string>> details = new List<List<string>>();

            if (File.Exists(filePath))
            {
                fileContents = File.ReadAllLines(filePath);
            }
            else
            {
                File.Create(filePath); // Create it for later. 
            }
            string[] currentLine;
            List<string> currentVal;
            string currentName;

            foreach (string line in fileContents)
            {
                //The last line will be blank. Don't include this!
                if (line != "")
                {
                    currentLine = decryptLine(line);
                    currentVal = new List<string>() { currentLine[0], currentLine[1] };

                    //concatenate and add the display name of the user.
                    currentName = "";
                    for (int i = 2; i < currentLine.Length; i++)
                    {
                        currentName += currentLine[i];
                    }
                    currentVal.Add(currentName);
                    details.Add(currentVal);
                }
            }

            return details;

        }

        // writes a file of encrypted strings from a nested list of structure:
        // [ [ o365user, o365pass, displayname ] ]
        public void EncryptAndWriteFile(List<List<string>> detailsList, string filepath)
        {
            string toWrite = "";
            string toEncrypt;

            foreach (List<string> userDetails in detailsList)
            {
                toEncrypt = "";
                foreach (string detail in userDetails)
                {
                    toEncrypt += detail + ' ';
                }
                toWrite += Encrypt(toEncrypt) + "\n";
            }

            File.WriteAllText(filepath, toWrite);

        }


        public static List<List<string>> getCurrentUserDetails()
        {
            List<List<string>> details = new List<List<string>>();
            List<string> currentDetails = new List<string>();

            // Search directory of files for this program for relevant user details
            string filepath = Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Roswell Signature Sync\\";
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name; // Current username
            filepath += Encrypt(username) + ".dts";

            foreach (string encryptedDetails in File.ReadAllLines(filepath))
            {


                string[] unSortedDetails = Decrypt(encryptedDetails).Split(' ');
                currentDetails.Add(unSortedDetails[0]);
                currentDetails.Add(unSortedDetails[1]);

                // Extract and concatenate the display name
                string displayname = unSortedDetails[2];
                for (int i = 3; i < unSortedDetails.Length; i++)
                {
                    displayname += ' ' + unSortedDetails[i];
                }
                currentDetails.Add(displayname);
                details.Add(currentDetails);
            }

            return details;
        }

        public static bool setCurrentUserDetails(List<List<string>> userDetailsList)
        {

            string toWrite = "";

            // Convert the nested list to an encrypted string
            foreach (List<string> detailSet in userDetailsList)
            {
                if (toWrite != "")
                    toWrite += '\n';
                toWrite += Encrypt(detailSet[0] + ' ' + detailSet[1] + ' ' + detailSet[2]); // Add all of the details to the detail set. 
            }

            
            // Attempt to write. 
            try
            {
                string filepath = Environment.GetEnvironmentVariable("PROGRAMFILES") + "\\Roswell Signature Sync\\";
                string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name; // Current username
                filepath += Encrypt(username) + ".dts";
                File.WriteAllText(filepath, toWrite);
                return true;
            }
            catch (Exception ex)
            {
                // We need error logging here! TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO 
                Console.WriteLine("Had trouble writing to path; could not save data.\nError reads:\n" + ex.Message); // This should be an error log write. 
                MessageBox.Show("Had trouble writing to path; could not save data.\nError reads:\n" + ex.Message); // Message should be changed once error logging is implemented. 
                return false;
            }
        }


        // File loading/saving functions END ==================================

    }
}