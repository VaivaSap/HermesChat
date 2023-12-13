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

        //žinutės sukuriamos tiek privačios, tiek grupinės
        public void CreateMessage(string receiver, string message, string currentUserName)
        {
            var CurrentReceiver = _context.Users.FirstOrDefault(u => u.UserName == receiver);

            var CurrentSender = _context.Users.FirstOrDefault(u => u.UserName == currentUserName);

            var conversation = ReturnConversationOfUsers(CurrentReceiver, CurrentSender);

            var newMessage = new Message { MessageBody = message, TimeSent = DateTime.Now.ToString(), Conversation = conversation };
            _context.Messages.Add(newMessage);
            _context.SaveChanges();
        }

        //public Conversation ReturnPrivateConversation(User receiver, User sender)
        //{

        //}

        public Conversation ReturnConversationOfUsers(User receiver, User sender)
        {
            var ReceiversConversations = _context.ConversationUsers.Where(c => c.User == receiver).ToList();
            var SenderConversations = _context.ConversationUsers.Where(c => c.User == sender).ToList();
            Conversation conversation = null; //null?
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

        public List<Message> ReturnTopMessagesFromUsers(string senderUserName, string receiver, int number)
        {
            var CurrentReceiver = _context.Users.FirstOrDefault(u => u.UserName == receiver);
            var CurrentSender = _context.Users.FirstOrDefault(u => u.UserName == senderUserName);

            var conversation = ReturnConversationOfUsers(CurrentReceiver, CurrentSender);

            if (conversation != null)
            {

                var result = _context.Messages.FromSql($"SELECT TOP ({number}) * FROM [HermesChatDB].[dbo].[Message] WHERE [ConversationId] = {conversation.Id}").ToList();

                return result;
            }
            else return new List<Message>();
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

