using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Abstract;
using System.Data.Entity;
using MySocNet.Dal.Entities;
using System.Diagnostics;

namespace MySocNet.Dal
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(MySocNetContext dbContext) : base(dbContext)
        {
        }

        public List<Post> GetAllTopRecentPosts(int skip, int top)
        {
            var res = _dbContext.Posts
                .AsNoTracking();
            if (skip > 0)
                res = res.Skip(skip);
            if (top > 0)
                res = res.Take(top);
            return res.ToList();
        }

        public List<Post> GetByAuthor(User author)
        {
            return _dbContext.Posts
                .AsNoTracking()
                .Where(p => p.AuthorId == author.Id)
                .OrderByDescending(p => p.Published)
                .ToList();
        }

        public List<Post> GetByAuthors(IEnumerable<User> authors)
        {
            int[] authorIds = authors.Select(a => a.Id).ToArray();
            return _dbContext.Posts
                .AsNoTracking()
                .Where(p => authorIds.Contains(p.AuthorId))
                .OrderByDescending(p => p.Published)
                .ToList();
        }

        public List<Post> GetByAuthorsOrThreads(IEnumerable<User> authors, IEnumerable<ConvThread> threads)
        {
            int[] authorIds = authors.Select(a => a.Id).ToArray();
            int[] threadIds = threads.Select(t => t.Id).ToArray();

            return _dbContext.Posts
                .AsNoTracking()
                .Where(p => authorIds.Contains(p.AuthorId) || threadIds.Contains(p.ThreadId))
                .OrderByDescending(p => p.Published)
                .ToList();
        }

        public List<Post> GetByThread(ConvThread thread)
        {
            return _dbContext.Posts
                .AsNoTracking()
                .Where(p => p.ThreadId == thread.Id)
                .OrderByDescending(p => p.Published)
                .ToList();
        }

        public List<Post> GetByThreads(IEnumerable<ConvThread> threads)
        {
            int[] threadIds = threads.Select(t => t.Id).ToArray();
            return _dbContext.Posts
                .AsNoTracking()
                .Where(p => threadIds.Contains(p.ThreadId))
                .OrderByDescending(p => p.Published)
                .ToList();
        }

        public List<Post> GetTopLatestFeedPosts(User user, int skip = -1, int top = -1)
        {
            /*
              SELECT p.Id, p.AuthorId, p.ThreadId, p.Text, p.Published FROM (SELECT ConvThread_Id FROM ConvThreadUsers WHERE [User_Id] = 2) as t
              JOIN Posts as p
	            ON t.ConvThread_Id = p.ThreadId
	            UNION
              SELECT p.Id, p.AuthorId, p.ThreadId, p.Text, p.Published FROM(SELECT * FROM UsersRelations WHERE SubscriberId = 2) as ur
              JOIN Posts as p
	            ON ur.PublisherId = p.AuthorId
	            ORDER BY Published DESC             
             */
            var result = _dbContext.Users.Include(u => u.Threads)
                .AsNoTracking()
                .Where(u => u.Id == user.Id)
                .SelectMany(u => u.ThreadSubscriptions.Select(t => t.Id))
            .Join(_dbContext.Posts.AsNoTracking(),
                    threadId => threadId,
                    p => p.ThreadId,
                    (threadId, p) => p)
                .Union(_dbContext.UserRelations
                    .AsNoTracking()
                    .Where(ur => ur.SubscriberId == user.Id)
                    .Join(_dbContext.Posts.AsNoTracking(),
                        ur => ur.PublisherId,
                        p => p.AuthorId,
                        (ur, p) => p));
            if (skip > 0)
                result = result.Skip(skip);
            if (top > 0)
                result = result.Take(top);
            return result.OrderByDescending(p => p.Published).ToList();
        }
    }
}
