using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Abstract;
using System.Data.Entity;
using MySocNet.Dal.Entities;
using MySocNet.Dal.Filters;

namespace MySocNet.Dal
{
    public static class ThreadRepositoryUtils
    {
        public static IQueryable<ConvThread> FilteredBy(this IQueryable<ConvThread> seq, ThreadFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Description))
                seq = seq.Where(t => t.Description.Contains(filter.Description));
            if (!string.IsNullOrWhiteSpace(filter.Name))
                seq = seq.Where(t => t.Name.Contains(filter.Name));
            if (!string.IsNullOrWhiteSpace(filter.Topic))
                seq = seq.Where(t => t.Topic.Contains(filter.Topic));
            return seq;
        }
    }

    public class ThreadIdEqualityComparer : IEqualityComparer<ConvThread>
    {
        public bool Equals(ConvThread x, ConvThread y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(ConvThread obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class ThreadRepository : GenericRepository<ConvThread>, IThreadRepository
    {
        public ThreadRepository(MySocNetContext dbContext) : base(dbContext)
        {
        }

        public List<string> GetAllTopics()
        {
            return _dbContext.Threads
                .AsNoTracking()
                .Where(t => t.Topic != string.Empty)
                .Select(t => t.Topic)
                .Distinct()
                .ToList();
        }

        public List<string> GetAllTopicsStartingWith(string start)
        {
            return _dbContext.Threads
                .AsNoTracking()
                .Where(t => t.Topic.StartsWith(start) && t.Topic != string.Empty)
                .Select(t => t.Topic)
                .Distinct()
                .ToList();
        }

        public List<ConvThread> GetByModerator(User moderator)
        {
            return _dbContext.Threads
                .AsNoTracking()
                .Where(t => t.Id == moderator.Id)
                .ToList();
        }

        public int GetSubscribersCount(ConvThread thread)
        {
            return _dbContext.Threads
                .Where(t => t.Id == thread.Id)
                .Include(t => t.Subscribers)
                .AsNoTracking()
                .SelectMany(t => t.Subscribers)
                .Count();
        }

        public List<KeyValuePair<ConvThread, int>> GetThreadsWithSubscribersCountByModerator(User moderator)
        {
            return _dbContext.Threads
                .AsNoTracking()
                .Where(t => t.ModeratorId == moderator.Id)
                .Include(t => t.Subscribers)
                .Select(t => new KeyValuePair<ConvThread, int>(t, t.Subscribers.Count))
                .ToList();
        }

        public List<KeyValuePair<ConvThread, int>> GetThreadsWithSubscribersCountByModeratorMatching(User moderator, ThreadFilter filter)
        {
            return _dbContext.Threads
                .AsNoTracking()
                .FilteredBy(filter)
                .Where(t => t.ModeratorId == moderator.Id)
                .Include(t => t.Subscribers)
                .Select(t => new KeyValuePair<ConvThread, int>(t, t.Subscribers.Count))
                .ToList();
        }

        public List<KeyValuePair<ConvThread, int>> GetThreadsWithSubscribersCountBySubscriber(User subscriber)
        {
            return _dbContext.Threads
                .AsNoTracking()
                .Include(t => t.Subscribers)
                .Where(t => t.Subscribers.Contains(subscriber, new UserIdEqualityComparer()))
                .Select(t => new KeyValuePair<ConvThread, int>(t, t.Subscribers.Count))
                .ToList();
        }

        public List<KeyValuePair<ConvThread, int>> GetThreadsWithSubscribersCountBySubscriberMatching(User subscriber, ThreadFilter filter)
        {
            return _dbContext.Threads
                .AsNoTracking()
                .Include(t => t.Subscribers)
                .FilteredBy(filter)
                .Where(t => t.Subscribers.Contains(subscriber, new UserIdEqualityComparer()))
                .Select(t => new KeyValuePair<ConvThread, int>(t, t.Subscribers.Count))
                .ToList();
        }

        public List<ConvThread> GetWall(User wallOwner)
        {
            return _dbContext.Threads
                .AsNoTracking()
                .Where(t => t.Name == string.Empty && t.ModeratorId == wallOwner.Id)
                .ToList();
        }
    }
}
