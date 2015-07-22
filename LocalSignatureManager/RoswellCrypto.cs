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



        public static string Decrypt(string toDecrypt)
        {
            return Decrypt(StrToByteArray(toDecrypt));
        }
                
        
         

        /*      // From http://stackoverflow.com/questions/165808/simple-two-way-encryption-for-c-sharp */
        // Change these keys
        private static byte[] Key = { 123, 217, 19, 31, 244, 236, 35, 40, 14, 14, 217, 16, 7, 12, 22, 210, 24, 240, 176, 44, 133, 253, 96, 219, 124, 126, 171, 18, 11, 216, 5, 2 };
        private static byte[] Vector = { 19, 168, 194, 172, 213, 43, 213, 119, 23, 11, 252, 142, 79, 22, 164, 153 };


        private static ICryptoTransform EncryptorTransform, DecryptorTransform;
        private static System.Text.UTF8Encoding UTFEncoder;

        public RoswellCrypto()
        {
            //This is our encryption method
            RijndaelManaged rm = new RijndaelManaged();

            //Create an encryptor and a decryptor using our encryption method, key, and vector.
            EncryptorTransform = rm.CreateEncryptor(Key, Vector);
            DecryptorTransform = rm.CreateDecryptor(Key, Vector);

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

        /// ----------- The commonly used methods ------------------------------    
        /// Encrypt some text and return a string suitable for passing in a URL.
        public static string EncryptToString(string TextValue)
        {
            return ByteArrToString(Encrypt(TextValue));
        }

        /// Encrypt some text and return an encrypted byte array.
        public static byte[] Encrypt(string TextValue)
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
        public static string DecryptString(string EncryptedString)
        {
            return Decrypt(StrToByteArray(EncryptedString));
        }

        /// Decryption when working with byte arrays.    
        public static string Decrypt(byte[] EncryptedValue)
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
        public static byte[] StrToByteArray(string str)
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
        public static string ByteArrToString(byte[] byteArr)
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
        private static string[] decryptLine(string line)
        {
            return Decrypt(line).Split(' ');
        }


        // Encryption & Decryption functions END ==============================



        // File loading/saving functions BEGIN ================================


        // Loads a file of encrypted strings and returns a nested list of structure:
        // [ [ o365user, o365pass, displayname ] ]
        public static List<List<string>> loadFile(string filePath)
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
        public static void EncryptAndWriteFile(List<List<string>> detailsList, string filepath)
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


        // File loading/saving functions END ==================================

    }
}