using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using HermesChat_TeamA.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IO;

namespace HermesChat_TeamA.Services
{
    public class ImagesHandling
    {
        private readonly HermesChatDbContext _context;
        public ImagesHandling(HermesChatDbContext context)
        {
            _context = context;
        }


        public void UploadProfilePicture(string profilePicturePath, User user)
        {
            UserProfilePicture userProfilePicture = _context.UserProfilePictures.FirstOrDefault(a => a.UserId == user.Id);


            UserProfilePicture newUserProfilePicture = new UserProfilePicture
            {
                User = user,
                ProfilePicturePath = profilePicturePath,
            };

            if (userProfilePicture != null)
            {
                userProfilePicture.ProfilePicturePath = profilePicturePath;
                userProfilePicture.User = user;

                _context.Update(userProfilePicture);

            }

            else
            {
                _context.UserProfilePictures.Add(newUserProfilePicture);

            }

            _context.SaveChanges();
        }
    }
}