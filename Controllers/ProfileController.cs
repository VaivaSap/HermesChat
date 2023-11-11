using HermesChat_TeamA.Areas.Identity.Data.Models;
using HermesChat_TeamA.Migrations;
using HermesChat_TeamA.Models;
using HermesChat_TeamA.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Security.Claims;

namespace HermesChat_TeamA.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IEmailSender _emailSender;
        private readonly ILogger<ProfileController> _logger;
        private readonly ImagesHandling _imagesHandling;
        private readonly CurrentUserService _currentUserService;

        public ProfileController(ILogger<ProfileController> logger, IEmailSender emailSender, ImagesHandling imagesHandling, CurrentUserService currentUserService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _imagesHandling = imagesHandling;
            _currentUserService = currentUserService;
        }

      
        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadPicture()
        {
            string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            Directory.CreateDirectory(uploadDirectory);

            var userProfilePicture = Request.Form.Files[0];

            if (userProfilePicture != null && userProfilePicture.Length > 0)
            {
              
                string filePath = Path.Combine(uploadDirectory, userProfilePicture.FileName);

               
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    userProfilePicture.CopyTo(stream);
                }

              
                _imagesHandling.UploadProfilePicture(filePath, _currentUserService.GetCurrentUser(User));

            }

            return RedirectToAction("Profile");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}