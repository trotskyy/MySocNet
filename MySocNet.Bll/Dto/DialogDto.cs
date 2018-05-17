using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Bll.Dto
{
    public class DialogDto
    {
        public int UserOneId { get; set; }
        public UserDto UserOne { get; set; }

        public int UserTwoId { get; set; }
        public UserDto UserTwo { get; set; }

        public List<MessageDto> Messages { get; set; }

        public DialogDto(int userOneId, int userTwoId)
        {
            UserOneId = userOneId;
            UserTwoId = userTwoId;
            Messages = new List<MessageDto>();
        }

        public DialogDto(MessageDto lastMsgInDialog) : this(lastMsgInDialog.FromId, lastMsgInDialog.ToId)
        {
            Messages.Add(lastMsgInDialog);
        }
    }
}
