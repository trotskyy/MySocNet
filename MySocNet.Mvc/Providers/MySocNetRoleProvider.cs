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

    public class MySocNetRoleProvider : RoleProvider
    {
        IServiceFacade _serviceFacade;

        public MySocNetRoleProvider()
        {
            _serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();
        }

        public override string ApplicationName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username">Login</param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return null;

            string[] roles = new string[] { };

            //username = login
            UserDto user = _serviceFacade.UserService.Get.ByLogin(username);

            if (user == null)
                return roles;

            return new MySocNetMembershipUser(user).Roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            //username = login
            return GetRolesForUser(username).Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}