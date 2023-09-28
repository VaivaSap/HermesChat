using HermesChat_TeamA.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HermesChat_TeamA.Hubs;

public class SyncHub : Hub
{
    [Authorize]
    public async Task SendMessage(string user, string message)
    {
      await  Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name?? "anonymous", message, DateTime.Now);
    }



    public async Task SendToParticularUser(string user, string receiverConnectionId, string message)
    {
        await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name?? "anonymous", message);
    }

   // Šitas turėtų būti geras, bet nesutvarkyta chat.js


    //public async Task AddToGroup(string groupName)
    //{
    //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

    //    await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the group {groupName}.");
    //}

    //public async Task RemoveFromGroup(string groupName)
    //{
    //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

    //    await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group {groupName}.");
    //}
    public string GetConnectionId() => Context.ConnectionId;
  
}

