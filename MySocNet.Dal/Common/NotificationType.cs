using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Dal.Common
{
    public enum NotificationType
    {
        /// <summary>
        /// Новый подписчик
        /// </summary>
        NewSubscriber,
        /// <summary>
        /// Новое сообщение
        /// </summary>
        NewMessage,
        /// <summary>
        /// Новый пост в треде самого пользователя
        /// </summary>
        NewPostOnUsersWall,
        /// <summary>
        /// Новый пост от юзера на которого тот подписан
        /// </summary>
        NewPostOfSubscriptionUser,
        /// <summary>
        /// Новый пост в треде, на который подписан юзер
        /// </summary>
        NewPostInSubsrciptionThread
    }
}
