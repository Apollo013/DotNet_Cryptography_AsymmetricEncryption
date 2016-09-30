using System;
using System.Security.Cryptography;
using System.Text;

namespace RSA_Example
{
    class Program
    {
        // The following keys were generated from the command prompt using
        // (A) To Generate: => cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319
        //                  => aspnet_regiis -pc "CONTAINER_NAME_HERE" -exp
        // (B) To Export:   => aspnet_regiis -px "CONTAINER_NAME_HERE" c:\temp\mykeys.xml -pri

        // An xml file containing generated RSA keys is included in this project, see 'mykeys.xml'.
        // We could also have used the method below to programatically generate the keys 'ProgrammaticRsaKeys()'.

        static void Main(string[] args)
        {
            // Vars
            string encryptionKey = "<RSAKeyValue><Modulus>wf53UqRfW77/agVrmzM8u6l1cxm4icbhCLPHnGeAJv2GWo490WIV772qcuV7M0YntTB9n6xbySzUap8yIIslub7JzadV0tadM4WJ2m2HNfA+SOStT+TQmF3nfEEoBUlD1CkmJM7JBJ4N/u0ibExRuonH7NCdzTS2UkgjF2dSzaE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            string decryptionKey = "<RSAKeyValue><Modulus>wf53UqRfW77/agVrmzM8u6l1cxm4icbhCLPHnGeAJv2GWo490WIV772qcuV7M0YntTB9n6xbySzUap8yIIslub7JzadV0tadM4WJ2m2HNfA+SOStT+TQmF3nfEEoBUlD1CkmJM7JBJ4N/u0ibExRuonH7NCdzTS2UkgjF2dSzaE=</Modulus><Exponent>AQAB</Exponent><P>9PUpmADapF3LHVZ1ZDCNwCbBJKZpy8BI7sfKID96xxpPRhmonZuOVWWbsxJGYJGOT8ZGv7vkM701bsXv9NluDw==</P><Q>yr0tdwiS0swHfqC0z3nUMCawSvS8IrPhVY6FFhMyCydQtoFb80tIS1i2z8vyauiCMUX97zbvxyzMH3UQPj+5Tw==</Q><DP>IwS9Wn6cwyypcds/UwBh81tW9z9XFoq5onErYyrQCZCoTpQyd72aPnkVJidxqjKEzsDAsn5Q5FijP9/KKw2+Xw==</DP><DQ>Cdfxkyv5ZP6/BmjrHn+9y7C1Mo57a/vr3umSkKXR8jSweIwDWOa41d+y0JgIZyRu3dGWKL00GymTp5tZdIxHhQ==</DQ><InverseQ>ct0IXawMmdDuvmg13qrRZSN6/tGhdroqhMFcNl7hqOGh4ucd4w7NL9x50VUmpKeFT+JI3nXlul8uEpI2IYfBag==</InverseQ><D>FT6RwKohi0GSZQDs9NUanI6FPzKc4/G0qShO/tDS1vJCqV4UZP6x2kxSAz0gTdAr/wqtZzQP5SsUpAyFB81VNx4naqRpEVzvnLhXiRb9GPZZYK8AazoAi0offOhHzuVRh3ctUT2VrGWmOQJ2L5G3jHZiXpo5SAvNVTXWtdvRSGU=</D></RSAKeyValue>";
            string plainText = "I'm a lumberjack and I don't care, I sleep all day and I drink all night";
            var encryptor = CreateCypher(encryptionKey);
            var decryptor = CreateCypher(decryptionKey);

            // Encrypt / Decrypt
            string encryptedText = Encrypt(encryptor, plainText);
            string decryptedText = Decrypt(decryptor, encryptedText);


            // Print Details
            Print("ORIGINAL TEXT", plainText);
            Print("ENCRYPTED TEXT", encryptedText);
            Print("DECRYPTED TEXT", decryptedText);

            Console.ReadKey();
        }

        #region Encrypt/Decrypt Methods
        private static string Encrypt(RSACryptoServiceProvider encryptor, string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedText = encryptor.Encrypt(plainBytes, true);
            return Convert.ToBase64String(encryptedText);
        }

        private static string Decrypt(RSACryptoServiceProvider decryptor, string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] originalBytes = decryptor.Decrypt(encryptedBytes, true);
            return Encoding.UTF8.GetString(originalBytes);
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// Creates a cypher for encrypting / decrypting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider CreateCypher(string key)
        {
            var cypher = new RSACryptoServiceProvider();
            cypher.FromXmlString(key);
            return cypher;
        }

        /// <summary>
        /// Prints arbitrary title to console
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        private static void Print(string title, string message)
        {
            string divider = new string('-', 160);
            System.Console.WriteLine(divider);
            System.Console.WriteLine(title);
            System.Console.WriteLine(divider);
            System.Console.WriteLine(message);
            System.Console.WriteLine(divider);
            System.Console.WriteLine();
        }

        /// <summary>
        /// Generates a new instance of an RSA algorithm and exports its auto-generated public key to an RSAParameters object.
        /// </summary>
        private static string ProgrammaticRsaKeys()
        {
            RSACryptoServiceProvider crypto = new RSACryptoServiceProvider();
            RSAParameters publicKey = crypto.ExportParameters(false); // false: do not export private key            
            return crypto.ToXmlString(true);
        }

        private static void PopulateCsp()
        {
            // N.B. Run the above commands at the beginning of this class
            //      Observe the contents of the output file
            //      Then run this method and check to see if both Modulus keys are the same !

            CspParameters parms = new CspParameters();
            parms.KeyContainerName = "TestKeys";
            parms.Flags = CspProviderFlags.UseMachineKeyStore;

            RSACryptoServiceProvider crypto = new RSACryptoServiceProvider(parms);
            RSAParameters publicKey = crypto.ExportParameters(true);
            System.Console.WriteLine(Convert.ToBase64String(publicKey.Modulus));
        }
        #endregion
    }
}
