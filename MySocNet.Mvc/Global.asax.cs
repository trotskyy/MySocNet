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
        public static IKernel NinjectKernel { get; private set; }

        private void InitializeDIContainer()
        {
            NinjectKernel = new StandardKernel();

            NinjectKernel.Bind<IServiceFacade>().To<ServiceFacade>();

            NinjectKernel.Bind<IUserService>().To<UserService>();
            NinjectKernel.Bind<IMessageService>().To<MessageService>();
            NinjectKernel.Bind<INotificationService>().To<NotificationService>();
            NinjectKernel.Bind<IPostService>().To<PostService>();
            NinjectKernel.Bind<IThreadService>().To<ThreadService>();

            NinjectKernel.Bind<ISecurityProvider>().To<SHA256SecurityProvider>();

            NinjectKernel.Bind<IUnitOfWorkFactory>().To<EfUnitOfWorkFactory>();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            InitializeDIContainer();
        }
    }
}
