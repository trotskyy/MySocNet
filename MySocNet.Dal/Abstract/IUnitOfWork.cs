using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository MessageRepository { get; }
        IPostRepository PostRepository { get; }
        IThreadRepository ThreadRepository { get; }
        IUserRepository UserRepository { get; }
        IUsersRelationRepository UsersRelationRepository { get; }
        INotificationRepository NotificationRepository { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
