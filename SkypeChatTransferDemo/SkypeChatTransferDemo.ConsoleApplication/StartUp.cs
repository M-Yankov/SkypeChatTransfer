namespace SkypeChatTransferDemo.ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using SkypeChatTransferDemo.NewAccountData;
    using SkypeChatTransferDemo.OldAccountData;

    public class StartUp
    {
        public static void Main()
        {
            const string ConversationIdentity = "<SkypeNameOfYourFriendContact>";

            // additionally installed Automapper to help in my code
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<OldAccountData.Message, NewAccountData.Message>());
            IMapper mapper = config.CreateMapper();
             
            IEnumerable<OldAccountData.Message> friendOldMessages;

            // Get messages from old account conversation
            using (OldAccountDataEntities entities = new OldAccountDataEntities())
            {
                OldAccountData.Conversation conv = entities.Conversations.FirstOrDefault(c => c.identity == ConversationIdentity);
                if (conv == null)
                {
                    return;
                }

                long conversationId = conv.id;
                friendOldMessages = entities.Messages.Where(m => m.convo_id == conversationId).ToList();
            }
            
            // Get conversation with same contact from new account 
            using (NewAccountDataEntities yankovContext = new NewAccountDataEntities())
            {
                NewAccountData.Conversation conversation = yankovContext.Conversations.FirstOrDefault(c => c.identity == ConversationIdentity);
                if (conversation == null)
                {
                    return;
                }

                long newConverstionId = conversation.id;

                long counter = 1;

                // when adding new message it needs to have unique id. So it is set manually.
                long lastId = yankovContext.Messages.OrderByDescending(c => c.id).FirstOrDefault().id;
                foreach (OldAccountData.Message message in friendOldMessages)
                {
                    lastId++;

                    Console.WriteLine("Processed {0} messages. Date {1:dd-MMM-yyyy HH:mm}", counter, new DateTime().AddSeconds(message.timestamp.Value));

                    // One line mapping.
                    NewAccountData.Message newMsg = mapper.Map<NewAccountData.Message>(message);

                    newMsg.convo_id = newConverstionId;
                    newMsg.id = lastId;

                    // adding new mapped object
                    yankovContext.Messages.Add(newMsg);
                    yankovContext.SaveChanges();

                    counter++;
                }
            }
        }
    }
}
