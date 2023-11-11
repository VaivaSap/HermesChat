namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
    public class UserProfilePicture
    {
        public int Id { get; set; }
        public string ProfilePicturePath { get; set; }

       public string UserId { get; set;}

        public User User { get; set; }

    }

}
