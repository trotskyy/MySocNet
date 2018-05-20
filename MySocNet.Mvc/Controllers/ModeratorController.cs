using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Mvc.Models;
using MySocNet.Mvc.Models.Common;
using MySocNet.Mvc.Models.Utils;
using MySocNet.Mvc.Providers;
using Ninject;
using AutoMapper;

namespace MySocNet.Mvc.Controllers
{
    [Authorize(Roles = Providers.Roles.Moderator)]
    public class ModeratorController : Controller
    {
        IServiceFacade _serviceFacade;

        public ModeratorController()
        {
            _serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();
        }

        // GET: Moderator page - like Feed of User
        public async Task<ActionResult> Index()
        {
            //TODO
            List<PostDto> recentPosts = _serviceFacade.PostService.Get.AllTopRecentPosts();
            return View();
        }

        public async Task<ActionResult> Users()
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<ActionResult> Threads()
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<ActionResult> UserPage()
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<ActionResult> Thread()
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<ActionResult> CreateNotificaton()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}