using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestTask_BO.Models;

namespace TestTask_BO.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly EmployeeDbContext _employeeDbContext;
		public HomeController(ILogger<HomeController> logger, EmployeeDbContext employeeDbContext)
		{
			_logger = logger;
			_employeeDbContext = employeeDbContext;
		}

		public IActionResult Index()
		{
			
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}