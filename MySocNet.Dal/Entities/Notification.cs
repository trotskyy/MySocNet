﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Common;

namespace MySocNet.Dal.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsRead { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
