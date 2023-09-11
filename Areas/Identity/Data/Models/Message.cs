namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageBody { get; set; }
        public string senderId { get; set; }
        public string receiverId { get; set; }
        public DateTime SentDate { get; set; }
        
        //do we need something alse here?
        //conversation class does the job of saving the message to database
    }
}
