using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;
using RegisterUserViewModel = WebStore.Domain.ViewModels.Identity.RegisterUserViewModel;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger _logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _logger = logger;
        }

        public async Task<IActionResult> IsNameFree(string UserName)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user != null)
                return Json("Пользователь уже существует");
            return Json("true");
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

            using (_logger.BeginScope($"Создан новый пользователь {Model.UserName}"))
            {
                var register_result = await _UserManager.CreateAsync(user, Model.Password);
                if (register_result.Succeeded)
                {
                    _logger.BeginScope($"Пользователь {Model.UserName} успешно зарегестрирован");
                    await _UserManager.AddToRoleAsync(user, Role.User);

                    _logger.BeginScope($"Пользователю {Model.UserName} добавлена роль {Role.User}");
                    await _SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in register_result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                _logger.LogWarning($"Ошибки при объявлении пльзователя {Model.UserName} :{Environment.NewLine}{string.Join(", ", (from p in register_result.Errors select $"{p.Code}-{p.Description}"))}");
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
                _logger.LogInformation($"Пользователь {Model.UserName} вошёл в систему");
                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль");
            _logger.LogWarning($"Ошибка входа в систему пользователя {Model.UserName}");

            return View(Model);
        }

        public async Task<IActionResult> Logout()
        {
            var _name = User.Identity.Name;
            _SignInManager.SignOutAsync();
            _logger.LogInformation($"Пользователь {_name} вошёл в систему");

            return RedirectToAction("Index", "Home");
        }
    }
}
