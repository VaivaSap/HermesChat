﻿using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using HermesChat_TeamA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

namespace HermesChat_TeamA.Hubs;

public class SyncHub : Hub
{

    private readonly IListOfGroupsRepository _groupsRepository;
    private readonly HermesChatDbContext _context;
    private readonly DataAccessService _dataAccessService;
    public SyncHub(IListOfGroupsRepository groupsRepository, HermesChatDbContext context)
    {
        _groupsRepository = groupsRepository;
        _context = context;
        _dataAccessService = new DataAccessService(_context);

    }

    [Authorize]
    public async Task SendMessage(string user, string message)
    {
        if (message.Length > 20)
        {
            await Clients.Caller.SendAsync("MessageError", "Message is too long.");
        }
        else
        {

            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message, DateTime.Now);
        }
}


    public async Task SendToParticularUser(string user, string receiverConnectionId, string message)
    {
        await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message);
        _dataAccessService.CreateMessage(receiverConnectionId, message, Context.User.Identity.Name);

    }

    //All group-chat-related

    public bool CreateGroupChat(string groupName)
    {
        return _groupsRepository.CreateNewGroupChat(groupName);

    }


    public async Task<bool> JoinGroupChat(string groupName)
    {
        bool isAdded = _groupsRepository.AddUserToGroupChat(groupName, Context.User.Identity.Name);

        if (isAdded)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.User.Identity.Name} has joined the group {groupName}.");
        }

        return isAdded;

    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        _groupsRepository.RemoveUserFromGroupChat(groupName, Context.User.Identity.Name);

        await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.User.Identity.Name} has left the group {groupName}.");

        if (_groupsRepository.UsersCountInGroupChat(groupName) == 0)
        {
            _groupsRepository.RemoveGroupFromGroupChatList(groupName);
        }
    }

    //  public string DeleteGroupChat(string groupName) {}


    public async Task SendMessageToGroup(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }

    public string GetConnectionId()
    {
        if (Context.User.Identity.Name != null && Context.ConnectionId != null)
        {
            _dataAccessService.CheckIfExistAndCreateConnectionLog(Context.ConnectionId, Context.User.Identity.Name);

        }
        return Context.ConnectionId;

    }

}

