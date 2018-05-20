using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MySocNet.Mvc.Models.Common
{
    public class ThreadVm
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        /// <summary>
        /// Тема треда
        /// </summary>
        [Display(Name = "Тема")]
        public string Topic { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        public string AvatarPath { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ModeratorId { get; set; }

        public ThreadVm()
        {

        }

        public ThreadVm(int id)
        {
            Id = id;
        }
    }
}