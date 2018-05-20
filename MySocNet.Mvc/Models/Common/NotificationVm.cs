using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MySocNet.Mvc.Models.Common
{
    
    public enum NotificationTypeVm
    {
        [Display(Name = "Ваш пост нарушает правила MySocNet")]
        PostComplain,

        [Display(Name = "Ваш пост был удален за нарушение правил MySocNet")]
        PostDeletion,

        [Display(Name = "Ваша страница(посты / описание) нарушает правила MySocNet")]
        UserPageComplain,

        [Display(Name = "Ваш пост была удален за нарушение правил MySocNet")]
        UserPageDeletion,

        [Display(Name = "Ваш тред нарушает правила MySocNet")]
        ThreadComplain,

        [Display(Name = "Ваш тред была удален за нарушение правил MySocNet")]
        ThreadDeletion,
    }

    public class NotificationVm
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ModeratorId { get; set; }

        [Display(Name = "Тип уведомления")]
        public NotificationTypeVm NotificationType { get; set; }

        [Display(Name = "Сообщение")]
        public string NotificationMessage { get; set; }

        public NotificationVm()
        {

        }

        public NotificationVm(int id)
        {
            Id = id;
        }
    }
}