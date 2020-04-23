using Factories.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Cores;

namespace Presentation.Controllers
{
	public class LoginController : ControllerBaseTemplate
	{
		IUnitOfWork _unitOfWork;
		public LoginController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		
		public IActionResult Index()
		{
			return View();
		}
	}
}
