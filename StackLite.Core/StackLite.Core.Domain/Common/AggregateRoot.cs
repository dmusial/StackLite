using System.Collections.Generic;

namespace StackLite.Core.Domain.Common
{
    public abstract class AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public IEnumerable<Event> GetUncommittedEvents()
        {
            return _changes;
        }
        
        protected void Append(Event @event)
        {
            _changes.Add(@event);
        }
    }
}
