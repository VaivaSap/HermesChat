using HermesChat_TeamA.Areas.Identity.Data.Models;
using Microsoft.AspNetCore.SignalR;

namespace HermesChat_TeamA.Hubs;

public class ConnectedUsersHub : Hub
{

    private readonly IListOfGroupsRepository _groupsRepository;
    public ConnectedUsersHub(IListOfGroupsRepository groupsRepository)
    {
        _groupsRepository = groupsRepository;
    }
    public static int UsersCount { get; set; } = 0;

    static HashSet<string> CurrentConnections = new HashSet<string>();



    public override async Task OnConnectedAsync()
    {

        UsersCount++;
        await Clients.All.SendAsync("OnlineUsersCount", UsersCount);

        var connectedUser = Context.User.Identity.Name;
        CurrentConnections.Add(connectedUser);

        await Clients.All.SendAsync("OnlineUsersList", CurrentConnections);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        UsersCount--;
        await Clients.All.SendAsync("OnlineUsersCount", UsersCount);

        var connectedUser = CurrentConnections.FirstOrDefault(x => x == Context.User.Identity.Name);


        if (connectedUser != null)
        {
            CurrentConnections.Remove(connectedUser);
        }
        await Clients.All.SendAsync("OnlineUsersList", CurrentConnections);

        await base.OnDisconnectedAsync(exception);

    }

    public List<string> GetUsersGroupChatList()
    {
        var connectedUser = Context.User.Identity.Name;
        return _groupsRepository.GetUsersGroupChatList(connectedUser);
    }


    public List<string> GetAllActiveChats()
    {
        return _groupsRepository.GetAllActiveChats();
    }






    public string GetOnlineUsersConnectionId()
    {
        return Context.ConnectionId;
    }
}

