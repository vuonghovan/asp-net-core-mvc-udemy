using Factories.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Identity;
using Models.ViewModels.Account;
using Presentation.Cores;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	public class AccountController : ControllerBaseTemplate
	{
		private SignInManager<AppUser> _signInManager;
		private UserManager<AppUser> _userManager;

		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("[action]")]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("[action]")]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null && user.IsDisable == 0)
				{
					var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
					if (result.Succeeded)
					{
						if (user.IsAdmin == 1)
						{
							return RedirectToAction("Index", "Home", new { Area = "Admin" });
						}
						if (user.IsAdmin == 0)
						{
							return RedirectToAction("Index", "Home");
						}
					}
					else
					{
						ModelState.AddModelError(nameof(model.Password), "Mật khẩu không đúng");
					}
				}
				else
				{
					ModelState.AddModelError("", "Tài khoản không tồn tại hoặc đã bị khóa");
				}
			}
			return View(model);
		}
	}
}
