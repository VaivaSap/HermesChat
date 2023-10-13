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

            var conversation = ReturnConversationOfUsers(CurrentReceiver, CurrentSender);

            var newMessage = new Message { MessageBody = message, TimeSent = DateTime.Now.ToString(), Conversation = conversation };
            _context.Messages.Add(newMessage);
            _context.SaveChanges();
        }

       

        public Conversation ReturnConversationOfUsers(User receiver, User sender) 
        {
            var ReceiversConversations = _context.ConversationUsers.Where(c => c.User == receiver).ToList();
            var SenderConversations = _context.ConversationUsers.Where(c => c.User == sender).ToList();
            Conversation conversation = null;
            if (ReceiversConversations.Any() && SenderConversations.Any()) 
            {
                while (conversation == null) 
                {
                    foreach (var ConversationUser in ReceiversConversations)
                    {
                        var conversationUser = SenderConversations.FirstOrDefault(c => c.Conversation == ConversationUser.Conversation);
                        if (conversationUser != null)
                        {
                            var conversationId = conversationUser.ConversationId;

                            conversation = _context.Conversations.FirstOrDefault(c => c.Id == conversationId);
                        }
                    }
                }
                
            }
            
            if (conversation == null) 
            {
                conversation = new Conversation();
                var conversationReceiver = new ConversationUser { Conversation = conversation, User = receiver };
                var conversationSender = new ConversationUser { Conversation = conversation, User = sender };

                receiver.ConversationUser.Add(conversationReceiver);
                sender.ConversationUser.Add(conversationSender);

                conversation.ConversationUser.Add(conversationReceiver);
                conversation.ConversationUser.Add(conversationSender);

                _context.SaveChanges();
                
            }
            return conversation;
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


    }
}
