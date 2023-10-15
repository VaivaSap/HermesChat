using System.ComponentModel.DataAnnotations.Schema;

namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string MessageBody { get; set; }

        public string TimeSent { get; set; }
        public Conversation Conversation { get; set; }
        
    }
}
