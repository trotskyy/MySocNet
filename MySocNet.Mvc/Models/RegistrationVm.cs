using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySocNet.Mvc.Models
{
    public class RegistrationVm
    {
        [Display(Name = "Логин")]
        [Required]
        [RegularExpression(@"[A-Za-z]+([A-Za-z]*[0-9]*)*", ErrorMessage = "Логин может состоять из букв и цифр")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Повторите пароль")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Имя")]
        [Required]
        [RegularExpression(@"[А-Яа-я]+", ErrorMessage = "Имя должно состоять из русских букв")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required]
        [RegularExpression(@"[А-Яа-я]+", ErrorMessage = "Имя должно состоять из русских букв")]
        public string LastName { get; set; }
    }
}