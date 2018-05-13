using MySocNet.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Dto.Utils
{
    public static class UserExtensions
    {
        public static string FullName(this User user)
        {
            return $"{user.FirstName} {user.LastName}";
        }
    }
}
