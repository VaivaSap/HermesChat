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


        public void UploadProfilePicture(int id, string profilePicturePath, User user)
        {
            UserProfilePicture userProfilePicture = _context.UserProfilePictures.Find(id);

          //  kaip priskirti ką?

            UserProfilePicture newUserProfilePicture = new UserProfilePicture {
                Id = id,
                User = user,
                ProfilePicturePath = profilePicturePath,
            };

            if (userProfilePicture != null) 
            {
                userProfilePicture.ProfilePicturePath = profilePicturePath;

                _context.Update(userProfilePicture);

                _context.SaveChanges();
            }

           else 
           { 
                _context.UserProfilePictures.Add(newUserProfilePicture);

                _context.SaveChanges();
           }
        }
    }
}