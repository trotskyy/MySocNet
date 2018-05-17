using MySocNet.Bll.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Bll.Services.Abstract
{
    public interface INotificationService
    {
        /// <summary>
        /// Deletes after reading
        /// </summary>
        void ReadNotification(NotificationDto notification);

        void WriteNotification(NotificationDto notification);

        /// <summary>
        /// Получить уведомления по получателю (обычный юзер)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<NotificationDto> GetNotificationsByReceiver(UserDto user);
        /// <summary>
        /// Получить уведомления по отправителю (Модератор)
        /// </summary>
        /// <param name="moderator"></param>
        /// <returns></returns>
        List<NotificationDto> GetNotificationsBySender(UserDto moderator);
    }
}
