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
        public static string PasswordHash = "pastsa"; // TODO: Is this the hashed password, or the plaintext?
        


        public string Decrypt(string toDecrypt)
        {
            return Decrypt(StrToByteArray(toDecrypt));
        }
        /*
        
        // Taken from https://social.msdn.microsoft.com/Forums/vstudio/en-US/d6a2836a-d587-4068-8630-94f4fb2a2aeb/encrypt-and-decrypt-a-string-in-c?forum=csharpgeneral
        // TODO: Comment this!
        // Encryption & Decryption functions BEGIN ============================

        public static string Encrypt(string plainText)
        {

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new System.Security.Cryptography.Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            MessageBox.Show(symmetricKey.BlockSize.ToString());
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
        */

        /*      // From http://stackoverflow.com/questions/165808/simple-two-way-encryption-for-c-sharp */
        // Change these keys
        private byte[] Key = { 123, 217, 19, 31, 244, 236, 35, 40, 14, 14, 217, 16, 7, 12, 22, 210, 24, 240, 176, 44, 133, 253, 96, 219, 124, 126, 171, 18, 11, 216, 5, 2 };
        private byte[] Vector = { 19, 168, 194, 172, 213, 43, 213, 119, 23, 11, 252, 142, 79, 22, 164, 153 };


        private ICryptoTransform EncryptorTransform, DecryptorTransform;
        private System.Text.UTF8Encoding UTFEncoder;

        public RoswellCrypto()
        {
            //This is our encryption method
            RijndaelManaged rm = new RijndaelManaged();

            //Create an encryptor and a decryptor using our encryption method, key, and vector.
            EncryptorTransform = rm.CreateEncryptor(this.Key, this.Vector);
            DecryptorTransform = rm.CreateDecryptor(this.Key, this.Vector);

            //Used to translate bytes to text and vice versa
            UTFEncoder = new System.Text.UTF8Encoding();
        }

        /// -------------- Two Utility Methods (not used but may be useful) -----------
        /// Generates an encryption key.
        static public byte[] GenerateEncryptionKey()
        {
            //Generate a Key.
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateKey();
            return rm.Key;
        }

        /// Generates a unique encryption vector
        static public byte[] GenerateEncryptionVector()
        {
            //Generate a Vector
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateIV();
            return rm.IV;
        }
        /*
        public string Encrypt(string toEncrypt)
        {
            return EncryptToString(toEncrypt);
        }

        public string Decrypt(string toDecrypt)
        {
            return DecryptString(toDecrypt);
        }*/


        /// ----------- The commonly used methods ------------------------------    
        /// Encrypt some text and return a string suitable for passing in a URL.
        public string EncryptToString(string TextValue)
        {
            return ByteArrToString(Encrypt(TextValue));
        }

        /// Encrypt some text and return an encrypted byte array.
        public byte[] Encrypt(string TextValue)
        {
            //Translates our text value into a byte array.
            Byte[] bytes = UTFEncoder.GetBytes(TextValue);

            //Used to stream the data in and out of the CryptoStream.
            MemoryStream memoryStream = new MemoryStream();

            /*
             * We will have to write the unencrypted bytes to the stream,
             * then read the encrypted result back from the stream.
             */
            #region Write the decrypted value to the encryption stream
            CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            #endregion

            #region Read encrypted value back out of the stream
            memoryStream.Position = 0;
            byte[] encrypted = new byte[memoryStream.Length];
            memoryStream.Read(encrypted, 0, encrypted.Length);
            #endregion

            //Clean up.
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        /// The other side: Decryption methods
        public string DecryptString(string EncryptedString)
        {
            return Decrypt(StrToByteArray(EncryptedString));
        }

        /// Decryption when working with byte arrays.    
        public string Decrypt(byte[] EncryptedValue)
        {
            #region Write the encrypted value to the decryption stream
            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write);
            decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
            decryptStream.FlushFinalBlock();
            #endregion

            #region Read the decrypted value from the stream.
            encryptedStream.Position = 0;
            Byte[] decryptedBytes = new Byte[encryptedStream.Length];
            encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            encryptedStream.Close();
            #endregion
            return UTFEncoder.GetString(decryptedBytes);
        }

        /// Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
        //      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        //      return encoding.GetBytes(str);
        // However, this results in character values that cannot be passed in a URL.  So, instead, I just
        // lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
        public byte[] StrToByteArray(string str)
        {
            if (str.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            byte val;
            byte[] byteArr = new byte[str.Length / 3];
            int i = 0;
            int j = 0;
            do
            {
                val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        // Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
        //      System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        //      return enc.GetString(byteArr);    
        public string ByteArrToString(byte[] byteArr)
        {
            byte val;
            string tempStr = "";
            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                val = byteArr[i];
                if (val < (byte)10)
                    tempStr += "00" + val.ToString();
                else if (val < (byte)100)
                    tempStr += "0" + val.ToString();
                else
                    tempStr += val.ToString();
            }
            return tempStr;
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


        public List<string> getCurrentUserDetails()
        {
            List<string> currentDetails = new List<string>();

            // Search directory of files for this program for relevant user details
            string filepath = Environment.SpecialFolder.ApplicationData + "\\Roswell Signature Sync\\";
            string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name; // Current username
            filepath += ByteArrToString(Encrypt(username)) + ".dts";

            if (!(File.Exists(filepath)))
            {
                return null;
            }


            MessageBox.Show(filepath);
            foreach (string encryptedDetails in File.ReadAllLines(filepath))
            {

                MessageBox.Show(encryptedDetails);
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
                return currentDetails;
            }
        
        }

        public bool setCurrentUserDetails(List<List<string>> userDetailsList)
        {

            string toWrite = "";

            // Convert the nested list to an encrypted string
            foreach (List<string> detailSet in userDetailsList)
            {
                if (toWrite != "")
                    toWrite += '\n';
                toWrite += ByteArrToString(Encrypt(detailSet[0] + ' ' + detailSet[1] + ' ' + detailSet[2])); // Add all of the details to the detail set. 
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
                filepath += ByteArrToString(Encrypt(username)) + ".dts";
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


        // File loading/saving functions END ==================================

    }
}