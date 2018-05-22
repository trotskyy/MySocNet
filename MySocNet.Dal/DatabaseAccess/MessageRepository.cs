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

        public List<Message> GetAllUnreadMessagesFrom(User from)
        {
            return _dbContext.Messages
                .AsNoTracking()
                .Where(m => m.FromId == from.Id && !m.IsRead)
                .ToList();
        }

        public List<Message> GetAllUnreadMessagesTo(User to)
        {
            return _dbContext.Messages
                .AsNoTracking()
                .Where(m => m.ToId == to.Id && !m.IsRead)
                .ToList();
        }

        public List<Message> GetLatestMessagesFromTopLatestDialogs(User user, int top)
        {
            /*
             *   SELECT * FROM (SELECT MAX(Id) as Id FROM (SELECT Id, ToId as UserId FROM [Messages] WHERE FromId = 1
                  UNION
                  SELECT Id, FromId as UserId FROM [Messages] WHERE ToId = 1) as t
                  GROUP BY t.UserId) as Id
                  JOIN [Messages] as m
	                ON m.Id = Id.Id
             */

            var sent = _dbContext.Messages.AsNoTracking()
                .Where(m => m.FromId == user.Id)
                .Select(m => new { Id = m.Id, UserId = m.ToId});

            var received = _dbContext.Messages.AsNoTracking()
                .Where(m => m.ToId == user.Id)
                .Select(m => new { Id = m.Id, UserId = m.FromId });

            return sent.Union(received)
                .GroupBy(t => t.UserId)
                .Select(gr => gr.Max(t => t.Id))
                .Join(_dbContext.Messages.AsNoTracking(),
                    id => id,
                    m => m.Id,
                    (t, m) => m)
                .ToList();
        }

        public List<Message> GetLatestMessagesFromTopLatestDialogs(User user, int skip, int top)
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

        public List<Message> GetTopLatestMessagesOfDialogBetween(User user1, User user2, int top)
        {
            return _dbContext.Messages
                .AsNoTracking()
                .Where(m => (m.FromId == user1.Id && m.ToId == user2.Id) ||
                            (m.ToId == user1.Id && m.FromId == user2.Id))
                .OrderByDescending(m => m.Id)
                .Take(top)
                .ToList();
        }

        public List<Message> GetTopLatestMessagesOfDialogBetween(User user1, User user2, int skip, int top)
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
