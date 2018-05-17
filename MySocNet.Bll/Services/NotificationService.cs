using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Dal.Entities;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Dto.Utils;

namespace MySocNet.Bll.Services
{
    public class NotificationService : GenericService<NotificationDto, Notification>, INotificationService
    {
        public NotificationService(IUnitOfWorkFactory unitOfWorkFactory):base(unitOfWorkFactory)
        {

        }

        private void ValidateNotification(NotificationDto notification)
        {
            if (notification == null)
                throw new ArgumentNullException();
            if (notification.Id == 0)
                throw new IdNotSpecifiedException();
        }

        public List<NotificationDto> GetNotificationsByReceiver(UserDto user)
        {
            ValidateUser(user);

            return ExecuteSelectQuery(uow => uow.NotificationRepository
                .GetNotificationsByReceiver(user.MapToDbEntity()));
        }

        public List<NotificationDto> GetNotificationsBySender(UserDto moderator)
        {
            ValidateUser(moderator);

            return ExecuteSelectQuery(uow => uow.NotificationRepository.GetNotificationsBySender(moderator.MapToDbEntity()));
        }

        public void ReadNotification(NotificationDto notification)
        {
            ValidateNotification(notification);

            ExecuteNonQuery(uow => uow.NotificationRepository.Delete(notification.MapToDbEntity()));
        }

        public void WriteNotification(NotificationDto notification)
        {
            ValidateNotification(notification);

            ExecuteNonQuery(uow => {
                User user = uow.UserRepository.GetById(notification.ModeratorId);

                if (!user.IsModerator)
                    throw new DomainModelException("User must be a moderator to write notifications!");

                uow.NotificationRepository.Create(notification.MapToDbEntity());
            });
        }
    }
}
