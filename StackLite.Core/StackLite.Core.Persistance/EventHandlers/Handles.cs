using StackLite.Core.Domain.Common;

namespace StackLite.Core.Persistance.EventHandlers
{
    public interface Handles<T> where T : Event
    {
        void Handle(T @event);
    }
}