using Factories.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Cores;

namespace Presentation.Controllers
{
	public class HomeController : ControllerBaseTemplate
	{
		IUnitOfWork _unitOfWork;
		public HomeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		[Route("/")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
