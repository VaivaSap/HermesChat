using System.ComponentModel.DataAnnotations.Schema;

namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
    public class ConversationUser
    {
        public ConversationUser() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public User User { get; set; }
        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }
    }
}
