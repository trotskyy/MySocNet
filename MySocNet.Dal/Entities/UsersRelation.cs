using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Dal.Entities
{
    /// <summary>
    /// Задает связь подписчик - подписка
    /// </summary>
    public class UsersRelation
    {
        public int Id { get; set; }
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public User Publisher { get; set; }
        [ForeignKey("Subscriber")]
        public int SubscriberId { get; set; }
        public User Subscriber { get; set; }
        /// <summary>
        /// Для отображения новых заявок на страничке Publisher-а
        /// </summary>
        public bool IsViewed { get; set; }
    }
}
