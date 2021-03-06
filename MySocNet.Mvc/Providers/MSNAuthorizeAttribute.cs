﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySocNet.Mvc.Providers
{
    /// <summary>
    /// My Soc Net AuthorizeAttribute
    /// </summary>
    public class MSNAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual MySocNetPrincipal CurrentUser => HttpContext.Current.User as MySocNetPrincipal;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (CurrentUser == null)
                return false;
            if (Roles == "")
                return true;
            return CurrentUser.IsInRole(Roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            if (CurrentUser == null)
            {
                routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                    (new
                    {
                        //TODO Redirect logic here
                        controller = "Home",
                        action = "Index",
                    }
                    ));
            }
            else
            {
                routeData = new RedirectToRouteResult
                (new System.Web.Routing.RouteValueDictionary
                 (new
                 {
                     //TODO ????
                     //controller = "Error",
                     //action = "AccessDenied"
                     controller = "Home",
                     action = "Index"
                 }
                 ));
            }

            filterContext.Result = routeData;
        }
    }
}