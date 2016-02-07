using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Persistance
{
    public interface IEventPublisher
    {
        void RegisterHandler<T>(Action<T> handler) where T : Event;
        void Publish<T>(T @event) where T : Event;
    }
    
    public class MessageBus : IEventPublisher
    {
        private readonly ILogger _logger;
        
        public MessageBus(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MessageBus>();    
        }
        
        private readonly Dictionary<Type, List<Action<Event>>> _routes = new Dictionary<Type, List<Action<Event>>>();

        public void RegisterHandler<T>(Action<T> handler) where T : Event
        {
            _logger.LogInformation(string.Format("Registering handler for {0}", typeof(T).Name));
            List<Action<Event>> handlers;

            if(!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<Event>>();
                _routes.Add(typeof(T), handlers);
            }

            handlers.Add((x => handler((T)x)));
        }

        public void Publish<T>(T @event) where T : Event
        {
            List<Action<Event>> handlers;

            _logger.LogInformation(string.Format("Looking up handlers for {0}", @event.GetType().Name));

            if (!_routes.TryGetValue(@event.GetType(), out handlers)) 
            {
                _logger.LogInformation("No handlers found");
                return;
            }

            _logger.LogInformation(string.Format("Executing handlers for {0}", @event.GetType().Name));

            foreach(var handler in handlers)
            {
                handler(@event);
            }
        }
    }
}