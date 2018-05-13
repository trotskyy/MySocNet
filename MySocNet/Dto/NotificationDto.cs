using System;
using System.Collections.Generic;
using System.Text;
using MySocNet.Dal.Common;

namespace MySocNet.Bll.Dto
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public int NotificationMessage { get; set; }
        public bool IsRead { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }

        public NotificationDto()
        {

        }

        public NotificationDto(int id)
        {
            Id = id;
        }
    }
}
