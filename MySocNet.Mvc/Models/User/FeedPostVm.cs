using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MySocNet.Mvc.Models.User
{
    public class FeedPostVm : MySocNet.Mvc.Models.Common.PostVm
    {
        public string AuthorFullName { get; set; }
        public string ThreadName { get; set; }
    }
}