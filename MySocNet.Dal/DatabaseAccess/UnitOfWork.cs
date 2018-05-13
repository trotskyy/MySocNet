using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Abstract;
using MySocNet.Dal;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        MySocNetContext _dbContext;

        MessageRepository _messageRepository;
        PostRepository _postRepository;
        ThreadRepository _threadRepository;
        UserRepository _userRepository;
        UsersRelationRepository _usersRelationRepository;
        NotificationRepository _notificationRepository;

        public UnitOfWork()
        {
            _dbContext = new MySocNetContext();
        }

        public IMessageRepository MessageRepository
        {
            get
            {
                if (_messageRepository is null)
                    _messageRepository = new MessageRepository(_dbContext);
                return _messageRepository;
            }
        }

        public IPostRepository PostRepository
        {
            get
            {
                if (_postRepository is null)
                    _postRepository = new PostRepository(_dbContext);
                return _postRepository;
            }
        }

        public IThreadRepository ThreadRepository
        {
            get
            {
                if (_threadRepository is null)
                    _threadRepository = new ThreadRepository(_dbContext);
                return _threadRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository is null)
                    _userRepository = new UserRepository(_dbContext);
                return _userRepository;
            }
        }

        public IUsersRelationRepository UsersRelationRepository
        {
            get
            {
                if (_usersRelationRepository is null)
                    _usersRelationRepository = new UsersRelationRepository(_dbContext);
                return _usersRelationRepository;
            }
        }

        public INotificationRepository NotificationRepository
        {
            get
            {
                if (_notificationRepository is null)
                    _notificationRepository = new NotificationRepository(_dbContext);
                return _notificationRepository;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
