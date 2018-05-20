using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MySocNet.Mvc.Models.Common
{
    public class PostVm
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ThreadId { get; set; }
        /// <summary>
        /// Тред, в котором был написан пост
        /// </summary>
        public ThreadVm Thread { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int AuthorId { get; set; }
        public UserVm Author { get; set; }

        [Display(Name = "Опубликован")]
        public DateTime Published { get; set; }

        public PostVm()
        {

        }

        public PostVm(int id)
        {
            Id = id;
        }
    }
}