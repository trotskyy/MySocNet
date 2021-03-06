﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        List<Post> GetByThread(ConvThread thread);
        List<Post> GetByThreads(IEnumerable<ConvThread> threads);

        List<Post> GetByAuthor(User author);
        List<Post> GetByAuthors(IEnumerable<User> authors);

        List<Post> GetByAuthorsOrThreads(IEnumerable<User> authors, IEnumerable<ConvThread> threads);

        List<Post> GetTopLatestFeedPosts(User user, int skip = -1, int top = -1, bool withAuthors = false);
        List<Post> GetAllTopRecentPosts(int skip, int top);
        List<Post> GetPostsByThreadName(string threadName, int top, int skip);
    }
}
