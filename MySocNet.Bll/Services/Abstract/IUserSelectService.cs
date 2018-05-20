using MySocNet.Bll.Dto;
using MySocNet.Dal.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Services.Abstract
{
    public interface IUserSelectService
    {
        UserDto ById(int id);

        UserDto ByLogin(string login);

        /// <summary>
        /// Получить всех пользователей соответствующих фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<UserDto> AllUsersMatching(UserFilterDto filter);
        /// <summary>
        /// Получить всех подписчиков пользователя
        /// </summary>
        /// <param name="publisher">пользователь</param>
        /// <returns></returns>
        List<UserDto> AllSubscribersOf(UserDto publisher);
        /// <summary>
        /// Получить N последних подписчиков пользователя
        /// </summary>
        /// <param name="publisher">пользователь</param>
        /// <param name="top">N</param>
        /// <returns></returns>
        List<UserDto> TopLastSubscribersOf(UserDto publisher, int top);

        /// <summary>
        /// Получить N последних подписчиков пользователя, следующих за М последних подписчиков
        /// </summary>
        /// <param name="publisher">пользователь</param>
        /// <param name="skip">M - которые пропускаем</param>
        /// <param name="top">N - которые берем</param>
        /// <returns></returns>
        List<UserDto> TopLastSubscribersOf(UserDto publisher, int skip, int top);

        /// <summary>
        /// Получить всех подписчиков пользователя, соответствующих фильтру
        /// </summary>
        /// <param name="publisher">пользователь</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<UserDto> AllSubscribersOfMatching(UserDto publisher, UserFilterDto filter);

        /// <summary>
        /// Получить все НЕ подписки, соответствующие фильтру
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<UserDto> AllNonSubscriptionsOfMatching(UserDto subscriber, UserFilterDto filter);

        /// <summary>
        /// Получить все подписки пользователя
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <returns></returns>
        List<UserDto> AllSubscriptionsOf(UserDto subscriber);

        /// <summary>
        /// Получить N последних подписок пользователя
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <param name="top">N</param>
        /// <returns></returns>
        List<UserDto> TopLastSubscriptionsOf(UserDto subscriber, int top);

        /// <summary>
        /// Получить N последних подписок пользователя, следующих за М последних подписок
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <param name="skip">M - которые пропускаем</param>
        /// <param name="top">N - которые берем</param>
        /// <returns></returns>
        List<UserDto> TopLastSubscriptionsOf(UserDto subscriber, int skip, int top);

        /// <summary>
        /// Получить все подписки пользователя, соответствующие фильтру
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<UserDto> AllSubscriptionsOfMatching(UserDto subscriber, UserFilterDto filter);

        /// <summary>
        /// Получить кол-во подписчиков
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns></returns>
        int CountOfSubscribers(UserDto publisher);

        /// <summary>
        /// Получить кол-во подписок
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        int CountOfSubscriptions(UserDto subscriber);

        /// <summary>
        /// Получить непросмотренные заявки в друзья (новых подписчиков)
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns></returns>
        List<UserDto> UnviewedFriendshipRequestOf(UserDto publisher);

        List<UserDto> AllModerators();

        List<UserDto> AllFriendsOf(UserDto user);
    }
}
