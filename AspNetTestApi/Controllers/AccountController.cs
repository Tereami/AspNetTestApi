using DomainModel.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetTestApi.ViewModels;

namespace AspNetTestApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            userManager = UserManager;
            signInManager = SignInManager;
        }

        [Authorize]
        public async Task<IActionResult> Users()
        {
            List<User> users = await userManager.Users.ToListAsync();
            List<UserViewModel> usersViewModel = new List<UserViewModel>();
            foreach (User user in users)
            {
                string role = string.Join(',', await userManager.GetRolesAsync(user));
                UserViewModel userViewModel = new UserViewModel {
                    Id = user.Id,
                    Username = user.UserName!,
                    PasswordHash = user.PasswordHash!,
                    FailedAccessCount = user.AccessFailedCount,
                    Role = role 
                };
                usersViewModel.Add(userViewModel);
            }
            return View(usersViewModel);
        }



        public IActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User user = new User
            {
                UserName = model.Username,
            };

            IdentityResult registerResult = await userManager.CreateAsync(user, model.Password);
            if (registerResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Role.Users);
                await signInManager.SignInAsync(user, false); //временный вход без пароля
                return RedirectToAction("Index", "Home");
            }

            foreach (IdentityError error in registerResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }




        public IActionResult Login(string ReturnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool blockOnPasswordFailures = true;
#if DEBUG
            blockOnPasswordFailures = false;
#endif

            var loginResult = await signInManager
                .PasswordSignInAsync(model.Username, model.Password, model.Remember, blockOnPasswordFailures);

            if (loginResult.Succeeded)
            {
                if (model.ReturnUrl is null)
                    model.ReturnUrl = "/";
                return LocalRedirect(model.ReturnUrl);
            }

            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);
        }




        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
