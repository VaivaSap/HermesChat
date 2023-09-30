using HermesChat_TeamA.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HermesChat_TeamA.Controllers
{
	public class HomeController : Controller
	{
		
		private readonly IEmailSender _emailSender;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
		{
			_logger = logger;
			_emailSender = emailSender;
		}

		/*public async Task<IActionResult> Index()
		{ 
			var receiver = "jurgita.pupalaigiene@gmail.com";
			var subject = "agsahgahgdfhahdfajfjajfghasgfagfajfasgh";
			var message = "Hellooooo";

			await _emailSender.SendEmailAsync(receiver, subject, message);

			return View();
		}*/
		
		
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