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

namespace MySocNet.Mvc.Controllers
{
    [MSNAuthorize(Roles = Roles.User)]
    public class UserController : Controller
    {
        IServiceFacade _serviceFacade;

        public UserController()
        {
            _serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();
        }

        // UserPage -> feed
        public async Task<ActionResult> Index(int id)
        {
            UserDto user = await Task.Run(() => _serviceFacade.UserService.Get.ById(id));

            UserVm userVm = Mapper.Map<UserDto, UserVm>(user);

            return View(userVm);
        }

        public async Task<ActionResult> Friends(int id)
        {
            List<UserDto> friends = await Task.Run(() => _serviceFacade.UserService.Get.AllFriendsOf(new UserDto(id)));

            List<UserVm> friendsVm = Mapper.Map<List<UserDto>, List<UserVm>>(friends);

            return View(friendsVm);
        }

        public async Task<ActionResult> Messages(int id) //dialogs shown
        {
            List<DialogDto> dialogs = await Task.Run(() => _serviceFacade.MessageService.Get.TopLatestDialogs(new UserDto(id), 10));

            List<MessageVm> messageVms = new List<MessageVm>();
            foreach (var dialog in dialogs)
                messageVms.Add(dialog.Messages[0].MapToVm());

            return View(messageVms);
        }

        public async Task<ActionResult> Dialog(MessageVm lastMessage)
        {
            List<MessageDto> dialogMessages = 
                await Task.Run(() => _serviceFacade.MessageService.Get.TopLatestMessagesOfDialog(new DialogDto(lastMessage.MapToDto()), 10));

            return View(dialogMessages.MapToVm());
        }

        public async Task<ActionResult> Notifications(int id)
        {
            List<NotificationDto> notifications = await Task.Run(() => _serviceFacade.NotificationService.GetNotificationsByReceiver(new UserDto(id)));

            return View(notifications.MapToVm());
        }

        public async Task<ActionResult> UserPage(int id)
        {
            UserDto user = await Task.Run(() => _serviceFacade.UserService.Get.ById(id));

            return View(user.MapToVm());
        }
    }
}