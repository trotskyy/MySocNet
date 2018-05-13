using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocNet.Dal.Entities
{
    /// <summary>
    /// Пост в треде
    /// </summary>
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int ThreadId { get; set; }
        /// <summary>
        /// Тред, в котором был написан пост
        /// </summary>
        public ConvThread Thread { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public DateTime Published { get; set; }
    }
}
