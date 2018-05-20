using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Bll.Dto;

namespace MySocNet.Bll.Services.Abstract
{
    public interface IThreadSelectService
    {
        int SubscribersCount(ThreadDto thread);

        ThreadDto ById(int id);

        /// <summary>
        /// Трэд с кол-вом подписчиков, которые ведет юзер
        /// </summary>
        /// <param name="moderator"></param>
        /// <returns>Список трэд - колво подписоты KeyValuePair</returns>
        List<KeyValuePair<ThreadDto, int>> ThreadsWithSubscribersCountByModerator(UserDto moderator);

        /// <summary>
        /// Трэд с кол-вом подписчиков, на которые подписан юзер
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns>Список трэд - колво подписоты KeyValuePair</returns>
        List<KeyValuePair<ThreadDto, int>> ThreadsWithSubscribersCountBySubscriber(UserDto subscriber);

        /// <summary>
        /// Трэд с кол-вом подписчиков, которые ведет юзер, которые соответствуют фильтру
        /// </summary>
        /// <param name="moderator"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<KeyValuePair<ThreadDto, int>> ThreadsWithSubscribersCountByModeratorMatching(UserDto moderator, ThreadFilterDto filter);

        /// <summary>
        /// Трэд с кол-вом подписчиков, на которые подписан юзер, которые соответствуют фильтру
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<KeyValuePair<ThreadDto, int>> ThreadsWithSubscribersCountBySubscriberMatching(UserDto subscriber, ThreadFilterDto filter);

        List<string> AllTopics();

        List<string> AllTopicsStartingWith(string start);
    }
}
