using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Mvc.Models.Common;
using MySocNet.Mvc.Models.Utils;
using Ninject;

namespace MySocNet.Mvc.Controllers
{
    public class ThreadController : Controller
    {
        IServiceFacade _serviceFacade;

        public ThreadController()
        {
            _serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();
        }

        // GET: Thread
        public async Task<ActionResult> Index(int id)
        {
            ThreadDto threadDto = await Task.Run(() => _serviceFacade.ThreadService.Get.ById(id));

            return View(threadDto.MapToVm());
        }

        public async Task<ActionResult> CreateNewThread()
        {
            //TODO Create appropriate VM and implement this async Task<async Task<ActionResult>>
            throw new NotImplementedException();
        }
    }
}