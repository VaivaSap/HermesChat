namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
	public class PrivateConversation : Conversation
	{
		public PrivateConversation(User user1, User user2) 
		{
			_users.Add(user1);
			_users.Add(user2);
		}
	}
}
