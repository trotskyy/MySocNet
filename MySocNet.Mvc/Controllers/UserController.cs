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

namespace MySocNet.Mvc.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {
            
        }

        private Task<UserDto> GetUser(Func<UserDto> action)
        {
            return Task.Run(() => action.Invoke());
        }

        // GET: User
        public async Task<ActionResult> Index(int id)
        {
            IServiceFacade serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();

            UserDto user = await Task.Run(() => serviceFacade.UserService.Get.ById(id));

            return View(user);
        }
    }
}