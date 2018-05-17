using MySocNet.Bll.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MySocNet.Bll.Services.Abstract
{
    public interface IMessageService
    {
        IMessageSelectService Get { get; }

        void ReadMessage(MessageDto message);
        void ReadMessages(IEnumerable<MessageDto> messages);

        void WriteMessage(MessageDto message);
        void WriteMessage(UserDto from, UserDto to, string text);
    }
}