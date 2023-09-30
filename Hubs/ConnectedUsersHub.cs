using Microsoft.AspNetCore.SignalR;

namespace HermesChat_TeamA.Hubs;

public class ConnectedUsersHub : Hub
{
    public static int UsersCount { get; set; } = 0;


    public override Task OnConnectedAsync()
    {
        //adding up users when they connect
        UsersCount++;
        Clients.All.SendAsync("GetOnlineUsersCount", UsersCount).GetAwaiter().GetResult();
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        // a lower online users' count is seen by all when somebody disconnects
        UsersCount--;
        Clients.All.SendAsync("GetOnlineUsersCount", UsersCount).GetAwaiter().GetResult();
        return base.OnDisconnectedAsync(exception);
    }


    public void SendUsersCount()
    {
        //..that everybody online could see how many users are currently connected
        Clients.All.SendAsync("GetOnlineUsersCount", UsersCount.ToString());
    }

    public string GetOnlineUsersConnectionId()
    {
        return Context.ConnectionId;
    }
}

