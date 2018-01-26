using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LiveHAPI.Shared.Custom
{
    public static class Utils
    {
        public static string HasToEndWith(this string value, string end)
        {
            return value.EndsWith(end) ? value : $"{value}{end}";
        }

        public static string Decrypt(string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                return string.Empty;

            //            Decryptor decry = new Decryptor(EncryptionAlgorithm.TripleDes)
            //            {
            //                IV = Encoding.ASCII.GetBytes("t3ilc0m3")
            //            };
            //
            //            return decry.Decrypt(parameter, "3wmotherwdrtybnio12ewq23");

            string retVal = "";
            // Create or open the specified file. 
            using (FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate))
            {

                // Create a new TripleDES object.
                using (TripleDES tripleDESalg = TripleDES.Create())
                {

                    // Create a CryptoStream using the FileStream 
                    // and the passed key and initialization vector (IV).
                    using (CryptoStream cStream = new CryptoStream(fStream,
                        tripleDESalg.CreateDecryptor(Key, IV),
                        CryptoStreamMode.Read))
                    {

                        // Create a StreamReader using the CryptoStream.
                        using (StreamReader sReader = new StreamReader(cStream))
                        {

                            // Read the data from the stream 
                            // to decrypt it.
                            retVal = sReader.ReadLine();
                        }
                    }
                }

            
            // Return the string. 
            return retVal;
        }

    }
}