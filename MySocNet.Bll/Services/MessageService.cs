using MySocNet.Bll.Dto;
using MySocNet.Bll.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Bll.Exceptions;
using MySocNet.Dal.Entities;
using MySocNet.Bll.Dto.Utils;

namespace MySocNet.Bll.Services
{
    public class MessageService : GenericService<MessageDto, Message>, IMessageService
    {
        IMessageSelectService _messageSelectService;

        public MessageService(IUnitOfWorkFactory unitOfWorkFactory):base(unitOfWorkFactory)
        {  
        }

        public IMessageSelectService Get
        {
            get
            {
                if(_messageSelectService == null)
                    _messageSelectService = new MessageSelectService(_unitOfWorkFactory);
                return _messageSelectService;
            }
        }

        private void ValidateMessage(MessageDto message)
        {
            if (message == null)
                throw new ArgumentNullException();
            if (message.Id == 0)
                throw new IdNotSpecifiedException();
        }

        public void ReadMessage(MessageDto message)
        {
            ValidateMessage(message);

            message.IsRead = true;

            ExecuteNonQuery(uow => {
                uow.MessageRepository.Update(message.MapToDbEntity());
                uow.SaveChanges();
            });
        }

        public void ReadMessages(IEnumerable<MessageDto> messages)
        {
            foreach (var m in messages)
            {
                ValidateMessage(m);
                m.IsRead = true;
            }

            ExecuteNonQuery(uow => {
                foreach (var m in messages)
                    uow.MessageRepository.Update(m.MapToDbEntity());
                uow.SaveChanges();
            });
        }

        public void WriteMessage(MessageDto message)
        {
            ExecuteNonQuery(uow => {
                uow.MessageRepository.Create(message.MapToDbEntity());
                uow.SaveChanges();
            });
        }

        public void WriteMessage(UserDto from, UserDto to, string text)
        {
            WriteMessage(new MessageDto { From = from, To = to, Text = text });
        }

        public void DeleteMessage(int id)
        {
            ExecuteNonQuery(uow =>
            {
                uow.MessageRepository.Delete(new Message { Id = id });
                uow.SaveChanges();
            });
        }
    }
}
