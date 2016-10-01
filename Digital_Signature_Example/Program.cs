using System;

namespace Digital_Signature_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var sender = new RSACipher()
            {
                SenderKeySet = @"<RSAKeyValue><Modulus>9M6YCj/zHjbZibpZuk0As88tu7gcCD6krBkVrVHYapHcX27N3/zwCz9N/r7XH8/j8Shh6Ed5aAls5/9JU6f8cfYiY4/92J8sazQHMia60Lcf29lpICoHa3JPVR/NhJE+fxV+sNerJiY6ke38RZwPbYg8617YiAP9+7plAvOfpsk=</Modulus><Exponent>AQAB</Exponent><P>/y1x4BRX96YZ9LmKvedqG+eXZS5TuwzEpIkn6gHYiTtJqtH8glMK6KRN7SEMyLwaJ6A5Y6So/odAFzCzhz1OLQ==</P><Q>9ZiXj9eSX48NYY73P7kPN0FIOpLf0IaK3VLfS2FU80xx80uzsgBtNdX/UVM2HXtizYbf6e4oygNVL53Lc6D4jQ==</Q><DP>9f4PSx8RdlGPsBW1pECUT/ZAQnAYk7dJUfxOmA3TeufGqn/n1pLLb14FKVW1B0YeSJjy+hXgPA6SQEjYqMT2uQ==</DP><DQ>RYXEobtsfBCKwSsvYqKIZCPexnX9VZJAjRaAj14mJhllyHGNlL36LFs/w03C6+WZuoSLrjT05vq6ipgol7rhoQ==</DQ><InverseQ>BFZgYzS9g72iNbZb9CJa0RPAxu3DVnInj9Z85GX1BX9VEwtFsnqPdrP5094T96UJ9qx4GVCw3lffWGc560gkhQ==</InverseQ><D>vnbiCc95ar+H/sUSvITekAcX8N4sSSnb2t3lZKSx+TAkwccmvCdB565IE3QNUX3gPaeKjTrWtV/n8JRJS6H+IHKelKww5y/srEug6dQA348yAUkmYk1hzr8EJtHEDJ5cF7YQrXtoY0k6iPX8nwc1y8HUD01K7J5SiZpSt7jZUtE=</D></RSAKeyValue>",
                ReceiverKeySet = @"<RSAKeyValue><Modulus>qWW2CtvSG0DqjEkb3BJq1xGgMsz/ReU8Ox34/1hE8A8pQeW0wsX2r8gkOBj5SAtEV2CN7fZMam/W06B0hcgxy0HAxXYsjxtWE0lXGw6f6P5VFyfZmNjj3X5URr6Kmxt0M2c/bs6pU3PwYcfxKAjzaZ7BiSgKGlosjDZrVUP/9kk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
            };

            var receiver = new RSACipher()
            {
                SenderKeySet = @"<RSAKeyValue><Modulus>9M6YCj/zHjbZibpZuk0As88tu7gcCD6krBkVrVHYapHcX27N3/zwCz9N/r7XH8/j8Shh6Ed5aAls5/9JU6f8cfYiY4/92J8sazQHMia60Lcf29lpICoHa3JPVR/NhJE+fxV+sNerJiY6ke38RZwPbYg8617YiAP9+7plAvOfpsk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>",
                ReceiverKeySet = @"<RSAKeyValue><Modulus>qWW2CtvSG0DqjEkb3BJq1xGgMsz/ReU8Ox34/1hE8A8pQeW0wsX2r8gkOBj5SAtEV2CN7fZMam/W06B0hcgxy0HAxXYsjxtWE0lXGw6f6P5VFyfZmNjj3X5URr6Kmxt0M2c/bs6pU3PwYcfxKAjzaZ7BiSgKGlosjDZrVUP/9kk=</Modulus><Exponent>AQAB</Exponent><P>4ugiHEYxb7v2AoHQuKhTfUzz4c0ZJOOMDirKglW74SpZs8dzzdeQaRBT8yNhr7JqH4ed8wyNWlkQM7EtteLZtQ==</P><Q>vx3rAOl3olpueHBpcFdOg/f2zutoRIsp3koFKOHUSL9UxXKxzHSR2vsGox+TpTQp0q3B/24Ws0TStVwdVOF2xQ==</Q><DP>JoD/sM6UewJpR5mhwoQFzuBtDicQmjrmvEy0mpNT02ytVh2FYCuxPLLhnAOoAvmCmqEXw3Og1PECF9N8Dz5UdQ==</DP><DQ>eVGClmed29/P9IBXTkr9umQzthJVc/1rLIKMV/FTEoLySwyNtR3iYMGs6uvmi55bVjOazIyTuGTd5OZ+cB1XiQ==</DQ><InverseQ>BRQ334pJQSE8+PN8DLTh7wqDJYYNMBbn+hTBZATTFcIk9lBEaIB2Hu/fsain+6lPQOum8j/7ZONIV0ou5yzAIg==</InverseQ><D>W3GOjdoQV6n5c3Vb1rcSebsIEaRcAS04EGmjqqPJwxVMHkiUBbi77DRMaQQCPYPnhouW0mhs4+AvxdoOTNG/HqpdL3rKOpuvhvvftRAAL5IYkwhUEsSWJ6A1U6BY6cVuE0gyDkx4Kj9CkVmeQby0WPxmRTCmmyPEt1wOcdJngqE=</D></RSAKeyValue>"
            };


            DigitalSignatureResult res = sender.BuildSignedMessage("This message is digitally signed");
            Console.WriteLine(res.CipherText);
            Console.WriteLine(res.SignatureText);

            String decryptedText = receiver.ExtractMessage(res);
            Console.WriteLine(decryptedText);

            Console.ReadKey();
        }
    }
}
