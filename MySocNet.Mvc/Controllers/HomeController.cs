using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MySocNet.Mvc.Models;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Mvc.Models.Utils;
using Ninject;
using MySocNet.Mvc.Providers;

namespace MySocNet.Mvc.Controllers
{
    //actor Guest may have access to this controllers methods only
    [AllowAnonymous]
    public class HomeController : Controller
    {
        IServiceFacade _serviceFacade;

        public HomeController()
        {
            _serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();
        }

        private RedirectToRouteResult RedirectIfAuthentificated(string controllerIfUser, string actionIfUser, 
            string controllerIfModerator, string actionIfModerator)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Roles.User))
                    return RedirectToAction("Index", "User", new { id = ((MySocNetPrincipal)User).UserId});
                if (User.IsInRole(Roles.Moderator))
                    return RedirectToAction("Index", "Moderator", new { id = ((MySocNetPrincipal)User).UserId });
            }
            return null;
        }

        // GET: Home
        public ActionResult Index()
        {
            var redirect = RedirectIfAuthentificated("User", "Index", "Moderator", "Index");
            if (redirect != null)
                return redirect;
            
            return RedirectToAction("SignUp", "Registration");
        }

        public async Task<ActionResult> UserPage(int id)
        {
            var redirect = RedirectIfAuthentificated("User", "Index", "Moderator", "Index");
            if (redirect != null)
                return redirect;

            UserDto userDto = await Task.Run(() => _serviceFacade.UserService.Get.ById(id));

            if (userDto == null)
                return HttpNotFound("Такого пользователя не существует");

            return View(userDto.MapToVm());
        }


        //TODO in DAL layer implement Data integrity constraints (using attributes)
        //TODO probably, I'd better get some instances by name (login for User, name for Thread etc) rather than Id to provide good links

        public async Task<ActionResult> Thread(int id)
        {
            ThreadDto threadDto = await Task.Run(() => _serviceFacade.ThreadService.Get.ById(id));

            if (threadDto == null)
                return HttpNotFound("Такого треда не существует");

            return View(threadDto.MapToVm());
        }

        public async Task<ActionResult> Post(int id)
        {
            PostDto postDto = await Task.Run(() => _serviceFacade.PostService.Get.ById(id));

            if (postDto == null)
                return HttpNotFound("Такого поста не существует");

            return View(postDto.MapToVm());
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}