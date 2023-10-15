﻿using EO.WebBrowser.DOM;
using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.Windows;

namespace HermesChat_TeamA.Hubs;

public class SyncHub : Hub
{

    private readonly IListOfGroupsRepository _groupsRepository;

    public SyncHub(IListOfGroupsRepository groupsRepository)
    {
        _groupsRepository = groupsRepository;
    }
    public override async Task OnConnectedAsync()
    {
        var userName = Context.User.Identity.Name;
        var usersGroups = _groupsRepository.GetUsersGroupChatList(userName);

        foreach (var groupName in usersGroups)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        await base.OnConnectedAsync();
    }

     [Authorize]
    public async Task SendMessage(string user, string message)
    {
        if (message.Length > 20)
        {
          
        }
        else
        {

            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message, DateTime.Now);
        }
}


 
    public async Task SendToParticularUser(string user, string receiverConnectionId, string message)
    {
        await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message);
    }

    
    public async Task SendMessageToGroup(string user, string groupName, string message)
    {
     
        await Clients.Group(groupName).SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message, groupName);
       
    }

   
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


 

    public string GetConnectionId() => Context.ConnectionId;

}

