using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal.Abstract
{
    public interface IMessageRepository:IRepository<Message>
    {
        /// <summary>
        /// Получить по одному последнему сообщению на каждый диалог из N последних диалогов юзера
        /// </summary>
        /// <param name="user"></param>
        /// <param name="top">N</param>
        /// <returns></returns>
        List<Message> GetLatestMessagesFromTopLatestDialogs(User user, int top);

        /// <summary>
        /// Получить по одному последнему сообщению на каждый диалог из N последних диалогов юзера, следующих за М последних диалогов юзера
        /// </summary>
        /// <param name="user"></param>
        /// <param name="skip">М - которые пропускаем</param>
        /// <param name="top">N - которые берем</param>
        /// <returns></returns>
        List<Message> GetLatestMessagesFromTopLatestDialogs(User user, int skip, int top);

        /// <summary>
        /// Получить N последних сообщений диалога между двумя пользователями
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <param name="top">N</param>
        /// <returns></returns>
        List<Message> GetTopLatestMessagesOfDialogBetween(User user1, User user2, int top);

        /// <summary>
        /// Получить N последних сообщений диалога между двумя пользователями, следующих за М последних сообщений диалога
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <param name="skip">M - - которые пропускаем</param>
        /// <param name="top">N - которые берем</param>
        /// <returns></returns>
        List<Message> GetTopLatestMessagesOfDialogBetween(User user1, User user2, int skip, int top);

        List<Message> GetAllUnreadMessagesFrom(User from);
        List<Message> GetAllUnreadMessagesTo(User to);
        /// <summary>
        /// Получить кол-во непрочитанных сообщений к юзеру
        /// </summary>
        /// <param name="to">получатель</param>
        /// <returns></returns>
        int GetUnreadMessagesCountTo(User to);
    }
}
