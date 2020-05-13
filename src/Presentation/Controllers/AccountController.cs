using Factories.Services;
using Infrastructure.Emails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
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
		private IEmailService _emailService;
		private ITemplateService _templateService;
		private readonly IWebHostEnvironment _env;

		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmailService emailService, ITemplateService templateService, IWebHostEnvironment env)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_emailService = emailService;
			_templateService = templateService;
			_env = env;
		}

		[HttpGet]
		[Route("login")]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("login")]
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

		[HttpGet]
		[Route("forgot-password")]
		public IActionResult ForgotPassword()
		{
			return View("~/Views/Partials/_ForgotPassword.cshtml");
		}

		[HttpPost]
		[Route("forgot-password")]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user != null)
			{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var callBack = Url.Action("ResetPassword", 
					"Account",new { token, email = user.Email }, Request.Scheme);
				var loginLink = Url.Action("Login",
					"Account", null, Request.Scheme);
				var homeLink = Url.Action("Index",
					"Home", null, Request.Scheme);

				string webRootPath = _env.WebRootPath;
				var bodyBuilder = new BodyBuilder();
				var template = _templateService.Load(webRootPath + "/Templates/Email/ForgotPassword.html");
				var data = new
				{
					Email = email,
					CallBack = callBack,
					LoginLink = loginLink,
					HomeLink = homeLink

				};
				bodyBuilder.HtmlBody = _templateService.Parse(template, data);
				await _emailService.SendEmailAsync(email, email, "Reset Password", bodyBuilder);
				return StatusCode(201, "Success");
			}
			return StatusCode(500, "Error");
		}

		[HttpGet]
		[Route("reset-password")]
		public IActionResult ResetPassword(string token, string email)
		{
			var model = new ResetPasswordModel { Token = token, Email = email };
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("reset-password")]
		public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
		{
			if (!ModelState.IsValid)
				return View(resetPasswordModel);

			var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
			if (user != null)
			{
				var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
				if (!resetPassResult.Succeeded)
				{
					foreach (var error in resetPassResult.Errors)
					{
						ModelState.TryAddModelError(error.Code, error.Description);
					}

					return View();
				}

				return RedirectToAction(nameof(ResetPasswordConfirmation));
			}
			return View(resetPasswordModel);
		}
		[Route("reset-password-confirmation")]
		public IActionResult ResetPasswordConfirmation()
		{
			return View();
		}
	}
}
