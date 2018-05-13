using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int ThreadId { get; set; }
        /// <summary>
        /// Тред, в котором был написан пост
        /// </summary>
        public ThreadDto Thread { get; set; }

        public int AuthorId { get; set; }
        public UserDto Author { get; set; }

        public DateTime Published { get; set; }

        public PostDto()
        {

        }

        public PostDto(int id)
        {
            Id = id;
        }
    }
}
