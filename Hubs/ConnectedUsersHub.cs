using HermesChat_TeamA.Areas.Identity.Data.Models;
using Microsoft.AspNetCore.SignalR;

namespace HermesChat_TeamA.Hubs;

public class ConnectedUsersHub : Hub
{
    public static int UsersCount { get; set; } = 0;

    static HashSet<string> CurrentConnections = new HashSet<string>();

   //Context.User.Identity.Name?? 

    public override async Task OnConnectedAsync()
    {
        //adding up users when they connect
        UsersCount++;
        await Clients.All.SendAsync("OnlineUsersCount", UsersCount);

        var connectedUser = Context.User.Identity.Name;
        CurrentConnections.Add(connectedUser);

        await Clients.All.SendAsync("OnlineUsersList", CurrentConnections);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // a lower online users' count is seen by all when somebody disconnects
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

    public string GetOnlineUsersConnectionId()
    {
        return Context.ConnectionId;
    }
}

