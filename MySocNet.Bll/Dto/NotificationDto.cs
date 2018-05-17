using System;
using System.Collections.Generic;
using System.Text;
using MySocNet.Dal.Filters;

namespace MySocNet.Bll.Dto
{
    public enum NotificationTypeDto
    {
        PostComplain,
        PostDeletion,
        UserPageComplain,
        UserPageDeletion,
        ThreadComplain,
        ThreadDeletion,
    }

    /// <summary>
    /// A censure notification from a Moderator to a User
    /// </summary>
    public class NotificationDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        /// <summary>
        /// Receiver
        /// </summary>
        public UserDto User { get; set; }

        public int ModeratorId { get; set; }
        /// <summary>
        /// Sender
        /// </summary>
        public UserDto Moderator { get; set; }

        public NotificationTypeDto NotificationType { get; set; }

        public string NotificationMessage { get; set; }

        public NotificationDto()
        {

        }

        public NotificationDto(int id)
        {
            Id = id;
        }
    }
}
