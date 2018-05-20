using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Services.Abstract;
using Ninject;

namespace MySocNet.Mvc.Providers
{
    public class MySocNetPrincipal : IPrincipal
    {
        //#region Identity properties
        public int UserId { get; set; }
        /// <summary>
        /// UserName, Email
        /// </summary>
        public string Login { get; set; }
        public string[] Roles { get; set; }
        //#endregion

        //private MySocNetMembershipUser _membershipUser;

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

        public MySocNetPrincipal(string username)
        {
            //UserDto userDto = MvcApplication.NinjectKernel.Get<IServiceFacade>()
            //    .UserService.Get.ByLogin(username);

            //_membershipUser = new MySocNetMembershipUser(userDto);

            Identity = new GenericIdentity(username);
        }

        public MySocNetPrincipal(MySocNetMembershipUser membershipUser) : this(membershipUser.Login)
        {
            UserId = membershipUser.UserId;
            Login = membershipUser.Login;
            Roles = membershipUser.Roles.ToArray();
        }
    }
}