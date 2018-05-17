using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Dal.Entities;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Dto.Utils;

namespace MySocNet.Bll.Services
{
    /// <summary>
    /// Encapsulates all services
    /// </summary>
    public class ServiceFacade : IServiceFacade
    {
        readonly IUserService _userService;
        readonly IMessageService _messageService;
        readonly INotificationService _notificationService;
        readonly IPostService _postService;
        readonly IThreadService _threadService;

        public ServiceFacade(IUserService userService, IMessageService messageService, INotificationService notificationService, IPostService postService, IThreadService threadService)
        {
            AutomapperInitializer.InitAutoMapper();

            _userService = userService;
            _messageService = messageService;
            _notificationService = notificationService;
            _postService = postService;
            _threadService = threadService;
        }

        public IUserService UserService => _userService;

        public IMessageService MessageService => _messageService;

        public INotificationService NotificationService => _notificationService;

        public IPostService PostService => _postService;

        public IThreadService ThreadService => _threadService;
    }
}
