using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Services.Abstract
{
    /// <summary>
    /// Encapsulates all services together
    /// </summary>
    public interface IServiceFacade
    {
        IUserService UserService { get; }
        IMessageService MessageService { get; }
        INotificationService NotificationService { get; }
        IPostService PostService { get; }
        IThreadService ThreadService { get; }
    }
}
