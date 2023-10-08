using Microsoft.EntityFrameworkCore.Internal;


namespace HermesChat_TeamA
{
    public interface IListOfGroupsRepository
    {
        public bool CreateNewGroupChat(string groupName);
        public string AddUserToGroupChat(string groupName, string user);
        public int UsersCountInGroupChat(string groupName);
        public string RemoveUserFromGroupChat(string groupName, string user);
        public string RemoveGroupFromGroupChatList(string groupName);
    }

    public class ListOfGroupsRepository : IListOfGroupsRepository
    {
        ListOfGroupsRepository _groupsRepository;


        private readonly Dictionary<string, List<string>> groupChats = new Dictionary<string, List<string>>();

        public bool CreateNewGroupChat(string groupName)
        {
            if (!groupChats.ContainsKey(groupName))


            {
                groupChats.Add(groupName, new List<string>());

                return true;
            }

            return false;
        }

        public string AddUserToGroupChat(string groupName, string user)
        {
            if (!groupChats.ContainsKey(groupName))
            {
                groupChats.Add(groupName, new List<string> { user });

                return "A new group successfully created.";

            }

            groupChats[groupName].Add(user);

            return "A new user successfully added.";
        }

        public int UsersCountInGroupChat(string groupName)
        {
            if(groupChats.ContainsKey(groupName))
            {
                var userCountInGroup = groupChats[groupName].Count();
                return userCountInGroup;
            }

            return 0;
        }

            public string RemoveUserFromGroupChat(string groupName, string user)
            {
                if (groupChats.ContainsKey(groupName))
                {
                    groupChats[groupName].Remove(user);

                    return "The user removed successfully.";
                }

                return "We did not found it.";

            }

            public string RemoveGroupFromGroupChatList(string groupName)
            {
                if (groupChats.ContainsKey(groupName))
                {
                    groupChats.Remove(groupName);

                    return "This group chat was closed.";
                }

                return "We haven't found a chat with this title. Please check it.";
            }
        }
    }






