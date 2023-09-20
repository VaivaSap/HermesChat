using Microsoft.AspNetCore.SignalR;

namespace HermesChat_TeamA.Hubs;

public class SyncHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.Now);
    }

}
