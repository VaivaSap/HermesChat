using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
    public class ConnectionLog
    {
        public ConnectionLog() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Connection { get; set; }
        public User User { get; set; }
        public string ConnectedUserId { get; set; }
    }
}
