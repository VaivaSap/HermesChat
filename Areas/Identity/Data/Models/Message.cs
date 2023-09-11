namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageBody { get; set; }
        public string senderId { get; set; }
        public string receiverId { get; set; }
        public DateTime SentDate { get; set; }
    }
}
