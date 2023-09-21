using HermesChat_TeamA.Areas.Identity.Data;
using Microsoft.AspNetCore.SignalR;

namespace HermesChat_TeamA.Hubs;

public class SyncHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
      await  Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name?? "anonymous", message, DateTime.Now);
    }



    public async Task SendToParticularUser(string user, string receiverConnectionId, string message)
    {
        await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name?? "anonymous", message);
    }

    public string GetConnectionId() => Context.ConnectionId;
  
}

