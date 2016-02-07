using System;
using System.Collections.Generic;
using System.Linq;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Persistance
{
public class EventStore : IEventStore
{
        private struct EventDescriptor
        {

            public readonly Event EventData;
            public readonly Guid Id;

            public EventDescriptor(Guid id, Event eventData)
            {
                EventData = eventData;
                Id = id;
            }
        }

        private readonly IEventPublisher _eventPublisher;

        private readonly Dictionary<Guid, List<EventDescriptor>> _current = new Dictionary<Guid, List<EventDescriptor>>();

        public EventStore(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events)
        {
            List<EventDescriptor> eventDescriptors;

            if(!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                _current.Add(aggregateId, eventDescriptors);
            }

            foreach (var @event in events)
            {
                eventDescriptors.Add(new EventDescriptor(aggregateId, @event));
                _eventPublisher.Publish(@event);
            }
        }


        public  List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            List<EventDescriptor> eventDescriptors;

            if (!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }

            return eventDescriptors.Select(desc => desc.EventData).ToList();
        }
    }

    public class AggregateNotFoundException : Exception
    {
    }
}