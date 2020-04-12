using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;
using RegisterUserViewModel = WebStore.Domain.ViewModels.Identity.RegisterUserViewModel;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UseManager;
        private readonly SignInManager<User> _SignInManager;

        public AccountController(UserManager<User> UseManager, SignInManager<User> SignInManager)
        {
            _UseManager = UseManager;
            _SignInManager = SignInManager;
        }

        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model, [FromServices] IMapper mapper)
        {
            if (!ModelState.IsValid)
                return View(Model);
            //var user = new User
            //{
            //    UserName = Model.UserName
            //};
            var user = mapper.Map<User>(Model);

            var register_result = await _UseManager.CreateAsync(user, Model.Password);
            if (register_result.Succeeded)
            {
                await _UseManager.AddToRoleAsync(user, Role.User);

                await _SignInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }




            foreach (var error in register_result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(Model);
        }

        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                false);
            if (login_result.Succeeded)
            {
                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль");

            return View(Model);
        }

        public async Task<IActionResult> Logout()
        {
            _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
