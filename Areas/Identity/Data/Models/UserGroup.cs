namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
	public class UserGroup
    {
        //is needed to limit number of people user can message,
        //wouldn't make sense to be able to message all users from database
        //since splitting users to groups isn't in the tasks, could be done later, if we have time
        public string Id { get; set; }
        public string Name { get; set; }
        private List<User> _users = new List<User>();

        public void AddUsers(User user)
        {
            _users.Add(user);
        }
        public void RemoveUser(User user)
        { _users.Remove(user); }
    }
}
