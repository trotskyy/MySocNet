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
    [MSNAuthorize(Roles = Roles.User)]
    public class UserController : Controller
    {
        IServiceFacade _serviceFacade;

        public UserController()
        {
            _serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();
        }

        // UserPage -> feed
        public async Task<ActionResult> Index()
        {
            UserDto user = new UserDto(((MySocNetPrincipal)User).UserId);

            List<PostDto> feed = await Task.Run(() => _serviceFacade.PostService.Get.TopLatestFeedPosts(user, top: 10, withAuthors: true));

            return View(feed.MapToVm());
        }

        private class UserEqualityComparer : IEqualityComparer<UserDto>
        {
            public bool Equals(UserDto x, UserDto y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(UserDto obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        public async Task<ActionResult> Friends(string specificly)
        {
            if (specificly == null)
                specificly = "";

            int id = ((MySocNetPrincipal)User).UserId;

            Func<List<UserDto>> selectFunc;

            ViewBag.IsSubscribers = false;
            ViewBag.IsSubscribtions = false;
            ViewBag.IsFriends = false;

            switch (specificly)
            {
                case "Subscribers":
                    selectFunc = () => _serviceFacade.UserService.Get.AllSubscribersOf(new UserDto(id))
                                            .Except(_serviceFacade.UserService.Get.AllFriendsOf(new UserDto(id)), new UserEqualityComparer()).ToList();
                    ViewBag.IsSubscribers = true;
                    break;
                case "Subscribtions":
                    selectFunc = () => _serviceFacade.UserService.Get.AllSubscriptionsOf(new UserDto(id))
                                            .Except(_serviceFacade.UserService.Get.AllFriendsOf(new UserDto(id)), new UserEqualityComparer()).ToList();
                    ViewBag.IsSubscribtions = true;
                    break;
                default:
                    selectFunc = () => _serviceFacade.UserService.Get.AllFriendsOf(new UserDto(id));
                    ViewBag.IsFriends = true;
                    break;
            }

            List<UserDto> result = await Task.Run(selectFunc);

            List<UserVm> resultVm = Mapper.Map<List<UserDto>, List<UserVm>>(result);

            return View(resultVm);
        }

        public async Task<ActionResult> Messages() //dialogs shown
        {
            int id = ((MySocNetPrincipal)User).UserId;
            List<DialogDto> dialogs = await Task.Run(() => _serviceFacade.MessageService.Get.TopLatestDialogs(new UserDto(id), 10, true));

            List<MessageVm> messageVms = new List<MessageVm>();
            foreach (var dialog in dialogs)
                messageVms.Add(dialog.Messages[0].MapToVm());

            return View(messageVms);
        }

        public async Task<ActionResult> Dialog(MessageVm lastMessage)
        {
            List<MessageDto> dialogMessages = 
                await Task.Run(() => _serviceFacade.MessageService.Get.TopLatestMessagesOfDialog(new DialogDto(lastMessage.MapToDto()), 10, true));

            return View(dialogMessages.MapToVm());
        }

        public async Task<ActionResult> Notifications()
        {
            int id = ((MySocNetPrincipal)User).UserId;
            List<NotificationDto> notifications = await Task.Run(() => _serviceFacade.NotificationService.GetNotificationsByReceiver(new UserDto(id)));

            return View(notifications.MapToVm());
        }

        //стена. личный тред юзера
        public async Task<ActionResult> UserPage()
        {
            var user = await Task.Run(() => _serviceFacade.UserService.Get.ById(((MySocNetPrincipal)User).UserId));
            var wall = await Task.Run(() => _serviceFacade.PostService.Get.WallPosts(user.Id, top: 10, withAuthors: true));

            UserPageVm userPageVm = new UserPageVm
            {
                AboutSelf = user.AboutSelf,
                CityOfBirth = user.CityOfBirth,
                CurrentCity = user.CurrentCity,
                CurrentState = user.CurrentState,
                DateOfBirth = user.DateOfBirth,
                FirstName = user.FirstName,
                IsMale = user.IsMale,
                LastName = user.LastName,
                StateOfBirth = user.StateOfBirth,
                Wall = new List<KeyValuePair<PostVm, string>>()
            };
            foreach (var p in wall)
            {
                userPageVm.Wall.Add(new KeyValuePair<PostVm, string>(p.Key.MapToVm(), p.Value));
            }

            return View(userPageVm);
        }
    }
}