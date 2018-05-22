using MySocNet.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Bll.Services.Utils
{
    /// <summary>
    /// What is the name of thread ( = user's wall). ask it
    /// </summary>
    static class WallThreadNameResolver
    {
        public static string GetWallThreadName(User wallOwner)
        {
            return string.Empty;

            if (wallOwner == null)
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(wallOwner.Login))
                throw new ArgumentException("Provide login");

            return wallOwner.Login;
        }
    }
}
