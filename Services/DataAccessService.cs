using HermesChat_TeamA.Areas.Identity.Data;
using HermesChat_TeamA.Areas.Identity.Data.Models;
using HermesChat_TeamA.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HermesChat_TeamA.Services
{
    public class DataAccessService
    {
        private readonly HermesChatDbContext _context;
        private readonly UserManager<User> _userManager;
        public DataAccessService(HermesChatDbContext context)
        {
            _context = context;
        }

        public void CreateMessage(string receiverConnectionId, string message, string currentUserName)
        {
            var CurrentReceiver = _context.Users.FirstOrDefault
            (u => u.Id == _context.ConnectionLogs.FirstOrDefault
            (u => u.Connection == receiverConnectionId).ConnectedUserId);
            var CurrentSender = _context.Users.FirstOrDefault(u => u.UserName == currentUserName);

            var conversation = CheckIfExistAndCreateConversation(CurrentReceiver, CurrentSender);

            var newMessage = new Message { MessageBody = message, TimeSent = DateTime.Now.ToString(), Conversation = conversation };
            _context.Messages.Add(newMessage);
            _context.SaveChanges();
        }

        public void CheckIfExistAndCreateConnectionLog(string connectionId, string currentUserName)
        {
            var connectionLog = _context.ConnectionLogs.FirstOrDefault(i => i.Connection == connectionId);

            if (connectionLog == null)
            { 
                var CurrentUserId = _context.Users.FirstOrDefault(u => u.UserName == currentUserName).Id;

                connectionLog = new ConnectionLog { Connection = connectionId, ConnectedUserId = CurrentUserId };
                _context.ConnectionLogs.Add(connectionLog);
                _context.SaveChanges();
            };
        }

        public Conversation CheckIfExistAndCreateConversation(User receiver, User sender)
        {
            var UsersinConversation = new List<User> { receiver, sender };

            //var conversation = _context.Conversations.FirstOrDefault(c => c.Users == UsersinConversation);
            //if (conversation == null)
            //{
                var conversation = new Conversation(UsersinConversation);
                _context.Conversations.Add(conversation);
                _context.SaveChanges();
            //}
            return conversation;
        }


    }
}
