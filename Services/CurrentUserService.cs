using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using System.Security.Claims;

namespace HermesChat_TeamA.Services
{
   
    public interface ICurrentUserService
    {
        public User GetCurrentUser(ClaimsPrincipal claims);
    }
    
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HermesChatDbContext _context;

        public CurrentUserService(HermesChatDbContext context)
        {
            _context = context;
        }
        public User GetCurrentUser(ClaimsPrincipal claims)
        {
            var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Find(userId);
            return user;
        }
    }
}
