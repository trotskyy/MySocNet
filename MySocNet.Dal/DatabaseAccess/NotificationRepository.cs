using MySocNet.Dal.Abstract;
using MySocNet.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MySocNet.Dal
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(MySocNetContext dbContext):base(dbContext)
        {
        }

        public List<Notification> GetNotificationsByReceiver(User user)
        {
            return _dbContext.Notifications
                .AsNoTracking()
                .Where(n => n.UserId == user.Id)
                .ToList();
        }

        public List<Notification> GetNotificationsBySender(User moderator)
        {
            return _dbContext.Notifications
                .AsNoTracking()
                .Where(n => n.ModeratorId == moderator.Id)
                .ToList();
        }
    }
}
