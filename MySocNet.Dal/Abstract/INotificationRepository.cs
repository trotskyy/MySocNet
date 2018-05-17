using MySocNet.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Dal.Abstract
{
    public interface INotificationRepository : IRepository<Notification>
    {
        /// <summary>
        /// Получить уведомления по получателю (обычный юзер)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<Notification> GetNotificationsByReceiver(User user);
        /// <summary>
        /// Получить уведомления по отправителю (Модератор)
        /// </summary>
        /// <param name="moderator"></param>
        /// <returns></returns>
        List<Notification> GetNotificationsBySender(User moderator);
    }
}
