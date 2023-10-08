using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace HermesChat_TeamA.Hubs;

public class SyncHub : Hub
{

    private readonly IListOfGroupsRepository _groupsRepository;

    public SyncHub(IListOfGroupsRepository groupsRepository)
    {
        _groupsRepository = groupsRepository;
    }

    [Authorize]
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message, DateTime.Now);
    }



    public async Task SendToParticularUser(string user, string receiverConnectionId, string message)
    {
        await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message);
    }

    //All group-chat-related

    public bool CreateGroupChat(string groupName)
    {
        return _groupsRepository.CreateNewGroupChat(groupName);

    }


    public async Task JoinGroupChat(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        _groupsRepository.AddUserToGroupChat(groupName, Context.User.Identity.Name);

        await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.User.Identity.Name} has joined the group {groupName}.");
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        _groupsRepository.RemoveUserFromGroupChat(groupName, Context.User.Identity.Name);

        await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.User.Identity.Name} has left the group {groupName}.");

        if (_groupsRepository.UsersCountInGroupChat(groupName) == 0 )
        {
            _groupsRepository.RemoveGroupFromGroupChatList(groupName);
        }
    }

  //  public string DeleteGroupChat(string groupName) {}
   

        public async Task SendMessageToGroup(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }

    public string GetConnectionId() => Context.ConnectionId;

}

