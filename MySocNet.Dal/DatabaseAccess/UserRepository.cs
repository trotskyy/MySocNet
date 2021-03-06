﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Abstract;
using System.Data.Entity;
using MySocNet.Dal.Filters;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal
{
    static class UserRepositoryUtils
    {
        public static IQueryable<User> FilteredBy(this IQueryable<User> seq, UserFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.FullName))
            {
                string[] fullNameParts = filter.FullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (fullNameParts.Length == 1)
                    seq = seq.Where(u => u.FirstName.StartsWith(fullNameParts[0]) || u.LastName.StartsWith(fullNameParts[0]));
                else
                    seq = seq.Where(u => (fullNameParts.Contains(u.FirstName) && fullNameParts.Contains(u.LastName)) ||
                                         (u.FirstName.Contains(fullNameParts[0]) && u.LastName.Contains(fullNameParts[1])));
            }

            if (!string.IsNullOrWhiteSpace(filter.AboutSelf))
                seq = seq.Where(u => u.AboutSelf.Contains(filter.AboutSelf));

            if (filter.AgeFrom != null)
                seq = seq.Where(u => u.DateOfBirth <= 
                    new DateTime(DateTime.Now.Year - (int)filter.AgeFrom, DateTime.Now.Month, DateTime.Now.Day));

            if (filter.AgeTo != null)
                seq = seq.Where(u => u.DateOfBirth >=
                    new DateTime(DateTime.Now.Year - (int)filter.AgeTo, DateTime.Now.Month, DateTime.Now.Day));

            if (!string.IsNullOrWhiteSpace(filter.CityOfBirth))
                seq = seq.Where(u => u.CityOfBirth == filter.CityOfBirth);

            if (!string.IsNullOrWhiteSpace(filter.CurrentCity))
                seq = seq.Where(u => u.CurrentCity == filter.CurrentCity);

            if (!string.IsNullOrWhiteSpace(filter.CurrentState))
                seq = seq.Where(u => u.CurrentState == filter.CurrentState);

            if (filter.IsMale != null)
                seq = seq.Where(u => u.IsMale == filter.IsMale);

            if (!string.IsNullOrWhiteSpace(filter.StateOfBirth))
                seq = seq.Where(u => u.StateOfBirth == filter.StateOfBirth);

            return seq;
        }
    }

    public class UserIdEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(User obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MySocNetContext dbContext) : base(dbContext)
        {
        }

        public List<User> GetAllFriendsOf(User user)
        {
            /*
             SELECT * FROM (SELECT SubscriberId FROM UsersRelations WHERE PublisherId = 1
              INTERSECT
             SELECT PublisherId FROM UsersRelations WHERE SubscriberId = 1) as ur
             JOIN Users as u
	           ON ur.SubscriberId = u.Id
             */
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.SubscriberId == user.Id)
                .Select(ur => ur.PublisherId) // subscriptions
                .Intersect(_dbContext.UserRelations
                            .AsNoTracking()
                            .Where(ur => ur.PublisherId == user.Id)
                            .Select(ur => ur.SubscriberId)) //subscribers
                .Join(_dbContext.Users.AsNoTracking(),
                        id => id,
                        u => u.Id,
                        (id, u) => u)
                .ToList();

        }

        public List<User> GetAllModerators()
        {
            return _dbContext.Users
                .AsNoTracking()
                .Where(u => u.IsModerator)
                .ToList();
        }

        public List<User> GetAllNonSubscriptionsOfMatching(User subscriber, UserFilter filter)
        {
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.SubscriberId != subscriber.Id && ur.PublisherId != subscriber.Id)
                .Join(_dbContext.Users.FilteredBy(filter),
                    ur => ur.PublisherId,
                    u => u.Id,
                    (ur, u) => u)
                .Distinct()
                .ToList();
        }

        public List<User> GetAllSubscribersOf(User publisher)
        {
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.PublisherId == publisher.Id)
                .Join(_dbContext.Users,
                    ur => ur.SubscriberId,
                    u => u.Id,
                    (ur, u) => u)
                .ToList();
        }

        public List<User> GetAllSubscribersOfMatching(User publisher, UserFilter filter)
        {
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.PublisherId == publisher.Id)
                .Join(_dbContext.Users.FilteredBy(filter),
                    ur => ur.SubscriberId,
                    u => u.Id,
                    (ur, u) => u)
                .ToList();
        }

        public List<User> GetAllSubscriptionsOf(User subscriber)
        {
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.SubscriberId == subscriber.Id)
                .Join(_dbContext.Users,
                    ur => ur.SubscriberId,
                    u => u.Id,
                    (ur, u) => u)
                .ToList();
        }

        public List<User> GetAllSubscriptionsOfMatching(User subscriber, UserFilter filter)
        {
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.SubscriberId == subscriber.Id)
                .Join(_dbContext.Users.FilteredBy(filter),
                    ur => ur.SubscriberId,
                    u => u.Id,
                    (ur, u) => u)
                .ToList();
        }

        public List<User> GetAllUsersMatching(UserFilter filter)
        {
            return _dbContext.Users
                .AsNoTracking()
                .FilteredBy(filter)
                .ToList();
        }

        public User GetByLogin(string login)
        {
            return _dbContext.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Login == login);
        }

        public int GetCountOfSubscribers(User publisher)
        {
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.PublisherId == publisher.Id)
                .Count();
        }

        public int GetCountOfSubscriptions(User subscriber)
        {
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.SubscriberId == subscriber.Id)
                .Count();
        }

        public List<User> GetTopLastFriendsOfMatching(User user, int skip = -1, int top = -1, UserFilter filter = null)
        {
            var result = _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.SubscriberId == user.Id) //all subscriptions
                .Select(ur => ur.PublisherId)
                .Intersect(_dbContext.UserRelations
                    .AsNoTracking()
                    .Where(ur => ur.PublisherId == user.Id) //all subscribers
                    .Select(ur => ur.SubscriberId))
                .Join(_dbContext.Users.AsNoTracking(),
                    id => id,
                    u => u.Id,
                    (ur, u) => u);

            if (filter != null)
                result = result.FilteredBy(filter);
            if (skip > 0)
                result = result.Skip(skip);
            if (top > 0)
                result = result.Take(top);

            return result.ToList();
        }

        public List<User> GetTopLastSubscribersOf(User publisher, int top)
        {
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.PublisherId == publisher.Id)
                .OrderByDescending(ur => ur.Id)
                .Take(top)
                .Join(_dbContext.Users,
                    ur => ur.SubscriberId,
                    u => u.Id,
                    (ur, u) => u)
                .ToList();
        }

        public List<User> GetTopLastSubscribersOf(User publisher, int skip, int top)
        {
            return _dbContext.UserRelations
                .AsNoTracking()
                .Where(ur => ur.PublisherId == publisher.Id)
                .OrderByDescending(ur => ur.Id)
                .Skip(skip)
                .Take(top)
                .Join(_dbContext.Users,
                    ur => ur.SubscriberId,
                    u => u.Id,
                    (ur, u) => u)
                .ToList();
        }

        public List<User> GetTopLastSubscriptionsOf(User subscriber, int top)
        {
            return _dbContext.UserRelations
               .AsNoTracking()
               .Where(ur => ur.SubscriberId == subscriber.Id)
               .OrderByDescending(ur => ur.Id)
               .Take(top)
               .Join(_dbContext.Users,
                   ur => ur.PublisherId,
                   u => u.Id,
                   (ur, u) => u)
               .ToList();
        }

        public List<User> GetTopLastSubscriptionsOf(User subscriber, int skip, int top)
        {
            return _dbContext.UserRelations
               .AsNoTracking()
               .Where(ur => ur.SubscriberId == subscriber.Id)
               .OrderByDescending(ur => ur.Id)
               .Skip(skip)
               .Take(top)
               .Join(_dbContext.Users,
                   ur => ur.PublisherId,
                   u => u.Id,
                   (ur, u) => u)
               .ToList();
        }

        public List<User> GetUnviewedFriendshipRequestOf(User publisher)
        {
            return _dbContext.UserRelations
                .Include(ur => ur.Subscriber)
                .AsNoTracking()
                .Where(ur => ur.PublisherId == publisher.Id && !ur.IsViewed)
                .Select(ur => ur.Subscriber)
                .ToList();
        }
    }
}
