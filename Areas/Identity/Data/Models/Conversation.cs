using System.ComponentModel.DataAnnotations.Schema;

namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
    public class Conversation
    {
        public Conversation() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;

        public List<ConversationUser> ConversationUser { get; set; } = new List<ConversationUser>();

    }
}
