using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using MySocNet.Mvc.Models.Utils;

namespace MySocNet.Mvc.Models.Common
{
    //TODO finish common view models
    public class UserVm
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        public string Passwod { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        /// <summary>
        /// Страна проживания
        /// </summary>
        [Display(Name = "Страна проживания")]
        public string CurrentState { get; set; }
        /// <summary>
        /// Город проживания
        /// </summary>
        [Display(Name = "Город проживания")]
        public string CurrentCity { get; set; }
        /// <summary>
        /// Страна рождения
        /// </summary>
        [Display(Name = "Страна рождения")]
        public string StateOfBirth { get; set; }
        /// <summary>
        /// Город рождения
        /// </summary>
        [Display(Name = "Город рождения")]
        public string CityOfBirth { get; set; }
        /// <summary>
        /// О себе
        /// </summary>
        [Display(Name = "О себе")]
        public string AboutSelf { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Male, Female or not mentioned
        /// </summary>
        [BooleanDisplayValuesAttribute("М","Ж")]
        public bool? IsMale { get; set; }
        /// <summary>
        /// Путь, по которому лежит фотка аватара юзера
        /// </summary>
        public string AvatarPath { get; set; }
        
        public bool? IsModerator { get; set; }

        public UserVm()
        {

        }

        public UserVm(int id)
        {
            Id = id;
        }
    }
}