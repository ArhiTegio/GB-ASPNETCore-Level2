using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => User.Identity.IsAuthenticated
        ? View("UserInfo") :
            View();

        //public IViewComponentResult Invoke() => User.Identity.IsAuthenticated
        //    ? User.IsInRole(Role.Administrator)
        //        ? View("AdminInfo")
        //        : View("UserInfo")
        //    : View();
    }
}
