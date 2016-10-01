using System;
using System.Security.Cryptography;
using System.Text;

namespace Digital_Signature_Example
{
    public class RSACipher
    {

        public string SenderKeySet { get; set; }
        public string ReceiverKeySet { get; set; }

        private RSACryptoServiceProvider GetSenderCipher()
        {
            RSACryptoServiceProvider crypto = new RSACryptoServiceProvider();
            crypto.FromXmlString(SenderKeySet);
            return crypto;
        }

        private RSACryptoServiceProvider GetReceiverCipher()
        {
            RSACryptoServiceProvider crypto = new RSACryptoServiceProvider();
            crypto.FromXmlString(ReceiverKeySet);
            return crypto;
        }

        /// <summary>
        /// Hashes the encrypted message
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
            RSAPKCS1SignatureFormatter signatureFormatter = new RSAPKCS1SignatureFormatter(GetSenderCipher());
            signatureFormatter.SetHashAlgorithm("SHA1");
            byte[] signature = signatureFormatter.CreateSignature(hashToSign);
            return signature;
        }

        private void VerifySignature(byte[] computedHash, byte[] signatureBytes)
        {
            RSACryptoServiceProvider senderCipher = GetSenderCipher();
            RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(senderCipher);
            deformatter.SetHashAlgorithm("SHA1");
            if (!deformatter.VerifySignature(computedHash, signatureBytes))
            {
                throw new ApplicationException("Signature did not match from sender");
            }
        }


        public DigitalSignatureResult BuildSignedMessage(string message)
        {
            /*
             * (1) Encrypt the message
             * (2) Compute the hash of the encrypted message
             * (3) Sign it
             */
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] cipherBytes = GetReceiverCipher().Encrypt(messageBytes, false);
            byte[] cipherHash = ComputeHashForMessage(cipherBytes);
            byte[] signatureHash = CalculateSignatureBytes(cipherHash);

            string cipher = Convert.ToBase64String(cipherBytes);
            string signature = Convert.ToBase64String(signatureHash);
            return new DigitalSignatureResult() { CipherText = cipher, SignatureText = signature };
        }

        public string ExtractMessage(DigitalSignatureResult signatureResult)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(signatureResult.CipherText);
            byte[] signatureBytes = Convert.FromBase64String(signatureResult.SignatureText);
            byte[] recomputedHash = ComputeHashForMessage(cipherTextBytes);
            VerifySignature(recomputedHash, signatureBytes);
            byte[] plainTextBytes = GetReceiverCipher().Decrypt(cipherTextBytes, false);

            return Encoding.UTF8.GetString(plainTextBytes);
        }
    }
}
