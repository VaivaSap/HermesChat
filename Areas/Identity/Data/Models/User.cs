using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HermesChat_TeamA.Areas.Identity.Data.Models;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{


    [Column(TypeName = "int")]
    public int UserId { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    public List<ConversationUser> ConversationUser { get; set; } = new List<ConversationUser>();
    public UserProfilePicture UserProfilePicture { get; set; }
}

