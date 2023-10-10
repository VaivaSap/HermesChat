using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using HermesChat_TeamA.Controllers;
using HermesChat_TeamA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HermesChat_TeamA.Hubs;

public class SyncHub : Hub
{
    private readonly HermesChatDbContext _context;
    private readonly DataAccessService _dataAccessService;
    public SyncHub(HermesChatDbContext context)
    {
        _context = context;
        _dataAccessService = new DataAccessService(_context);

    }
    public async Task SendMessage(string user, string message)
    {
      await  Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name?? "anonymous", message, DateTime.Now);
    }

    

    public async Task SendToParticularUser(string user, string receiverConnectionId, string message)
    {
        await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message);

        CreateMessage(receiverConnectionId, message, Context.User.Identity.Name);

        
    }
    public string GetConnectionId()
    {
        if (Context.User.Identity.Name != null && Context.ConnectionId != null)
        {
            _dataAccessService.CheckIfExistAndCreateConnectionLog(Context.ConnectionId, Context.User.Identity.Name);

        }
        return Context.ConnectionId;

    }

    public void CreateMessage(string receiverConnectionId, string message, string CurrentUserName)
    {
        _dataAccessService.CreateMessage(receiverConnectionId, message, CurrentUserName);
    }
}

