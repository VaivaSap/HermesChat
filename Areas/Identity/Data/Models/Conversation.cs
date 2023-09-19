namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
    public class Conversation
    {
        protected List<User> _users = new List<User>();
        protected List<Message> _messages = new List<Message>();
        public void AddMessagetoConversation (string messageBody, User sender, User receiver)
        {
            _messages.Add(new Message { MessageBody = messageBody, senderId = sender.Id, 
                receiverId = receiver.Id, SentDate = DateTime.Now});
            //should make an auto generated Id, 
            //also should either add code to save message to database,
            //or reference a function from service class that would do it
        }
    }
}
