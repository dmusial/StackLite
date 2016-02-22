using System;
using System.Net;
using System.Text;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Answers;
using StackLite.Core.Domain.Questions;
using StackLite.Core.Persistance;
using StackLite.Core.Projections.Handlers;

namespace StackLite.Core.Projections
{
    public class Program
    {
        private static IEventPublisher _eventPublisher;
        
        public static void Main(string[] args)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            _eventPublisher = new MessageBus(loggerFactory);
            RegisterHandlers(_eventPublisher);
            
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync().Wait();

            // var subscription = connection.SubscribeToAllAsync(true, Appeared, Dropped, new UserCredentials("admin", "changeit")).Result;
            var subscription = connection.SubscribeToAllFrom(Position.Start, true, Appeared, null, Dropped, new UserCredentials("admin", "changeit"));

            Console.Read();
        }
        
        private static void Appeared(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {   
            if (resolvedEvent.Event.EventType.Contains("Question") || resolvedEvent.Event.EventType.Contains("Answer"))
            {
                Console.WriteLine("Read event {0} with data: {1}", 
                    resolvedEvent.Event.EventType, 
                    Encoding.UTF8.GetString(resolvedEvent.Event.Data));
                
                var @event = EventDeserializer.Deserialize(resolvedEvent.Event.EventType, Encoding.UTF8.GetString(resolvedEvent.Event.Data));
                _eventPublisher.Publish(@event);
            }
        }

        private static void Dropped(EventStoreCatchUpSubscription subscription, SubscriptionDropReason subscriptionDropReason, Exception exception)
        {
            
        }
        
        private static void RegisterHandlers(IEventPublisher eventPublisher)
        {
            var questionHandler = new QuestionHandler();
            var answerHandler = new AnswerHandler();
            
            eventPublisher.RegisterHandler<QuestionAsked>(questionHandler.Handle);
            eventPublisher.RegisterHandler<QuestionAmended>(questionHandler.Handle);
            
            eventPublisher.RegisterHandler<AnswerSuggested>(answerHandler.Handle);
            eventPublisher.RegisterHandler<AnswerAmended>(answerHandler.Handle);
            eventPublisher.RegisterHandler<AnswerUpvoted>(answerHandler.Handle);
            eventPublisher.RegisterHandler<AnswerDownvoted>(answerHandler.Handle);
        }
    }
}
