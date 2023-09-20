namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
	public class GroupConversation : Conversation
	{
		private string _name;

		public void ChangeName(string name) 
		{ _name = name; }

		public void AddUsers (User user)
		{
			_users.Add(user);
			//will need improvement
			//also needs to be saved to database
		}
	}
}
