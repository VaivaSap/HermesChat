namespace HermesChat_TeamA.Areas.Identity.Data.Models
{
	public class UserGroup
    {
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
