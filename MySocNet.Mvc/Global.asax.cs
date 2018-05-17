using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Services;
using Ninject;


namespace MySocNet.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private void InitializeDIContainer()
        {
            IKernel ninjectKernel = new StandardKernel();

            ninjectKernel.Bind<IServiceFacade>().To<ServiceFacade>();

            ninjectKernel.Bind<IUserService>().To<UserService>();
            ninjectKernel.Bind<IMessageService>().To<MessageService>();
            ninjectKernel.Bind<INotificationService>().To<NotificationService>();
            ninjectKernel.Bind<IPostService>().To<PostService>();
            ninjectKernel.Bind<IThreadService>().To<ThreadService>();

            ninjectKernel.Bind<ISecurityProvider>().To<SHA256SecurityProvider>();

            ninjectKernel.Bind<IUnitOfWorkFactory>().To<EfUnitOfWorkFactory>();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
