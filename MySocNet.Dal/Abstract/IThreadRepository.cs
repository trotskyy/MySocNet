using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Entities;
using MySocNet.Dal.Filters;

namespace MySocNet.Dal.Abstract
{
    public interface IThreadRepository : IRepository<ConvThread>
    {
        int GetSubscribersCount(ConvThread thread);

        List<ConvThread> GetByModerator(User moderator);

        /// <summary>
        /// Трэд с кол-вом подписчиков, которые ведет юзер
        /// </summary>
        /// <param name="moderator"></param>
        /// <returns>Список трэд - колво подписоты KeyValuePair</returns>
        List<KeyValuePair<ConvThread, int>> GetThreadsWithSubscribersCountByModerator(User moderator);

        /// <summary>
        /// Трэд с кол-вом подписчиков, на которые подписан юзер
        /// </summary>
        /// <param name="subscriber"></param>
        /// <returns>Список трэд - колво подписоты KeyValuePair</returns>
        List<KeyValuePair<ConvThread, int>> GetThreadsWithSubscribersCountBySubscriber(User subscriber);

        /// <summary>
        /// Трэд с кол-вом подписчиков, которые ведет юзер, которые соответствуют фильтру
        /// </summary>
        /// <param name="moderator"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<KeyValuePair<ConvThread, int>> GetThreadsWithSubscribersCountByModeratorMatching(User moderator, ThreadFilter filter);

        /// <summary>
        /// Трэд с кол-вом подписчиков, на которые подписан юзер, которые соответствуют фильтру
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<KeyValuePair<ConvThread, int>> GetThreadsWithSubscribersCountBySubscriberMatching(User subscriber, ThreadFilter filter);

        List<string> GetAllTopics();

        List<string> GetAllTopicsStartingWith(string start);
    }
}
