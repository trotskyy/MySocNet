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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(MySocNetContext dbContext) : base(dbContext)
        {
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
    }
}
