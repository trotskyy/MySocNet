using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Passwod { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
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
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Male, Female or not mentioned
        /// </summary>
        public bool? IsMale { get; set; }
        /// <summary>
        /// Путь, по которому лежит фотка аватара юзера
        /// </summary>
        public string AvatarPath { get; set; }

        public UserDto()
        {

        }

        public UserDto(int id)
        {
            Id = id;
        }
    }
}
