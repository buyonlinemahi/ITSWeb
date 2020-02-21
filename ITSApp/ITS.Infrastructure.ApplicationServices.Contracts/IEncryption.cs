using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Infrastructure.ApplicationServices.Contracts
{
    public interface IEncryption
    {
        string HashPassword(string password);

        /// <summary>
        /// Verifies the current plain text password with the hashed password provided
        /// </summary>
        /// <param name="password">plain text password to verify</param>
        /// <param name="hashedPassword">hashed password</param>
        /// <returns></returns>
        bool VerifyHashedPassword(string password, string hashedPassword);
        //string EncryptString(string plainText);
        //string DecryptString(string plainText);
    }
}
