using HermesChat_TeamA.Models;
using HermesChat_TeamA.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HermesChat_TeamA.Controllers
{
    public class ChatMessagesController : Controller
    {

        private readonly IEmailSender _emailSender;
        private readonly ILogger<ChatMessagesController> _logger;
        private readonly CurrentUserService _currentUserService;
        private readonly DataAccessService _dataAccessService;


        public ChatMessagesController(ILogger<ChatMessagesController> logger, IEmailSender emailSender, CurrentUserService currentUserService, DataAccessService dataAccessService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _currentUserService = currentUserService;
            _dataAccessService = dataAccessService;
        }



        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult GetTopMessagesOfUsers()
        //{
        //    var receiver = ;
        //    int numberOfMessagesDisplayed = 100;
        //    var messages = _dataAccessService.ReturnTopMessagesFromUsers(_currentUserService.GetCurrentUser(User), _currentUserService.GetCurrentUser(User), numberOfMessagesDisplayed);


        //    return Ok(messages);
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
