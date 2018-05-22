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
    public class MessageController : Controller
    {
        IServiceFacade _serviceFacade;

        public MessageController()
        {
            _serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();
        }

        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string UnreadMessagesCount(int id)
        {
            int res = _serviceFacade.MessageService.Get.UnreadMessagesCountTo(new UserDto(id));
            if (res == 0)
                return "";
            return res.ToString();
        }

        [HttpPost]
        public ActionResult Write(int toId, string text)
        {
            _serviceFacade.MessageService.WriteMessage(new MessageDto
            {
                FromId = ((MySocNetPrincipal)User).UserId,
                Sent = DateTime.Now,
                ToId = toId,
                Text = text
            });
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult DeleteMessage(int id)
        {
            _serviceFacade.MessageService.DeleteMessage(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}