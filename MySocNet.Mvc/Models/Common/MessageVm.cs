using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MySocNet.Mvc.Models.Common
{
    public class MessageVm
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int FromId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ToId { get; set; }

        public string Text { get; set; }
        /// <summary>
        /// Было ли сообщение прочитано
        /// </summary>
        [Display(Name = "прочитано")]
        public bool? IsRead { get; set; }

        [Display(Name = "Отправлено")]
        public DateTime? Sent { get; set; }

        public MessageVm()
        {

        }

        public MessageVm(int id)
        {
            Id = id;
        }
    }
}