using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Services;
using Ninject;
using AutoMapper;
using MySocNet.Mvc.Models.Common;
using MySocNet.Mvc.Models.Utils;
using MySocNet.Mvc.Providers;
using MySocNet.Mvc.Models.User;

namespace MySocNet.Mvc.Controllers
{
    [MSNAuthorize]
    public class PostController : Controller
    {
        IServiceFacade _serviceFacade;

        public PostController()
        {
            _serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Write(string text, int authorId, int? threadId)
        {
            if(threadId == null)
                threadId = _serviceFacade.UserService.Get.UserWallThreadId(new UserDto(((MySocNetPrincipal)User).UserId));

            PostDto postDto = new PostDto { AuthorId = authorId, Text = text, ThreadId = threadId.Value, Published = DateTime.Now };
            _serviceFacade.PostService.Write(postDto);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}