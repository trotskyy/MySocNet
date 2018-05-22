using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using MySocNet.Mvc.Models.Utils;
using MySocNet.Mvc.Models.Common;

namespace MySocNet.Mvc.Models.User
{
    public class UserPageVm
    {
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
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Male, Female or not mentioned
        /// </summary>
        [BooleanDisplayValues("М", "Ж")]
        public bool? IsMale { get; set; }

        /// <summary>
        /// Post and author name
        /// </summary>
        public List<KeyValuePair<PostVm, string>> Wall { get; set; }
    }
}