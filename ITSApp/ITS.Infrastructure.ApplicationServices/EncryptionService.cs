using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Security.Cryptography;
using System.IO;

namespace ITS.Infrastructure.ApplicationServices
{
    public class EncryptionService : IEncryption
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 11);
        }

        public bool VerifyHashedPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        //string _key = "qwertyuiopasdfghjklzxcvbnm1234567890";
        //public string EncryptString(string encryptString)
        //{
        //    TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
        //    MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
        //    byte[] byteHash, byteBuff;
        //    string strTempKey = _key;
        //    byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
        //    objHashMD5 = null;
        //    objDESCrypto.Key = byteHash;
        //    objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
        //    byteBuff = ASCIIEncoding.ASCII.GetBytes(encryptString);
        //    return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
        //    TransformFinalBlock(byteBuff, 0, byteBuff.Length)).Replace("/", "!").Replace("+", "]");
        //}

        //public string DecryptString(string cipherText)
        //{
        //    cipherText = cipherText.Replace("!", "/").Replace("]", "+");
        //    TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
        //    MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
        //    byte[] byteHash, byteBuff;
        //    string strTempKey = _key;
        //    byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
        //    objHashMD5 = null;
        //    objDESCrypto.Key = byteHash;
        //    objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
        //    byteBuff = Convert.FromBase64String(cipherText);
        //    string strDecrypted = ASCIIEncoding.ASCII.GetString
        //    (objDESCrypto.CreateDecryptor().TransformFinalBlock
        //    (byteBuff, 0, byteBuff.Length));
        //    objDESCrypto = null;
        //    return strDecrypted;
        //}


    }
}
