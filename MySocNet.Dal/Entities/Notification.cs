using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Dal.Entities
{
    //TODO rewrite mock context for instance

    /// <summary>
    /// A censure notification from a Moderator to a User
    /// </summary>
    public class Notification
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        /// <summary>
        /// Receiver
        /// </summary>
        public User User { get; set; }

        [ForeignKey("Moderator")]
        public int ModeratorId { get; set; }
        /// <summary>
        /// Sender
        /// </summary>
        public User Moderator { get; set; }

        public string NotificationMessage { get; set; }
    }
}
