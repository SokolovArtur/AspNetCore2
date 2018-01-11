using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tochka.Areas.Accounts.Data;
using Tochka.Areas.Accounts.Models.AuthViewModels;

namespace Tochka.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.UserName,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: true);
                if (result.IsLockedOut)
                {
                    return RedirectToAction(nameof(Lockout));
                }
                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return NotFound();
            }
            
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return NotFound();
            }
            
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
 
            return View(new ResetPasswordViewModel { Id = userId, Code = code });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string userId, string code, [Bind("Id, Code, Password, ConfirmPassword")] ResetPasswordViewModel model)
        {
            if ((userId == null || userId != model.Id)
                || (code == null || code != model.Code))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                var r = ModelState;

                return View(model);
            }
            
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return BadRequest();
            }
            
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
