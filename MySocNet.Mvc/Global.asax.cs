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
using MySocNet.Bll.Dto.Utils;
using Ninject;
using AutoMapper;
using MySocNet.Mvc.Models.Common;
using System.Web.Security;
using MySocNet.Mvc.Providers;
using System.Web.Helpers;
using MySocNet.Mvc.Models;

namespace MySocNet.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IKernel NinjectKernel { get; private set; }

        //from https://www.c-sharpcorner.com/article/custom-authentication-with-asp-net-mvc/
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[Cookies.AuthentificationCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = Json.Decode<MembershipUserSerializeModel>(authTicket.UserData);
                //var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                MySocNetPrincipal principal = new MySocNetPrincipal(authTicket.Name);

                principal.UserId = serializeModel.UserId;
                principal.Login = serializeModel.Login;
                principal.Roles = serializeModel.Roles.ToArray();
                //principal.UserId = serializeModel.UserId;
                //principal.FirstName = serializeModel.FirstName;
                //principal.LastName = serializeModel.LastName;
                //principal.Roles = serializeModel.RoleName.ToArray<string>();

                HttpContext.Current.User = principal;
            }

        }

        private void InitializeAutomapper()
        {
            AutomapperInitializer.InitAutoMapper(cfg =>
            {
                cfg.CreateMap<UserDto, UserVm>();
                cfg.CreateMap<MessageDto, MessageVm>();
                cfg.CreateMap<NotificationDto, NotificationVm>();
                cfg.CreateMap<PostDto, PostVm>();
                cfg.CreateMap<ThreadFilterDto, ThreadFilterVm>();
                cfg.CreateMap<ThreadDto, ThreadVm>();
                cfg.CreateMap<UserFilterDto, UserFilterVm>();
                cfg.CreateMap<UserDto, RegistrationVm>()
                    .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore());
            });
        }

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
            InitializeAutomapper();
        }
    }
}
