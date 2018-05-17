using System;
using System.Collections.Generic;
using System.Text;
using MySocNet.Bll.Dto;
using MySocNet.Dal.Filters;

namespace MySocNet.Bll.Services.Abstract
{
    public interface IUserService
    {
        IUserSelectService Get { get; }

        void Subscribe(UserDto publisher, UserDto subscriber);
        void Subscribe(int publisherId, int subscriberId);
        /// <summary>
        /// Checks login, and password hash
        /// </summary>
        /// <param name="user">With login and password (clean, no hash)</param>
        /// <param name="isLoginValid"></param>
        /// <param name="isPasswordValid"></param>
        void CheckLoginAndPassword(UserDto user, out bool isLoginValid, out bool isPasswordValid);

        /// <summary>
        /// Создать аккаунт нового пользователя
        /// </summary>
        void CreateAccount(UserDto user);
        /// <summary>
        /// Создать аккаунт нового пользователя
        /// </summary>
        void CreateAccount(string login, string password);
    }
}
