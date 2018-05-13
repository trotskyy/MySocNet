using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocNet.Dal.Entities
{
    /// <summary>
    /// Conversational thread
    /// </summary>
    public class ConvThread
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Тема треда
        /// </summary>
        public string Topic { get; set; }
        public string Description { get; set; }
        public string AvatarPath { get; set; }

        //[ForeignKey("Moderator")]
        public int ModeratorId { get; set; }
        /// <summary>
        /// Модератор. По умолчанию модератором является создатель треда
        /// </summary>
        public User Moderator { get; set; }
        /// <summary>
        /// Юзеры, подписанные на тред
        /// </summary>
        public ICollection<User> Subscribers { get; set; }

        /// <summary>
        /// Посты в треде
        /// </summary>
        public ICollection<Post> Posts { get; set; }
    }
}
