using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int FromId { get; set; }
        /// <summary>
        /// Отправитель
        /// </summary>
        public UserDto From { get; set; }

        public int ToId { get; set; }
        /// <summary>
        /// Получатель
        /// </summary>
        public UserDto To { get; set; }

        public string Text { get; set; }
        /// <summary>
        /// Было ли сообщение прочитано
        /// </summary>
        public bool? IsRead { get; set; }

        public DateTime? Sent { get; set; }

        public MessageDto()
        {

        }

        public MessageDto(int id)
        {
            Id = id;
        }
    }
}
