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
        List<Notification> GetNotificationsForUser(User user);
    }
}
