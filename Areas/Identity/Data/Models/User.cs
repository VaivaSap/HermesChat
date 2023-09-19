using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HermesChat_TeamA.Areas.Identity.Data.Models;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    //public void SendMessage()
    //{will create message;
    //senders can be added automatically from participants in current conversation;
    //will be stored in database, either through this method, or through a separate method in service class}
}

