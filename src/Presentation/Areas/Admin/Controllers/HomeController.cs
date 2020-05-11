using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factories.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
		IUnitOfWork _unitOfWork;
		public HomeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		[Route("/admin/")]
		public IActionResult Index()
		{
			return View();
		}
	}
}