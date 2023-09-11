namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
	public class GroupConversation : Conversation
	{
		private string _name;

		public void ChangeName(string name) 
		{ _name = name; }
	}
}
