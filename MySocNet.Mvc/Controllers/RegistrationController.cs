using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MySocNet.Bll.Dto;
using MySocNet.Bll.Exceptions;
using MySocNet.Bll.Services.Abstract;
using MySocNet.Mvc.Models;
using MySocNet.Mvc.Models.Common;
using MySocNet.Mvc.Models.Utils;
using MySocNet.Mvc.Providers;
using Ninject;
using AutoMapper;

namespace MySocNet.Mvc.Controllers
{
    public class RegistrationController : Controller
    {
        IServiceFacade _serviceFacade;

        public RegistrationController()
        {
            _serviceFacade = MvcApplication.NinjectKernel.Get<IServiceFacade>();
        }
        /// <summary>
        /// Creates FormsAuthenticationTicket and HttpCookie authCookie. Adds them to HttpResponse
        /// </summary>
        /// <param name="user"></param>
        private void SignIn(MySocNetMembershipUser user)
        {
            MembershipUserSerializeModel serializeModel = new MembershipUserSerializeModel()
            {
                UserId = user.UserId,
                Login = user.Login,
                Roles = user.Roles
            };

            string userData = System.Web.Helpers.Json.Encode(serializeModel);

            FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket
                (1, user.Login, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData);

            string enTicket = FormsAuthentication.Encrypt(authenticationTicket);
            HttpCookie authCookie = new HttpCookie(Cookies.AuthentificationCookieName, enTicket);
            Response.Cookies.Add(authCookie);
        }

        [AllowAnonymous]
        [HttpPost]
        //войти в систему
        public async Task<ActionResult> SignIn(string login, string password)
        {
            var u = Request.RequestContext.HttpContext.User;
            var u2 = User;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return Json("Таких логина/пароля не может быть", JsonRequestBehavior.AllowGet);

            bool isLoginValid, isPasswordValid;
            UserDto userDto = new UserDto { Login = login, Passwod = password };
            _serviceFacade.UserService.CheckLoginAndPassword(userDto, out isLoginValid, out isPasswordValid);

            if(!isLoginValid)
                return Json("Такого пользователя не существует", JsonRequestBehavior.AllowGet);

            else if (!isPasswordValid)
                return Json("Неверный пароль", JsonRequestBehavior.AllowGet);

            var user = await Task.Run(() => (MySocNetMembershipUser)Membership.GetUser(login, false));

            SignIn(user);

            //redirect logic

            if (user.Roles.Contains(Providers.Roles.User))
                return JavaScript($"window.location = '{Url.Action("Index", "User", new { id = user.UserId })}'");

            else //Moderator
                return JavaScript($"window.location = '{Url.Action("Index", "Moderator", new { id = user.UserId })}'");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignUp() //registration form
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Providers.Roles.User))
                    return RedirectToAction("Index", "User", new { id = ((MySocNetPrincipal)User).UserId});

                if (User.IsInRole(Providers.Roles.Moderator))
                    return RedirectToAction("Index", "Moderator", new { id = ((MySocNetPrincipal)User).UserId });
            }

            return View(new RegistrationVm());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SignUp(RegistrationVm registrationVm)
        {
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                //Login Verification
                var userDto = await Task.Run(() => _serviceFacade.UserService.Get.ByLogin(registrationVm.Login));
                if (userDto != null)
                {
                    ModelState.AddModelError("Warning key", "This login already exists");
                }

                //Create user
                UserDto newUser = Mapper.Map<RegistrationVm, UserDto>(registrationVm);
                _serviceFacade.UserService.CreateAccount(newUser);

                MySocNetMembershipUser membershipUser = new MySocNetMembershipUser(newUser);
                SignIn(membershipUser);

                //redirecting
                return JavaScript($"window.location = '{Url.Action("Settings", "User", new { id = membershipUser.UserId })}'");
            }
            else
                messageRegistration = "Не все поля заполнены корректно!";

            ViewBag.RegistrationErrorMessage = messageRegistration;

            return View(registrationVm);
        }

        [HttpPost]
        [MSNAuthorize]
        public async Task<ActionResult> SignOut()
        {
            HttpCookie httpCookie = new HttpCookie(Cookies.AuthentificationCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(httpCookie);

            FormsAuthentication.SignOut();
            
            return RedirectToAction("About", "Home");
        }
    }
}