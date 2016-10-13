# DotNet_Cryptography_AsymmetricEncryption
A couple of console apps demonstrating asymmetric encryption & digital signing.

---

Developed with Visual Studio 2015 Community

---

###Techs
|Tech|
|----|
|C#|

---

### Algorithms
|Algorithm|
|---------|
| RSA |

---

###Features
|Feature|
|-------|
|Generating a container for RSA key pairs using command line |
|Exporting the container to an xml file |
|Use of 'CspParameters' object to populate the appropriate properties of an 'RSACryptoServiceProvider' object with the RSA Keys generated |
|Ecrypting / decrypting using 'RSACryptoServiceProvider'|
|Generating a signature using 'RSAPKCS1SignatureFormatter' |
|Verifying a signature using 'RSAPKCS1SignatureDeformatter' | 
|Computing a hash using SHA1 |

---

###Resources
|Title|Author|Website|
|-----|------|-------|
|[RSA Class](https://msdn.microsoft.com/en-us/library/system.security.cryptography.rsa(v=vs.110).aspx)| | MSDN |
|[RSAKeyValue Class](https://msdn.microsoft.com/en-us/library/system.security.cryptography.xml.rsakeyvalue(v=vs.110).aspx)| | MSDN |
|[KeyInfo Class](https://msdn.microsoft.com/en-us/library/system.security.cryptography.xml.keyinfo(v=vs.110).aspx) | | MSDN |
|[Cryptographic Signatures](https://msdn.microsoft.com/en-us/library/hk8wx38z(v=vs.110).aspx)| | MSDN |
|[How to Sign and Verify the signature with .NET and a certificate (C#)](https://blogs.msdn.microsoft.com/alejacma/2008/06/25/how-to-sign-and-verify-the-signature-with-net-and-a-certificate-c/)| Alejandro Campos Magencio | MSDN |
|[Introduction to digital signatures in .NET cryptography](https://dotnetcodr.com/2013/11/14/introduction-to-digital-signatures-in-net-cryptography/)| Andras Nemes | dotnetcodr |
