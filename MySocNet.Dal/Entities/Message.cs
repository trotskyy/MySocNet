using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Dal.Entities
{
    public class Message
    {
        public int Id { get; set; }

        [ForeignKey("From")]
        public int FromId { get; set; }
        /// <summary>
        /// Отправитель
        /// </summary>
        public User From { get; set; }

        [ForeignKey("To")]
        public int ToId { get; set; }
        /// <summary>
        /// Получатель
        /// </summary>
        public User To { get; set; }

        [Required]
        public string Text { get; set; }
        /// <summary>
        /// Было ли сообщение прочитано
        /// </summary>
        public bool IsRead { get; set; }

        public DateTime Sent { get; set; }
    }
}
