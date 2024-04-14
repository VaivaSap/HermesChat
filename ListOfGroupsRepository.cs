using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using HermesChat_TeamA.Services;


namespace HermesChat_TeamA
{
    public interface IListOfGroupsRepository
    {
        public bool CreateNewGroupChat(string groupName);
        public List<string> GetUsersGroupChatList(string userName);

        public List<string> GetAllActiveChats();
        public bool AddUserToGroupChat(string groupName, string user);
        public bool AddClickerToGroup(string groupName, string user);
        public int UsersCountInGroupChat(string groupName);
        public string RemoveUserFromGroupChat(string groupName, string user);
        public string RemoveGroupFromGroupChatList(string groupName);
    }

    public class ListOfGroupsRepository : IListOfGroupsRepository
    {
        ListOfGroupsRepository _groupsRepository;

        private readonly HermesChatDbContext _context;
        DataAccessService _dataAccessService;
        CurrentUserService _currentUserService;

        public ListOfGroupsRepository(HermesChatDbContext context)
        {
            _context = context;
        }

        public bool CreateNewGroupChat(string groupName)
        {
            groupName = groupName.Trim();

            if (!_context.Conversations.Any(g => g.Name == groupName))
            {
                var newConversation = new Conversation { Name = groupName };

                _context.Conversations.Add(newConversation);

                _context.SaveChanges();

            }

            return false;
        }

        public List<string> GetUsersGroupChatList(string userName)
        {
            var userGroupChats = _context.Conversations
                .Where(c => c.ConversationUser.Any(u => u.User.UserName == userName))
                .Select(c => c.Name)
                .ToList();

            return userGroupChats;
        }

        public List<string> GetAllActiveChats()
        {
            var allActiveChats = _context.Conversations.Select(c => c.Name).ToList();

            return allActiveChats;
        }

        public bool AddUserToGroupChat(string groupName, string userName) // Kai pats prisijungia
                                                                          
        {
            groupName = groupName.Trim();
            var conversation = _context.Conversations.FirstOrDefault(c => c.Name == groupName);
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    Name = groupName,
                    ConversationUser = new List<ConversationUser> { new ConversationUser { User = user } }
                };

                _context.Conversations.Add(conversation);
            }

            else
            {
                conversation.ConversationUser.Add(new ConversationUser { User = user });
            }

            _context.SaveChanges();
            return true;

        }



    public bool AddClickerToGroup(string groupName, string userName)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
        groupName = groupName.Trim();

        if (!_context.Conversations.Any(c => c.ConversationUser.Any(u => u.User.UserName == userName)))
        {
            var conversation = new Conversation
            {

                Name = groupName,
                ConversationUser = new List<ConversationUser> { new ConversationUser { User = user } }
            };
            _context.Conversations.Add(conversation);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public int UsersCountInGroupChat(string groupName)
    {
        var conversation = _context.Conversations.FirstOrDefault(c => c.Name == groupName);
        if (conversation != null)
        {
            return conversation.ConversationUser.Count();
        }
        return 0;
    }

    public string RemoveUserFromGroupChat(string groupName, string userName)
    {
        var conversation = _context.Conversations.FirstOrDefault(c => c.Name == groupName);
        if (conversation != null)
        {
            var user = conversation.ConversationUser.FirstOrDefault(u => u.User.UserName == userName);
            if (user != null)
            {

                conversation.ConversationUser.Remove(user);
                _context.SaveChanges();
                return "The user removed successfully.";
            }

        }

        return "We did not found it.";

    }

    public string RemoveGroupFromGroupChatList(string groupName)
    {
        var conversation = _context.Conversations.FirstOrDefault(c => c.Name == groupName);
        if (conversation != null)
        {
            _context.Conversations.Remove(conversation);
            _context.SaveChanges();
            return "This group chat was closed.";
        }

        return "We haven't found a chat with this title. Please check it.";
    }
}
}
