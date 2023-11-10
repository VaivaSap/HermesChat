using HermesChat_TeamA.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace HermesChat_TeamA.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IEmailSender _emailSender;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
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