using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Services.Abstract;
using Ninject;

namespace MySocNet.Mvc.Providers
{
    public class MySocNetMembershipUser : MembershipUser
    {
        public MySocNetMembershipUser(UserDto user) : base("MySocNetMembershipProvider", user.Login, user.Id,
            user.Login, string.Empty, string.Empty, true, false, 
            DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.Id;
            Login = user.Login;
            Roles = new List<string>();

            if (user.IsModerator == true)
                Roles.Add(Providers.Roles.Moderator);
            else
                Roles.Add(Providers.Roles.User);
        }

        public int UserId { get; private set; }
        /// <summary>
        /// UserName, Email
        /// </summary>
        public string Login { get; private set; }
        public List<string> Roles { get; private set; }
    }
}