using System;
using System.Collections.Generic;
using StackLite.Core.Domain.Common;

namespace StackLite.Core.Persistance
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events);
        List<Event> GetEventsForAggregate(Guid aggregateId);
    }
}