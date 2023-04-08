using DomainModel;
using DomainModel.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetTestApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountApiController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountApiController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            string username = model.Username;
            string password = model.Password;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) )
                return BadRequest("Invalid username or password");

            bool blockOnPasswordFailures = true;
#if DEBUG
            blockOnPasswordFailures = false;
#endif

            var loginResult = await signInManager
                .PasswordSignInAsync(username, password, true, blockOnPasswordFailures);

            if (loginResult.Succeeded)
            {
                return Ok("Login success");
            }
            else
            {
                return BadRequest("Invalid login");
            }
        }


        //[Authorize]
        [HttpGet]
        public IActionResult ReadTextAuth()
        {
            if (!User.Identity!.IsAuthenticated)
                return BadRequest("No login");
            string text = "Hello authorized user from AccountApiController";
            return Content(text);
        }

    }
}
