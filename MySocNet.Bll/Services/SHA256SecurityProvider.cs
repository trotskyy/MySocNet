using System;
using System.Collections.Generic;
using System.Text;
using MySocNet.Bll.Services.Abstract;
using System.Security.Cryptography;

namespace MySocNet.Bll.Services
{
    public class SHA256SecurityProvider : ISecurityProvider
    {

        public SHA256SecurityProvider()
        {
            
        }

        public string ComputeHash(string original)
        {
            byte[] input = Encoding.UTF8.GetBytes(original);
            SHA256Managed sHA256Managed = new SHA256Managed();
            byte[] hash = sHA256Managed.ComputeHash(input);
            return Convert.ToBase64String(hash);
        }

        public string ComputeHash(string original, string salt)
        {
            return ComputeHash(original + salt);
        }

        public string GenerateSalt(int size)
        {
            var randomNumberGenerator = new RNGCryptoServiceProvider();
            var randomNumbers = new byte[size];
            randomNumberGenerator.GetBytes(randomNumbers);
            return Convert.ToBase64String(randomNumbers);
        }
    }
}
