using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Filters;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByLogin(string login);
        /// <summary>
        /// Получить всех пользователей соответствующих фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<User> GetAllUsersMatching(UserFilter filter);

        /// <summary>
        /// Получить всех подписчиков пользователя
        /// </summary>
        /// <param name="publisher">пользователь</param>
        /// <returns></returns>
        List<User> GetAllSubscribersOf(User publisher);

        /// <summary>
        /// Получить N последних подписчиков пользователя
        /// </summary>
        /// <param name="publisher">пользователь</param>
        /// <param name="top">N</param>
        /// <returns></returns>
        List<User> GetTopLastSubscribersOf(User publisher, int top);

        /// <summary>
        /// Получить N последних подписчиков пользователя, следующих за М последних подписчиков
        /// </summary>
        /// <param name="publisher">пользователь</param>
        /// <param name="skip">M - которые пропускаем</param>
        /// <param name="top">N - которые берем</param>
        /// <returns></returns>
        List<User> GetTopLastSubscribersOf(User publisher, int skip, int top);

        /// <summary>
        /// Получить всех подписчиков пользователя, соответствующих фильтру
        /// </summary>
        /// <param name="publisher">пользователь</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<User> GetAllSubscribersOfMatching(User publisher, UserFilter filter);

        /// <summary>
        /// Получить все НЕ подписки, соответствующие фильтру
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<User> GetAllNonSubscriptionsOfMatching(User subscriber, UserFilter filter);

        /// <summary>
        /// Получить все подписки пользователя
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <returns></returns>
        List<User> GetAllSubscriptionsOf(User subscriber);

        /// <summary>
        /// Получить N последних подписок пользователя
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <param name="top">N</param>
        /// <returns></returns>
        List<User> GetTopLastSubscriptionsOf(User subscriber, int top);

        /// <summary>
        /// Получить N последних подписок пользователя, следующих за М последних подписок
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <param name="skip">M - которые пропускаем</param>
        /// <param name="top">N - которые берем</param>
        /// <returns></returns>
        List<User> GetTopLastSubscriptionsOf(User subscriber, int skip, int top);

        /// <summary>
        /// Получить все подписки пользователя, соответствующие фильтру
        /// </summary>
        /// <param name="subscriber">пользователь</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<User> GetAllSubscriptionsOfMatching(User subscriber, UserFilter filter);

        List<User> GetTopLastFriendsOfMatching(User user, int skip = -1, int top = -1, UserFilter filter = null);

        /// <summary>
        /// Получить кол-во подписчиков
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns></returns>
        int GetCountOfSubscribers(User publisher);

        /// <summary>
        /// Получить кол-во подписок
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        int GetCountOfSubscriptions(User subscriber);

        /// <summary>
        /// Получить непросмотренные заявки в друзья (новых подписчиков)
        /// </summary>
        /// <param name="publisher"></param>
        /// <returns></returns>
        List<User> GetUnviewedFriendshipRequestOf(User publisher);

        List<User> GetAllModerators();

        List<User> GetAllFriendsOf(User user);
    }
}
