using System.ComponentModel.DataAnnotations.Schema;

namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
    public class GroupConversation : Conversation
    {
        public GroupConversation() { }
        //public GroupConversation(List<User> users) { Users = users; }
        //public GroupConversation(string name) { Name = name; }

       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       // public int Id { get; set; }

        //public List<User> Users { get; } = new();
        //public string? Name { get; set; }
    }
}
