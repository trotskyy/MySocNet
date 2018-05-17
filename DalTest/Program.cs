using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal;
using System.Data.Entity;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Dto.Utils;
using AutoMapper;
using System.Diagnostics;
using MySocNet.Dal.Entities;

namespace DalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User() { Id = 2 };

            using (MySocNetContext _dbContext = new MySocNetContext())
            {
                _dbContext.Database.Log = sql => Debug.WriteLine($"\n\nSQL\n\n{sql}\n\n");

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
                var r = result.ToList().Select(p => p.Id).ToList();
                //if (skip > 0)
                //    result = result.Skip(skip);
                //if (top > 0)
                //    result = result.Take(top);
                int breakpoint = 10;
            }

            Console.WriteLine("Press ENTER to quit...");
            Console.ReadLine();
        }
    }
}
