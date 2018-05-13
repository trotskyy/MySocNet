using System;
using System.Collections.Generic;
using System.Text;
using MySocNet.Bll.Services.Abstract;

namespace MySocNet.Bll.Services
{
    public class SecurityProvider : ISecurityProvider
    {
        public string GenerateHash(string original)
        {
            throw new NotImplementedException();
        }

        public string GenerateHash(string original, string salt)
        {
            throw new NotImplementedException();
        }

        public string GenerateSalt(int size)
        {
            throw new NotImplementedException();
        }
    }
}
