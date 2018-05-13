using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Abstract;
using System.Data.Entity;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(MySocNetContext dbContext) : base(dbContext)
        {
        }

        public ICollection<Message> GetAllUnreadMessagesFrom(User from)
        {
            return _dbContext.Messages
                .AsNoTracking()
                .Where(m => m.FromId == from.Id && !m.IsRead)
                .ToList();
        }

        public ICollection<Message> GetAllUnreadMessagesTo(User to)
        {
            return _dbContext.Messages
                .AsNoTracking()
                .Where(m => m.ToId == to.Id && !m.IsRead)
                .ToList();
        }

        public ICollection<Message> GetLatestMessagesFromTopLatestDialogs(User user, int top)
        {
            return _dbContext.Messages
                .AsNoTracking()
                .Take(top)
                .Distinct()
                .Where(m => m.FromId == user.Id || m.ToId == user.Id)
                .OrderByDescending(m => m.Sent)
                .ToList();
        }

        public ICollection<Message> GetLatestMessagesFromTopLatestDialogs(User user, int skip, int top)
        {
            return _dbContext.Messages
                .AsNoTracking()
                .Skip(skip)
                .Take(top)
                .Distinct()
                .Where(m => m.FromId == user.Id || m.ToId == user.Id)
                .OrderByDescending(m => m.Sent)
                .ToList();
        }

        public ICollection<Message> GetTopLatestMessagesOfDialogBetween(User user1, User user2, int top)
        {
            return _dbContext.Messages
                .AsNoTracking()
                .Take(top)
                .Where(m => (m.FromId == user1.Id && m.ToId == user2.Id) ||
                            (m.ToId == user1.Id && m.FromId == user2.Id))
                .OrderByDescending(m => m.Sent)
                .ToList();
        }

        public ICollection<Message> GetTopLatestMessagesOfDialogBetween(User user1, User user2, int skip, int top)
        {
            return _dbContext.Messages
                .AsNoTracking()
                .Skip(skip)
                .Take(top)
                .Where(m => (m.FromId == user1.Id && m.ToId == user2.Id) ||
                            (m.ToId == user1.Id && m.FromId == user2.Id))
                .OrderByDescending(m => m.Sent)
                .ToList();
        }

        public int GetUnreadMessagesCountTo(User to)
        {
            return _dbContext.Messages
                .Where(m => m.ToId == to.Id && !m.IsRead)
                .Count();
        }
    }
}
