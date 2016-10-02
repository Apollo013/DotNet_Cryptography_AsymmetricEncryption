using System;
using System.Security.Cryptography;
using System.Text;

namespace Digital_Signature_Example
{
    public class RSACipher
    {
        public string RSAKey { get; set; }
        public string PublicKey { get; set; }
        public string HashAlgorithm { get; set; } = "SHA1";

        private RSACryptoServiceProvider GetEncryptor()
        {
            RSACryptoServiceProvider crypto = new RSACryptoServiceProvider();
            crypto.FromXmlString(RSAKey);
            return crypto;
        }

        private RSACryptoServiceProvider GetDecryptor()
        {
            RSACryptoServiceProvider crypto = new RSACryptoServiceProvider();
            crypto.FromXmlString(PublicKey);
            return crypto;
        }

        /// <summary>
        /// Generates a hash for the encrypted message
        /// </summary>
        /// <param name="cipherBytes"></param>
        /// <returns></returns>
        private byte[] ComputeHashForMessage(byte[] cipherBytes)
        {
            SHA1Managed alg = new SHA1Managed();
            byte[] hash = alg.ComputeHash(cipherBytes);
            return hash;
        }

        /// <summary>
        /// Signs an RSA provider. This signature will be used by the receiver to verify the public key
        /// </summary>
        /// <param name="hashToSign"></param>
        /// <returns></returns>
        private byte[] CalculateSignatureBytes(byte[] hashToSign)
        {
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(GetEncryptor());
            formatter.SetHashAlgorithm(HashAlgorithm);
            byte[] signature = formatter.CreateSignature(hashToSign);
            return signature;
        }

        /// <summary>
        /// Verifies the message signature
        /// </summary>
        /// <param name="computedHash"></param>
        /// <param name="signatureBytes"></param>
        private void VerifySignature(byte[] computedHash, byte[] signatureBytes)
        {
            RSACryptoServiceProvider senderCipher = GetEncryptor();
            RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(senderCipher);
            deformatter.SetHashAlgorithm(HashAlgorithm);
            if (!deformatter.VerifySignature(computedHash, signatureBytes))
            {
                throw new ApplicationException("Signature did not match from sender");
            }
        }

        /// <summary>
        /// Encrypts a message and signs it
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public DigitalSignatureResult ConstructMessage(string message)
        {
            /*
             * (1) Encrypt the message
             * (2) Compute the hash of the encrypted message
             * (3) Sign it
             */
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] cipherBytes = GetDecryptor().Encrypt(messageBytes, false);
            byte[] cipherHash = ComputeHashForMessage(cipherBytes);
            byte[] signatureHash = CalculateSignatureBytes(cipherHash);

            string cipher = Convert.ToBase64String(cipherBytes);
            string signature = Convert.ToBase64String(signatureHash);
            return new DigitalSignatureResult() { CipherText = cipher, SignatureText = signature };
        }

        /// <summary>
        /// Decrypts a message and verifies it's signature
        /// </summary>
        /// <param name="signatureResult"></param>
        /// <returns></returns>
        public string ExtractMessage(DigitalSignatureResult signatureResult)
        {
            // Get message bytes
            byte[] cipherTextBytes = Convert.FromBase64String(signatureResult.CipherText);
            byte[] signatureBytes = Convert.FromBase64String(signatureResult.SignatureText);

            byte[] recomputedHash = ComputeHashForMessage(cipherTextBytes);
            VerifySignature(recomputedHash, signatureBytes);
            byte[] messageBytes = GetDecryptor().Decrypt(cipherTextBytes, false);

            return Encoding.UTF8.GetString(messageBytes);
        }
    }
}
