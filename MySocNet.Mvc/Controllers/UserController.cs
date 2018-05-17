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

        // GET: User
        public async Task<ActionResult> Index(int id)
        {
            //Task t = new Task<UserDto>(() => )

            return View();
        }
    }
}