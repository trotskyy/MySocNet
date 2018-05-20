using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocNet.Mvc.Models.Common
{
    public class UserFilterVm
    {
        [Display(Name = "Имя Фамилия")]
        public string FullName { get; set; }

        [Display(Name = "Страна проживания")]
        public string CurrentState { get; set; }

        [Display(Name = "Город проживания")]
        public string CurrentCity { get; set; }

        [Display(Name = "Страна рождения")]
        public string StateOfBirth { get; set; }

        [Display(Name = "Город рождения")]
        public string CityOfBirth { get; set; }

        [Display(Name = "Возраст от")]
        public int? AgeFrom { get; set; }

        [Display(Name = "Возраст до")]
        public int? AgeTo { get; set; }

        [Display(Name = "Пол")]
        public bool? IsMale { get; set; }

        [Display(Name = "О себе")]
        public string AboutSelf { get; set; }
    }
}