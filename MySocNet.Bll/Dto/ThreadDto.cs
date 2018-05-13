using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Dto
{
    public class ThreadDto
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
        public UserDto Moderator { get; set; }
        /// <summary>
        /// Юзеры, подписанные на тред
        /// </summary>
        public ICollection<UserDto> Subscribers { get; set; }

        /// <summary>
        /// Посты в треде
        /// </summary>
        public ICollection<PostDto> Posts { get; set; }

        public ThreadDto()
        {

        }

        public ThreadDto(int id)
        {
            Id = id;
        }
    }
}
