using System;
using System.Collections.Generic;
using MySocNet.Bll.Dto;

namespace MySocNet.Bll.Services.Abstract
{
    public interface IMessageSelectService
    {
        /// <summary>
        /// Получить top последних диалогов юзера, каждый из которых содержит по последнему сообщению
        /// </summary>
        /// <param name="user"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        List<DialogDto> TopLatestDialogs(UserDto user, int top, bool withAuthor = false);

        /// <summary>
        /// Получить top последних диалогов юзера (каждый из которых содержит по последнему сообщению), следующих за skip последних диалогов юзера
        /// </summary>
        /// <param name="user"></param>
        /// <param name="skip">которые пропускаем</param>
        /// <param name="top">оторые берем</param>
        /// <returns></returns>
        List<DialogDto> TopLatestDialogs(UserDto user, int skip, int top);

        /// <summary>
        /// Получить top последних сообщений диалога
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <param name="top">N</param>
        /// <returns></returns>
        List<MessageDto> TopLatestMessagesOfDialog(DialogDto dialog, int top, bool withAuthor = false);

        /// <summary>
        /// Получить top последних сообщений диалога между двумя пользователями, следующих за skip последних сообщений диалога
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <param name="skip">которые пропускаем</param>
        /// <param name="top">которые берем</param>
        /// <returns></returns>
        List<MessageDto> TopLatestMessagesOfDialog(DialogDto dialog, int skip, int top);

        List<MessageDto> AllUnreadMessagesFrom(UserDto from);
        List<MessageDto> AllUnreadMessagesTo(UserDto to);
        /// <summary>
        /// Получить кол-во непрочитанных сообщений к юзеру
        /// </summary>
        /// <param name="to">получатель</param>
        /// <returns></returns>
        int UnreadMessagesCountTo(UserDto to);
    }
}