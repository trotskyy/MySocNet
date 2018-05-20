using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MySocNet.Mvc.Models.Common
{
    public class ThreadFilterVm
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Тема")]
        public string Topic { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}