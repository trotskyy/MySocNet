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
    public class MessageSelectService : GenericService<MessageDto, Message>, IMessageSelectService
    {
        public MessageSelectService(IUnitOfWorkFactory unitOfWorkFactory) : base(unitOfWorkFactory)
        {
        }

        public List<MessageDto> AllUnreadMessagesFrom(UserDto from)
        {
            ValidateUser(from);

            return ExecuteSelectQuery(uow => uow.MessageRepository
                .GetAllUnreadMessagesFrom(from.MapToDbEntity()));
        }

        public List<MessageDto> AllUnreadMessagesTo(UserDto to)
        {
            ValidateUser(to);

            return ExecuteSelectQuery(uow => uow.MessageRepository
                .GetAllUnreadMessagesTo(to.MapToDbEntity()));
        }

        public List<DialogDto> TopLatestDialogs(UserDto user, int top, bool withAuthor = false)
        {
            if (top <= 0)
                throw new ArgumentOutOfRangeException("top");
            ValidateUser(user);

            var dialogs = ExecuteSelectQuery(uow => uow.MessageRepository
                .GetLatestMessagesFromTopLatestDialogs(user.MapToDbEntity(), top))
                .Select(msg => new DialogDto(msg))
                .ToList();
            
            if(withAuthor)
            {
                foreach (var d in dialogs)
                {
                    d.Messages.Last().From = ExecuteSelectQuery(uow => uow.UserRepository.GetById(d.Messages.Last().FromId)).MapToDtoEntity();
                    d.Messages.Last().To = ExecuteSelectQuery(uow => uow.UserRepository.GetById(d.Messages.Last().ToId)).MapToDtoEntity();
                }
            }

            return dialogs;
        }

        public List<DialogDto> TopLatestDialogs(UserDto user, int skip, int top)
        {
            if (skip <= 0)
                throw new ArgumentOutOfRangeException("skip");
            if (top <= 0)
                throw new ArgumentOutOfRangeException("top");
            ValidateUser(user);

            return ExecuteSelectQuery(uow => uow.MessageRepository
                .GetLatestMessagesFromTopLatestDialogs(user.MapToDbEntity(), skip, top))
                .Select(msg => new DialogDto(msg))
                .ToList();
        }

        public List<MessageDto> TopLatestMessagesOfDialog(DialogDto dialog, int top, bool withAuthor = false)
        {
            UserDto userOne = dialog?.UserOne ?? new UserDto() { Id = dialog.UserOneId };
            UserDto userTwo = dialog?.UserTwo ?? new UserDto() { Id = dialog.UserTwoId };

            if (top <= 0)
                throw new ArgumentOutOfRangeException("top");
            ValidateUser(userOne);
            ValidateUser(userTwo);

            return ExecuteSelectQuery(uow => {
                var res = uow.MessageRepository
                .GetTopLatestMessagesOfDialogBetween(userTwo.MapToDbEntity(), userOne.MapToDbEntity(), top);

                if (withAuthor)
                {
                    foreach (var m in res)
                    {
                        m.From = uow.UserRepository.GetById(m.FromId);
                        m.To = uow.UserRepository.GetById(m.ToId);
                    }
                }
                return res;
                });

        }

        public List<MessageDto> TopLatestMessagesOfDialog(DialogDto dialog, int skip, int top)
        {
            UserDto userOne = dialog?.UserOne ?? new UserDto() { Id = dialog.UserOneId };
            UserDto userTwo = dialog?.UserTwo ?? new UserDto() { Id = dialog.UserTwoId };

            if (top <= 0)
                throw new ArgumentOutOfRangeException("top");
            if (skip <= 0)
                throw new ArgumentOutOfRangeException("skip");
            ValidateUser(userOne);
            ValidateUser(userTwo);

            return ExecuteSelectQuery(uow => uow.MessageRepository
                .GetTopLatestMessagesOfDialogBetween(userTwo.MapToDbEntity(), userOne.MapToDbEntity(), skip, top));
        }

        public int UnreadMessagesCountTo(UserDto to)
        {
            ValidateUser(to);

            return ExecuteSelectQuery(uow => uow.MessageRepository
                .GetUnreadMessagesCountTo(to.MapToDbEntity()));
        }
    }
}
