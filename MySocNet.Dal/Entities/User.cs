using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocNet.Dal.Entities
{
    public class User
    {
        public int Id { get; set; }
        //[Required]
        public string Login { get; set; }
        //[Required]
        public string PasswodHash { get; set; }
        public string PasswodSalt { get; set; }
        //[Required]
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        //[Required]
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Страна проживания
        /// </summary>
        public string CurrentState { get; set; }
        /// <summary>
        /// Город проживания
        /// </summary>
        public string CurrentCity { get; set; }
        /// <summary>
        /// Страна рождения
        /// </summary>
        public string StateOfBirth { get; set; }
        /// <summary>
        /// Город рождения
        /// </summary>
        public string CityOfBirth { get; set; }
        /// <summary>
        /// О себе
        /// </summary>
        public string AboutSelf { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Male, Female or not mentioned
        /// </summary>
        public bool? IsMale { get; set; }
        /// <summary>
        /// Путь, по которому лежит фотка аватара юзера
        /// </summary>
        public string AvatarPath { get; set; }

        public ICollection<Message> MessagesFromUser { get; set; }
        public ICollection<Message> MessagesToUser { get; set; }
        /// <summary>
        /// Треды, которые админит юзер
        /// </summary>
        public ICollection<ConvThread> Threads { get; set; }
        /// <summary>
        /// Треды, на которые юзер подписан
        /// </summary>
        public ICollection<ConvThread> ThreadSubscriptions { get; set; }
        /// <summary>
        /// Посты в тредах авторства пользователя
        /// </summary>
        public ICollection<Post> Posts { get; set; }
        /// <summary>
        /// Подписки
        /// </summary>
        public ICollection<UsersRelation> Subscriptions { get; set; }
        /// <summary>
        /// Подписчики
        /// </summary>
        public ICollection<UsersRelation> Subscribers { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}
